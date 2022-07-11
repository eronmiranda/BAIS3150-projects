using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
namespace WebAPIAssignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IsItPrimeController : ControllerBase
    {
        private readonly ILogger<IsItPrimeController> _logger;

        public IsItPrimeController(ILogger<IsItPrimeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{primeNumber}")]
        public JsonResult Get(int primeNumber)
        {
            if (primeNumber == 1)
            {
                return new JsonResult(false);
            }
            for (int i = 2; i <= primeNumber / 2; i++)
            {
                if (primeNumber % i == 0)
                {
                    return new JsonResult(false);
                }
            }
            //Action result will be better
            return new JsonResult(true);
        }
    }
}
