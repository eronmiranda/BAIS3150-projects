using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emiranda4BAIS3150CodeSample.Classes
{
    public class Student
    {
        private string _studentID;
        private string _email;

        public string StudentID
        {
            get { return _studentID; }
            set { _studentID = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProgramCode { get; set; }

        public Student()
        {
            // Empty constructor for now.
        }
    }
}
