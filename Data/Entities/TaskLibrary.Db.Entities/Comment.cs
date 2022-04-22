using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.Db.Entities
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }

        public int ProgrammingTaskId { get; set; }
        public virtual ProgrammingTask ProgrammingTask{get;set;}

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
