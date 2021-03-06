using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using emiranda4BAIS3150CodeSample.Classes;
using emiranda4BAIS3150CodeSample.Services;

namespace emiranda4BAIS3150CodeSample
{
    public class BCS
    {
        public bool CreateProgram(string ProgramCode, string Description)
        {
            bool confirmation = false;

            Programs ProgramManager = new Programs();

            confirmation = ProgramManager.AddProgram(ProgramCode, Description);

            return confirmation;
        }

        public bool EnrollStudent(Student AcceptedStudent, string programCode)
        {
            Students StudentManager = new Students();

            return StudentManager.AddStudent(AcceptedStudent, programCode);
        }

        public Student FindStudent(string studentID)
        {
            Students StudentManager = new Students();

            return StudentManager.GetStudent(studentID);
        }

        public bool ModifyStudent(Student EnrolledStudent)
        {
            Students StudentManager = new Students();

            return StudentManager.UpdateStudent(EnrolledStudent);
        }

        public bool RemoveStudent(string studentID)
        {
            Students StudentManager = new Students();

            return StudentManager.DeleteStudent(studentID);
        }

        public Classes.Program FindProgram(string programCode)
        {
            Programs ProgramManager = new Programs();

            return ProgramManager.GetProgram(programCode);
        }
    }
}
