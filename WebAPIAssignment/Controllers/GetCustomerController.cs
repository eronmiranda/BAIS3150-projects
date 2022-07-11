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
    public class GetCustomerController : ControllerBase
    {
        private readonly ILogger<GetCustomerController> _logger;

        public GetCustomerController(ILogger<GetCustomerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public JsonResult GetCustomers()
        {
            SqlConnection NorthwindConnection = new SqlConnection();
            NorthwindConnection.ConnectionString = @"Data Source=dev1.baist.ca;Initial Catalog=Northwind;User Id=emiranda4; Password=EMiranda22!";

            SqlCommand NorthwindGetCustomers = CreateSqlCommand(NorthwindConnection, "emiranda4.GetNorthwindCustomers");

            NorthwindConnection.Open();

            SqlDataReader NorthwindGetCustomersReader = NorthwindGetCustomers.ExecuteReader();

            List<Customer> CustomerList = new List<Customer>();

            if (NorthwindGetCustomersReader.HasRows)
            {
                while (NorthwindGetCustomersReader.Read())
                {
                    Customer existingCustomer = new Customer
                    {
                        CustomerID = NorthwindGetCustomersReader["CustomerID"].ToString(),
                        CompanyName = NorthwindGetCustomersReader["CompanyName"].ToString(),
                        ContactName = NorthwindGetCustomersReader["ContactName"].ToString(),
                        ContactTitle = NorthwindGetCustomersReader["ContactTitle"].ToString(),
                        Address = NorthwindGetCustomersReader["Address"].ToString(),
                        City = NorthwindGetCustomersReader["City"].ToString(),
                        Region = NorthwindGetCustomersReader["Region"].ToString(),
                        PostalCode = NorthwindGetCustomersReader["PostalCode"].ToString(),
                        Phone = NorthwindGetCustomersReader["Phone"].ToString(),
                        Fax = NorthwindGetCustomersReader["Fax"].ToString()
                    };
                    CustomerList.Add(existingCustomer);
                }
            }

            NorthwindConnection.Close();

            return new JsonResult(CustomerList);
        }

        [HttpGet("{customerId}")]
        public JsonResult GetCustomer(string customerId)
        {
            SqlConnection NorthwindConnection = new SqlConnection();
            NorthwindConnection.ConnectionString = @"Data Source=dev1.baist.ca;Initial Catalog=Northwind;User Id=emiranda4; Password=EMiranda22!";

            SqlCommand NorthwindGetCustomer = CreateSqlCommand(NorthwindConnection, "emiranda4.GetNorthwindCustomer");
            SqlParameter customerIdParameter = CreateSqlParameter("@CustomerID", SqlDbType.NVarChar, customerId);
            NorthwindGetCustomer.Parameters.Add(customerIdParameter);
            NorthwindConnection.Open();

            SqlDataReader NorthwindGetCustomerReader = NorthwindGetCustomer.ExecuteReader();
            Customer existingCustomer = new Customer();
            if (NorthwindGetCustomerReader.Read())
            {
                existingCustomer = new Customer
                {
                    CustomerID = NorthwindGetCustomerReader["CustomerID"].ToString(),
                    CompanyName = NorthwindGetCustomerReader["CompanyName"].ToString(),
                    ContactName = NorthwindGetCustomerReader["ContactName"].ToString(),
                    ContactTitle = NorthwindGetCustomerReader["ContactTitle"].ToString(),
                    Address = NorthwindGetCustomerReader["Address"].ToString(),
                    City = NorthwindGetCustomerReader["City"].ToString(),
                    Region = NorthwindGetCustomerReader["Region"].ToString(),
                    PostalCode = NorthwindGetCustomerReader["PostalCode"].ToString(),
                    Phone = NorthwindGetCustomerReader["Phone"].ToString(),
                    Fax = NorthwindGetCustomerReader["Fax"].ToString()
                };
            }
            return new JsonResult(existingCustomer);
        }
        private static SqlCommand CreateSqlCommand(SqlConnection northwindConnection, string storedProcedureName)
        {
            SqlCommand northwindCommand = new SqlCommand();
            northwindCommand.CommandType = CommandType.StoredProcedure;
            northwindCommand.Connection = northwindConnection;
            northwindCommand.CommandText = storedProcedureName;
            return northwindCommand;
        }
        private static SqlParameter CreateSqlParameter(string parameterName, SqlDbType sqlDbType, string value)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = parameterName;
            parameter.SqlDbType = sqlDbType;
            parameter.Value = value;
            parameter.Direction = ParameterDirection.Input;
            return parameter;
        }
    }
}
