using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPs_demo.Classes
{
    class Program
    {
        public string ProgramCode { get; set; }
        public string Description { get; set; }

        public ICollection<Student> Students { get; set; }

        public Program()
        {
            // empty constructor.
        }
        
    }
}
