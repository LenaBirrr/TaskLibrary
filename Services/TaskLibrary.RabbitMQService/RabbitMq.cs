﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using TaskLibrary.Settings;

namespace TaskLibrary.RabbitMQService
{
    public class RabbitMq:IRabbitMq, IDisposable
    {
        private readonly object connectionLock = new();
        private readonly IRabbitMqSettings settings;
        private IModel channel;

        private IConnection connection;

        public RabbitMq(IRabbitMqSettings settings)
        {
            this.settings = settings;
        }

        public void Dispose()
        {
            channel?.Close();
            connection?.Close();
        }

        private IModel GetChannel()
        {
            return channel;
        }

        private async Task RegisterListener(string queueName, EventHandler<BasicDeliverEventArgs> onReceive, int? messageLifetime = null)
        {
            Connect();

            await AddQueue(queueName, messageLifetime);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += onReceive;

            channel.BasicConsume(queueName, false, consumer);
        }

        private async Task Publish<T>(string queueName, T data)
        {
            Connect();

            await AddQueue(queueName);

            var json = JsonSerializer.Serialize<object>(data, new JsonSerializerOptions() { });

            var message = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(string.Empty, queueName, null, message);
        }

        private void Connect()
        {
            lock (connectionLock)
            {
                if (connection?.IsOpen ?? false)
                    return;

                var factory = new ConnectionFactory
                {
                    Uri = new Uri(settings.Uri),
                    UserName = settings.UserName,
                    Password = settings.Password,

                    AutomaticRecoveryEnabled = true,
                    NetworkRecoveryInterval = TimeSpan.FromSeconds(5)
                };

                var retriesCount = 0;
                while (retriesCount < 15)
                    try
                    {
                        if (connection == null)
                        {
                            connection = factory.CreateConnection();
                        }

                        if (channel == null)
                        {
                            channel = connection.CreateModel();
                            channel.BasicQos(0, 1, false);
                        }

                        break;
                    }
                    catch (BrokerUnreachableException)
                    {
                        Task.Delay(1000).Wait();

                        retriesCount++;
                    }
            }
        }

        private async Task AddQueue(string queueName, int? messageLifetime = null)
        {
            Connect();
            channel.QueueDeclare(queueName, true, false, false, null);
        }

        public async Task Subscribe<T>(string queueName, OnDataReceiveEvent<T> onReceive)
        {
            if (onReceive == null)
                return;

            await RegisterListener(queueName, async (_, eventArgs) =>
            {
                var channel = GetChannel();
                try
                {
                    var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

                    var obj = JsonSerializer.Deserialize<T>(message ?? "");

                    await onReceive(obj);
                    channel.BasicAck(eventArgs.DeliveryTag, false);
                }
                catch (Exception e)
                {
                    channel.BasicNack(eventArgs.DeliveryTag, false, false);
                }
            });
        }

        public async Task PushAsync<T>(string queueName, T data)
        {
            await Publish(queueName, data);
        }
    }
}
