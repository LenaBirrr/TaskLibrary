using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.Db.Entities
{
    public class ProgrammingLanguage:BaseEntity
    {
        public string Name { get; set; }
       
        public string Level { get; set; }

        public string Paradigm { get; set; }

        public string Realization { get; set; }

        public virtual ICollection<LanguageForTask>? ProgrammingTasks { get; set; }
        public virtual ICollection<Solution>? Solutions { get; set; }
    }
}
