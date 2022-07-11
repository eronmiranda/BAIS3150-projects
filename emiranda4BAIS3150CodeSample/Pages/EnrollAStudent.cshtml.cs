using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using emiranda4BAIS3150CodeSample.Classes;
using System.ComponentModel.DataAnnotations;

namespace emiranda4BAIS3150CodeSample.Pages
{
    public class EnrollAStudentModel : PageModel
    {
        public string ErrorMessage { get; set; }
        public string FormMessage { get; set; }
        
        [BindProperty]
        [Required(ErrorMessage = "Student ID is required")]
        [RegularExpression("[a-zA-Z0-9]{1,10}", ErrorMessage = "Student ID can only have letters and numbers. Maximum of 10 characters. (e.g. johndoe1)")]
        public string StudentID { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Special characters and numbers are not allowed.")]
        public string FirstName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Last Name is required")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Special characters and numbers are not allowed.")]
        public string LastName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        //[RegularExpression("/[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/g", ErrorMessage = "Please enter a valid email address (e.g. johndoe@email.com)")]
        // I used input = "email" validation
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Program Code is required")]
        [RegularExpression("[a-zA-Z]{1,10}", ErrorMessage = "Program Code can only have capital letters. Maximum of 10 letters. (e.g. BAIST)")]
        public string ProgramCode { get; set; }


        public void OnPostSubmit()
        {
            Student acceptedStudent = new Student
            {
                StudentID = StudentID,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email
            };

            if(EnrollStudent(acceptedStudent,ProgramCode))
            {
                FormMessage = "You have successfully enrolled " + acceptedStudent.FirstName + " " + acceptedStudent.LastName + " into " + ProgramCode + " program.";

                StudentID = "";
                FirstName = "";
                LastName = "";
                Email = "";
                ProgramCode = "";
            }
            else
            {
                ErrorMessage = "Student enrollment was not successful";
            }
        }

        public bool EnrollStudent(Student acceptedStudent, string programCode)
        {
            bool success = false;
            BCS RequestDirector = new BCS();

            try
            {
                success = RequestDirector.EnrollStudent(acceptedStudent, programCode);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message.ToString();
            }

            return success;            
        }
    } 
}
