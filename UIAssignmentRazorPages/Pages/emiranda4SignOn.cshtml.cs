using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UIAssignmentRazorPages.Pages
{
    public class emiranda4SignOnModel : PageModel
    {
        [BindProperty] 
        public string FormResult { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "User ID value is required and is a Valid User ID. 4 letters followed by 4 digits (e.g. BAIS9999)")]
        [StringLength(8),MinLength(8)]
        [RegularExpression("[a-zA-Z]{4}[0-9]{4}", ErrorMessage ="User ID Value must be 4 letters followed by 4 digits (e.g. BAIS9999)")]
        public string UserId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password value is required and is at least 6 characters long")]
        [MinLength(6)]
        public string Password { get; set; }

        public void OnPost()
        {
            FormResult = "You have successfully signed on your student account";
        }
    }
}
