using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.Db.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ProgrammingTask>? ProgrammingTasks { get; set; }
    }
}
