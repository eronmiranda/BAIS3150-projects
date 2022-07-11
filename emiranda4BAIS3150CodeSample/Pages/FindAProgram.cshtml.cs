using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace emiranda4BAIS3150CodeSample.Pages
{
    public class FindAProgramModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "ProgramCode is required")]
        [RegularExpression("[a-zA-Z]{1,10}", ErrorMessage = "Program Code can only have capital letters. Maximum of 10 letters. (e.g. BAIST)")]
        public string ProgramCode { get; set; }

        public string ErrorMessage { get; set; }

        public Classes.Program ExistingProgram { get; set; }

        public void OnPostSearch()
        {
            try
            {
                ExistingProgram = new BCS().FindProgram(ProgramCode);
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message.ToString();
            }
        }
    }
}
