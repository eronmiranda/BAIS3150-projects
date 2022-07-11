using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3150CoreWebAppDemo.Pages
{
    public class CalculatorModel : PageModel
    {
        [BindProperty]
        public int FirstNumber { get; set; }

        [BindProperty]
        public int SecondNumber { get; set; }

        [BindProperty]
        public string Result { get; set; }

        public void OnPost()
        {
            Result = (FirstNumber + SecondNumber).ToString();
        }
    }
}
