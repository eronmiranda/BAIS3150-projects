using System;
using System.Collections.Generic;
using OOPs_demo;
using OOPs_demo.Classes;

namespace OOPs_demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Student AcceptedStudent = new Student()
            {
                StudentID = "msmith1234",
                FirstName = "Mary",
                LastName = "Smith",
                Email = "msmith1@nait.ca",
            };
            string option;

            do
            {
                Console.Clear();
                DisplayMenu();
                string programCode, studentID;
                Console.Write("Choose your option: ");
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        // Add Program
                        Console.Write("Enter a program code: ");
                        programCode = Console.ReadLine().ToUpper();
                        Console.Write("Enter a description: ");
                        string description = Console.ReadLine();
                        AddProgram(programCode, description);
                        break;
                    case "2":
                        //  Enroll Student
                        Console.Write("Enter a program code:");
                        programCode = Console.ReadLine();
                        EnrollStudent(AcceptedStudent, programCode);
                        break;
                    case "3":
                        // Find Student
                        Console.Write("Enter student ID: ");
                        studentID = Console.ReadLine();
                        FindStudent(studentID);
                        break;
                    case "4":
                        // Modify Student
                        Console.Write("Enter student ID: ");
                        studentID = Console.ReadLine();
                        ModifyStudent(studentID);
                        break;
                    case "5":
                        // Delete Student
                        Console.Write("Enter student ID: ");
                        studentID = Console.ReadLine();
                        DeleteStudent(studentID);
                        break;
                    case "6":
                        // Find Program
                        Console.Write("Enter a program code: ");
                        programCode = Console.ReadLine().ToUpper();
                        FindProgram(programCode);
                        break;
                    case "x":
                    case "X":
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
                Console.WriteLine();
                Console.Write("Click any key to go back to menu. . . .");
                Console.ReadKey();
            } while (option != "x" && option != "X");
        }

        public static void DisplayMenu()
        {
            Console.WriteLine("MENU OPTIONS");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Please enter any of the options below");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("1. Add Program");
            Console.WriteLine("2. Enroll Student");
            Console.WriteLine("3. Find Student");
            Console.WriteLine("4. Modify Student");
            Console.WriteLine("5. Delete Student");
            Console.WriteLine("6. Find Program");
            Console.WriteLine("X  to exit");
            Console.WriteLine("----------------------------------");
            Console.WriteLine();
        }

        public static void AddProgram(string programCode, string description)
        {
            BCS RequestDirector = new BCS();
            // Add Program
            try
            {
                bool isSuccessful = RequestDirector.CreateProgram(programCode, description);
                Console.WriteLine(isSuccessful);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public static void EnrollStudent(Student acceptedStudent, string programCode)
        {
            BCS RequestDirector = new BCS();
            try
            {
                bool confirmation;
                confirmation = RequestDirector.EnrollStudent(acceptedStudent, programCode);
                Console.WriteLine(confirmation);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void FindStudent(string studentID)
        {
            BCS RequestDirector = new BCS();
            // Find Student
            try
            {
                Student ExistingStudent = RequestDirector.FindStudent(studentID);
                Console.WriteLine("Student ID: " + ExistingStudent.StudentID);
                Console.WriteLine("First Name: " + ExistingStudent.FirstName);
                Console.WriteLine("Last Name: " + ExistingStudent.LastName);
                Console.WriteLine("Email: " + ExistingStudent.Email);
                Console.WriteLine("Program Code: " + ExistingStudent.ProgramCode);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public static void ModifyStudent(string studentID)
        {
            BCS RequestDirector = new BCS();
            try
            {
                bool success;
                Student EnrolledStudent = RequestDirector.FindStudent(studentID);
                EnrolledStudent.LastName = "TESTUPDATE";
                success = RequestDirector.ModifyStudent(EnrolledStudent);
                Console.WriteLine(success);
                EnrolledStudent = RequestDirector.FindStudent("msmith1234");
                Console.WriteLine("Student ID: " + EnrolledStudent.StudentID);
                Console.WriteLine("First Name: " + EnrolledStudent.FirstName);
                Console.WriteLine("Last Name: " + EnrolledStudent.LastName);
                Console.WriteLine("Email: " + EnrolledStudent.Email);
                Console.WriteLine("Program Code: " + EnrolledStudent.ProgramCode);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteStudent(string studentID)
        {
            BCS RequestDirector = new BCS();
            try
            {
                Student EnrolledStudent = RequestDirector.FindStudent(studentID);
                bool success = RequestDirector.RemoveStudent(EnrolledStudent.StudentID);
                Console.WriteLine(success);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void FindProgram(string programCode)
        {
            BCS RequestDirector = new BCS();
            try
            {
                Classes.Program ExistingProgram = RequestDirector.FindProgram(programCode);
                Console.WriteLine("Program Code: " + ExistingProgram.ProgramCode);
                Console.WriteLine("Description: " + ExistingProgram.Description);

                    Console.WriteLine("----------------------------------");
                    Console.WriteLine("List of Students: ");
                if (ExistingProgram.Students.Count > 0)
                {
                    foreach (Student student in ExistingProgram.Students)
                    {
                        Console.WriteLine("----------------------------------");
                        Console.WriteLine("Student ID: " + student.StudentID);
                        Console.WriteLine("First Name: " + student.FirstName);
                        Console.WriteLine("Last Name: " + student.LastName);
                        Console.WriteLine("Email: " + student.Email);
                        Console.WriteLine("Program Code: " + student.ProgramCode);
                    }
                }
                else
                {
                    Console.WriteLine("No students enrolled in this program");
                }
                Console.WriteLine("----------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
