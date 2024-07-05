using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DisasterAlleviationFoundation.Pages
{
    public class CreateCategoryModel : PageModel
    {
        public String errorMessage = "";
        public void OnGet()
        {
           
        }
        public void OnPost()
        {
            String cate = Request.Form["CategoryName"];
            try
            {
                using (SqlConnection connection = new SqlConnection(Logic.Users.connectionString))
                {
                    connection.Open();
                    String sql = "Insert into categories(category) values(@cate);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@cate", cate);

                        command.ExecuteNonQuery();
                        errorMessage = "Succussfully Added Category";
                    }
                }

            }
            catch (Exception er)
            {
                errorMessage = er.Message;
            }
        }
    }
}
