using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.Db.Entities
{
    public class User:IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public UserStatus Status { get; set; }

        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Solution>? Solutions { get; set; }
        public ICollection<Subscription>? Subscriptions { get; set; }

        public ICollection<ProgrammingTask>? ProgrammingTasks { get; set; }
    }
}
