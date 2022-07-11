using OOPs_demo.Classes;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OOPs_demo
{
    class Program
    {
        static void Main(string[] args)
        {
            string option;

            do
            {
                string studentID,
                       programCode,
                       description;

                AnsiConsole.Clear();

                AnsiConsole.Write(new FigletText("BCS - BAIS3150")
                                        .Centered()
                                        .Color(Color.LightCyan1));

                AnsiConsole.Write(new FigletText("System")
                        .Centered()
                        .Color(Color.LightCyan1));

                AnsiConsole.WriteLine();
                AnsiConsole.WriteLine();
                AnsiConsole.WriteLine();

                option = DisplayMenu();

                switch (option)
                {
                    case "Add Program":
                        DisplayHeader("Add Program");
                        AnsiConsole.WriteLine();

                        do
                        {
                            programCode = AnsiConsole.Ask<string>("Enter a [gold3_1]program code[/]:").ToUpper();
                            description = AnsiConsole.Ask<string>("Enter a [gold3_1]description[/]:");

                            AnsiConsole.WriteLine();

                            // Insert table here
                            Classes.Program newProgram = new Classes.Program
                            {
                                ProgramCode = programCode,
                                Description = description
                            };
                            DisplayProgramTable(newProgram);

                            AnsiConsole.WriteLine();
                        } while (!AnsiConsole.Confirm("Is the information above correct?"));

                        AnsiConsole.WriteLine();

                        AddProgram(programCode, description);
                        break;

                    case "Enroll Student":
                        DisplayHeader("Enroll Student");
                        AnsiConsole.WriteLine();

                        Student AcceptedStudent = new Student();

                        do
                        {
                            AcceptedStudent.StudentID = AnsiConsole.Ask<string>("Enter [gold3_1]Student ID[/]:").ToLower();
                            AcceptedStudent.FirstName = AnsiConsole.Ask<string>("Enter student's [gold3_1]First Name[/]:");
                            AcceptedStudent.LastName = AnsiConsole.Ask<string>("Enter student's [gold3_1]Last Name[/]:");
                            AcceptedStudent.Email = AnsiConsole.Ask<string>("Enter student's [gold3_1]Email Address[/]:");
                            AcceptedStudent.ProgramCode = AnsiConsole.Ask<string>("Enter [gold3_1]program code[/]:");

                            AnsiConsole.WriteLine();

                            // Insert table here
                            DisplayStudentTable(AcceptedStudent);

                            AnsiConsole.WriteLine();
                        } while (!AnsiConsole.Confirm("Is the information above correct?"));

                        AnsiConsole.WriteLine();

                        EnrollStudent(AcceptedStudent, AcceptedStudent.ProgramCode);
                        break;

                    case "Find Student":
                        DisplayHeader("Find Student");
                        AnsiConsole.WriteLine();

                        studentID = AnsiConsole.Ask<string>("Enter [gold3_1]Student ID[/]:").ToLower();

                        FindStudent(studentID);
                        break;

                    case "Modify Student":
                        DisplayHeader("Modify Student");
                        AnsiConsole.WriteLine();

                        studentID = AnsiConsole.Ask<string>("Enter [gold3_1]Student ID[/]:").ToLower();

                        Student EnrolledStudent = new Student();
                        do
                        {
                            EnrolledStudent = FindStudent(studentID);
                            AnsiConsole.WriteLine();

                            List<string> columnList = GetModifyStudentColumnList();
                            foreach (string column in columnList)
                            {
                                switch (column)
                                {
                                    case "Student ID":
                                        EnrolledStudent.StudentID = AnsiConsole.Ask<string>("Enter [gold3_1]Student ID[/]:").ToLower();
                                        break;
                                    case "First Name":
                                        EnrolledStudent.FirstName = AnsiConsole.Ask<string>("Enter student's [gold3_1]First Name[/]:");
                                        break;
                                    case "Last Name":
                                        EnrolledStudent.LastName = AnsiConsole.Ask<string>("Enter student's [gold3_1]Last Name[/]:");
                                        break;
                                    case "Email":
                                        EnrolledStudent.Email = AnsiConsole.Ask<string>("Enter student's [gold3_1]Email Address[/]:");
                                        break;
                                    case "Program Code":
                                        EnrolledStudent.ProgramCode = AnsiConsole.Ask<string>("Enter [gold3_1]program code[/]:");
                                        break;
                                    default:
                                        break;
                                }
                            }

                            AnsiConsole.WriteLine();

                            DisplayStudentTable(EnrolledStudent);

                            AnsiConsole.WriteLine();
                        } while (!AnsiConsole.Confirm("Is the information above correct?"));

                        AnsiConsole.WriteLine();

                        ModifyStudent(EnrolledStudent);
                        break;

                    case "Delete Student":
                        DisplayHeader("Delete Student");
                        AnsiConsole.WriteLine();

                        studentID = AnsiConsole.Ask<string>("Enter [gold3_1]Student ID[/]:").ToLower();

                        AnsiConsole.WriteLine();

                        EnrolledStudent = FindStudent(studentID);

                        AnsiConsole.WriteLine();
                        if (EnrolledStudent.StudentID != null)
                        {
                            if (AnsiConsole.Confirm("Are you sure you want to delete [gold3_1]" + EnrolledStudent.FirstName + " " + EnrolledStudent.LastName + "[/] to the Student List?"))
                            {
                                AnsiConsole.WriteLine();
                                DeleteStudent(studentID);
                            }
                        }
                        break;

                    case "Find Program":
                        DisplayHeader("Find Program");
                        AnsiConsole.WriteLine();

                        programCode = AnsiConsole.Ask<string>("Enter a [gold3_1]Program Code[/]:").ToUpper();

                        AnsiConsole.WriteLine();

                        FindProgram(programCode);
                        break;

                    case "[red]Exit Program[/]":
                        break;

                    default:
                        break;
                }
                Console.WriteLine();
            } while (option != "[red]Exit Program[/]" && AnsiConsole.Confirm("Do you want to go back to menu?"));

            AnsiConsole.WriteLine();

            DisplaySystemMessage("[yellow]Thank you for using BCS: BAIS3150 Code Sample System. Have a great day![/]", Color.Blue);
        }

        private static string DisplayMenu()
        {
            string option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Please select from one of the [gold3_1]options[/] below")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                    .AddChoices(new[] {
                        "Add Program", "Enroll Student",
                        "Find Student", "Modify Student", "Delete Student",
                        "Find Program", "[red]Exit Program[/]"
                    }));

            return option;
        }
        private static void DisplayHeader(string headerMessage)
        {
            var rule = new Rule("[bold]" + headerMessage + "[/]");
            rule.Style = Style.Parse("bold invert");
            AnsiConsole.Write(rule);

        }

        private static void DisplaySystemMessage(string message, Color borderColor)
        {
            var panel = new Panel(message);
            panel.Header(" System Message ", Justify.Center);
            panel.Border = BoxBorder.Double;
            panel.BorderColor(borderColor);
            panel.Expand().PadLeft(2);

            AnsiConsole.Write(panel);
        }

        private static void DisplayStudentTable(Student student)
        {
            Table studentTable = new Table();
            studentTable.Border(TableBorder.HeavyHead);
            studentTable.Width(80);

            studentTable.AddColumns(new[]
            {
                "[cyan3]Student ID[/]", "[cyan3]First Name[/]", "[cyan3]Last Name[/]", "[cyan3]Email[/]", "[cyan3]Program Code[/]"
            });

            studentTable.Columns[0].PadLeft(1);
            studentTable.Columns[1].PadLeft(1);
            studentTable.Columns[2].PadLeft(1);
            studentTable.Columns[3].PadLeft(1);
            studentTable.Columns[4].PadLeft(1);

            studentTable.AddRow(student.StudentID, student.FirstName, student.LastName, student.Email, student.ProgramCode);

            AnsiConsole.Write(studentTable);
        }

        private static void DisplayStudentTable(List<Student> studentList)
        {
            Table studentTable = new Table();
            studentTable.Border(TableBorder.HeavyHead);
            studentTable.Width(80);

            studentTable.AddColumns(new[]
            {
                "[cyan3]Student ID[/]", "[cyan3]First Name[/]", "[cyan3]Last Name[/]", "[cyan3]Email[/]", "[cyan3]Program Code[/]"
            });

            studentTable.Columns[0].PadLeft(1);
            studentTable.Columns[1].PadLeft(1);
            studentTable.Columns[2].PadLeft(1);
            studentTable.Columns[3].PadLeft(1);
            studentTable.Columns[4].PadLeft(1);
            if (studentList.Count > 0)
            {
                foreach (Student student in studentList)
                {
                    studentTable.AddRow(student.StudentID, student.FirstName, student.LastName, student.Email, student.ProgramCode);
                }

                AnsiConsole.Write(studentTable);
            }
            else
            {
                var panel = new Panel("[cyan3]No students are enrolled in this program[/]");
                panel.Header("Student List", Justify.Center);
                panel.Border = BoxBorder.Ascii;
                panel.PadLeft(2);

                AnsiConsole.Write(panel);
            }
        }

        private static void DisplayProgramTable(Classes.Program program)
        {
            Table programTable = new Table();
            programTable.Border(TableBorder.HeavyHead);
            programTable.Width(80);

            programTable.AddColumns(new[]
            {
                "[cyan3]Program Code[/]", "[cyan3]Description[/]"
            });

            programTable.Columns[0].PadLeft(1);
            programTable.Columns[1].PadLeft(1);


            programTable.AddRow(program.ProgramCode, program.Description);

            AnsiConsole.Write(programTable);
        }

        private static List<string> GetModifyStudentColumnList()
        {
            List<string> studentColumns = AnsiConsole.Prompt(
                            new MultiSelectionPrompt<string>()
                                .Title("Please select student information you want to update")
                                .Required() // Required to have a selection
                                .PageSize(10)
                                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                                .InstructionsText(
                                    "[grey](Press [gold3_1]<space>[/] to toggle a student information, " +
                                    "[green]<enter>[/] to accept)[/]")
                                .AddChoices(new[] {
                                    "Student ID", "First Name",
                                    "Last Name", "Email", "Program Code",
                                }));

            return studentColumns;
        }

        private static void AddProgram(string programCode, string description)
        {
            BCS RequestDirector = new BCS();

            try
            {
                bool isSuccessful = RequestDirector.CreateProgram(programCode, description);
                if (isSuccessful)
                {
                    DisplaySystemMessage("Successfully added [gold3_1]" + programCode + "[/] to the Programs list", Color.Green);
                }
            }
            catch (Exception ex)
            {
                DisplaySystemMessage("[[Error Message]] " + ex.Message, Color.Red);
            }
        }

        private static void EnrollStudent(Student acceptedStudent, string programCode)
        {
            BCS RequestDirector = new BCS();

            try
            {
                bool confirmation = RequestDirector.EnrollStudent(acceptedStudent, programCode);
                if (confirmation)
                {
                    DisplaySystemMessage("Successfully enrolled [gold3_1]" + acceptedStudent.FirstName + " " + acceptedStudent.LastName + "[/] to [gold3_1]" + programCode + "[/] program.", Color.Green);
                }
            }
            catch (Exception ex)
            {
                DisplaySystemMessage("[[Error Message]] " + ex.Message, Color.Red);
            }
        }

        private static Student FindStudent(string studentID)
        {
            BCS RequestDirector = new BCS();
            Student ExistingStudent = new Student();
            // Find Student
            try
            {
                ExistingStudent = RequestDirector.FindStudent(studentID);

                DisplayStudentTable(ExistingStudent);
            }
            catch (Exception ex)
            {
                DisplaySystemMessage("[[Error Message]] " + ex.Message, Color.Red);
            }

            return ExistingStudent;
        }

        private static void ModifyStudent(Student EnrolledStudent)
        {
            BCS RequestDirector = new BCS();
            try
            {
                bool success = RequestDirector.ModifyStudent(EnrolledStudent);
                if (success)
                {
                    DisplaySystemMessage("Successfully updated [gold3_1]" + EnrolledStudent.FirstName + " " + EnrolledStudent.LastName + "[/] information.", Color.Green);
                }
            }
            catch (Exception ex)
            {
                DisplaySystemMessage("[[Error Message]] " + ex.Message, Color.Red);
            }
        }

        private static void DeleteStudent(string studentID)
        {
            BCS RequestDirector = new BCS();
            try
            {
                Student EnrolledStudent = RequestDirector.FindStudent(studentID);
                bool success = RequestDirector.RemoveStudent(EnrolledStudent.StudentID);
                if (success)
                {
                    DisplaySystemMessage("Successfully deleted [gold3_1]" + EnrolledStudent.FirstName + " " + EnrolledStudent.LastName + "[/] information.", Color.Green);
                }
            }
            catch (Exception ex)
            {
                DisplaySystemMessage("[[Error Message]] " + ex.Message, Color.Red);
            }
        }

        private static void FindProgram(string programCode)
        {
            BCS RequestDirector = new BCS();
            try
            {
                Classes.Program ExistingProgram = RequestDirector.FindProgram(programCode);

                DisplayProgramTable(ExistingProgram);
                DisplayStudentTable(ExistingProgram.Students.ToList());
            }
            catch (Exception ex)
            {
                DisplaySystemMessage("[[Error Message]] " + ex.Message, Color.Red);
            }
        }
    }
}
