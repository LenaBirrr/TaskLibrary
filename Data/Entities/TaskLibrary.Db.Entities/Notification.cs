using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.Db.Entities
{
    public class Notification:BaseEntity
    {
        public Subscription? Subscription { get; set; }
        public int? SubscribtionId { get; set; }
        public string Text { get; set; }

    }
}
