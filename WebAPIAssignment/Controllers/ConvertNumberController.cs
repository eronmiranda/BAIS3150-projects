using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebAPIAssignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConvertNumberController : ControllerBase
    {
        private readonly ILogger<ConvertNumberController> _logger;

        public ConvertNumberController(ILogger<ConvertNumberController> logger)
        {
            _logger = logger;
        }

        [HttpGet("toDecimal/{base2}", Name = "BinaryToDecimal")]
        public JsonResult GetBinaryToDecimal(int base2) // make it a long
        {
            int decimalValue = 0;
            int base1 = 1;
            Regex binaryPattern = new Regex(@"[01]+");

            if(binaryPattern.IsMatch(base2.ToString()))
            {
                while (base2 > 0)
                {
                    int remainder = base2 % 10;
                    base2 = base2 / 10;
                    decimalValue += remainder * base1;
                    base1 = base1 * 2;
                }
                return new JsonResult(decimalValue);
            }

            return new JsonResult("Error: Please enter a Base2 input.");
        }

        [HttpGet("toBinary/{base10}", Name = "DecimalToBinary")]
        public JsonResult GetDecimalToBinary(int base10)
        {
            string base2 = string.Empty;
            for (int i = 0; base10 > 0; i++)
            {
                base2 = base10 % 2 + base2;
                base10 = base10 / 2;
            }
            return new JsonResult(int.Parse(base2));
        }
    }
}
