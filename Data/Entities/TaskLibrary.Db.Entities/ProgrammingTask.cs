using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.Db.Entities
{
    public class ProgrammingTask:BaseEntity
    {
        public string ProblemStatement { get; set; }

        public string Name { get; set; }

        public string Answers { get; set; }

        public Guid? UserId { get; set; }

        public int? CategoryId { get; set; }


        public virtual User? User { get; set; }
        public virtual Category? Category{get;set;}
        public virtual ICollection<LanguageForTask>? ProgrammingLanguages { get; set; }
        public virtual ICollection<Solution>? Solutions { get; set; }
        public virtual ICollection<Subscription>? Subscriptions { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
