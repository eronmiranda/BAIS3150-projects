using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace emiranda4BAIS3150CodeSample.Pages
{
    public class CreateAProgramModel : PageModel
    {
        public string ErrorMessage { get; set; }
        public string FormMessage { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "ProgramCode is required")]
        [RegularExpression("[a-zA-Z]{1,10}", ErrorMessage = "Program Code can only have capital letters. Maximum of 10 letters. (e.g. BAIST)")]
        public string ProgramCode { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Description is required")]
        [RegularExpression("[a-zA-Z ]{1,}", ErrorMessage = "Description can only have letters.")]
        public string Description { get; set; }

        public Classes.Program NewProgram { get; set; }

        public void OnPost()
        {
            if(AddProgram(ProgramCode, Description))
            {
                FormMessage = "You have successfully created a Program";
            }
        }
        
        private bool AddProgram(string programCode, string description)
        {
            bool success = false;
            BCS RequestDirector = new BCS();

            try
            {
                success = RequestDirector.CreateProgram(programCode, description);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message.ToString();
            }

            return success;
        }
    }
}
