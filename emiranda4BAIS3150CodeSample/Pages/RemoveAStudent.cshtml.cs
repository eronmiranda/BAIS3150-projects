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
    public class RemoveAStudentModel : PageModel
    {
        public string ErrorMessage { get; set; }
        public string FormMessage { get; set; }

        [BindProperty]
        public bool Disable { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Student ID is required")]
        [RegularExpression("[a-zA-Z0-9]{1,10}", ErrorMessage = "Student ID can only have letters and numbers. Maximum of 10 characters. (e.g. johndoe1)")]
        public string StudentID { get; set; }

        public Student ExistingStudent { get; set; }


        public void OnGet()
        {
            Disable = true; 
        }

        public void OnPostSearch()
        {
            Disable = true;
            try
            {
                ExistingStudent = new BCS().FindStudent(StudentID);
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message.ToString();
                return;
            }
            Disable = false;
        }
        
        public void OnPostRemove()
        {
            if (RemoveStudent(StudentID))
            {
                ErrorMessage = "";
                FormMessage = "You have successfully deleted " + ExistingStudent.FirstName + " " + ExistingStudent.LastName + "'s Student Account.";
            }
        }

        public bool RemoveStudent(string studentID)
        {
            bool success = false;
            BCS RequestDirector = new BCS();
            try
            {
                ExistingStudent = RequestDirector.FindStudent(studentID);
                ErrorMessage = "";
                success = RequestDirector.RemoveStudent(studentID);
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message.ToString();
            }
            return success;
        }
    }
}
