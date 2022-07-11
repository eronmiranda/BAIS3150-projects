using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UIAssignmentRazorPages.Pages
{
    public class emiranda4CategoriesModel : PageModel
    {
        public List<Category> CategoryList { get; set; }
        public List<string> CategoryColumn { get; set; }
        public void OnGet()
        {
            SqlConnection NWConnection = new SqlConnection();
            NWConnection.ConnectionString = UIAssignmentRazorPages.Startup.ConnectionString;

            NWConnection.Open();

            SqlCommand GetNorthWindCategoriesCommand = new SqlCommand
            {
                Connection = NWConnection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "emiranda4.GetNorthWindCategories"
            };

            SqlDataReader DataReader = GetNorthWindCategoriesCommand.ExecuteReader();

            CategoryColumn = new List<string>();

            for (int column = 0; column < DataReader.FieldCount; column++)
            {
                CategoryColumn.Add(DataReader.GetName(column).ToString());
            }

            CategoryList = new List<Category>();
            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    Category existingCategory = new Category
                    {
                        CategoryName = DataReader["CategoryName"].ToString(),
                        Description = DataReader["Description"].ToString(),
                        Picture = (byte[])DataReader["Picture"]
                    };
                    CategoryList.Add(existingCategory);
                }
            }
            NWConnection.Close();
        }
    }
}
