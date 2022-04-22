using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.Db.Entities
{
    public class LanguageForTask:BaseEntity
    {
        public int ProgrammingLanguageId { get; set; }
        public int ProgrammingTaskId { get; set; }
        public virtual ProgrammingLanguage ProgrammingLanguage { get; set; }
        public virtual ProgrammingTask ProgrammingTask { get; set; }

    }
}
