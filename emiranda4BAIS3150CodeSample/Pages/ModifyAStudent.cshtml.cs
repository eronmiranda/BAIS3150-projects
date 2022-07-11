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
    public class ModifyAStudentModel : PageModel
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

        public Student ExistingStudent { get; set; }

        public bool Disable { get; set; }

        public void OnPostSearch()
        {
            try
            {
                ExistingStudent = new BCS().FindStudent(StudentID);
                Disable = false;
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message.ToString();
            }
        }

        public void OnPostSubmit()
        {
            Student existingStudent = new Student
            {
                StudentID = StudentID,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                ProgramCode = ProgramCode
            };

            if (ModifyStudent(existingStudent))
            {
                FormMessage = "You have successfully modified " + existingStudent.FirstName + " " + existingStudent.LastName + "'s student account information.";
                Disable = true;
            }
            else
            {
                ErrorMessage = "Updating student's information was not successful.";
            }
        }

        private bool ModifyStudent(Student existingStudent)
        {
            bool success = false;
            BCS RequestDirector = new BCS();

            try
            {
                success = RequestDirector.ModifyStudent(existingStudent);
                ExistingStudent = new Student();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message.ToString();
            }
            
            return success;
        }
    }
}
