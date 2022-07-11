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
    public class FindAStudentModel : PageModel
    {
        public string ErrorMessage { get; set; }

        [BindProperty]
        [Required(ErrorMessage ="Student ID is required")]
        [RegularExpression("[a-zA-Z0-9]{1,10}", ErrorMessage = "Student ID can only have letters and numbers. Maximum of 10 characters. (e.g. johndoe1)")]
        public string StudentID { get; set; }

        public Student ExistingStudent { get; set; }

        public void OnPostSearch()
        {
            try
            {
                ExistingStudent = new BCS().FindStudent(StudentID);
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message.ToString();
            }
        }
    }
}
