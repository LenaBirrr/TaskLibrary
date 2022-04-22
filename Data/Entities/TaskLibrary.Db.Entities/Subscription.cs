using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.Db.Entities
{
    public class Subscription:BaseEntity
    {
        public virtual ProgrammingTask ProgrammingTask { get; set; }

        public virtual ICollection<Notification>? Notifications { get; set; }
        public virtual User User { get; set; }
        public int ProgrammingTaskId { get; set; }
        public Guid UserId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
    }
}
