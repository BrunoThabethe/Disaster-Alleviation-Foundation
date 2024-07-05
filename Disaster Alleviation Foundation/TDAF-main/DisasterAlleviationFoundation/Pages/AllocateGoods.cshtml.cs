using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DisasterAlleviationFoundation.Pages
{
    public class AllocateGoodsModel : PageModel
    {
        public List<String> Description = new List<string>();
        public List<String> DisastersFrmDB = new List<string>();
        public List<String> DisastersDeducted = new List<string>();
        public List<String> goodsDeducted = new List<string>();
        public String errorMessage = "";
        
        public void OnGet()
        {
           
                using (SqlConnection connection = new SqlConnection(Logic.Users.connectionString))
                {
                    String sql = "Select * from GoodsDonations";
                    connection.Open();
                    using (SqlCommand commaned = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = commaned.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Description.Add(reader.GetString(5));

                            }
                        }
                    }
                    connection.Close();
                    connection.Open();
                    String sql3 = "SELECT * FROM Disasters;";
                    using (SqlCommand command = new SqlCommand(sql3, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DisastersFrmDB.Add(reader.GetString(3));

                            }
                        }
                    }
                    connection.Close();
                    connection.Open();
                    String sql1 = "SELECT * FROM GoodsDeduction;";
                    using (SqlCommand command = new SqlCommand(sql1, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DisastersDeducted.Add(reader.GetString(1));
                                goodsDeducted.Add(reader.GetString(0));

                            }
                        }
                    }
                }

            
           


        }
        public void OnPost()
        {
            String disaster = Request.Form["Selection"];
            String goods = Request.Form["description"];
            using (SqlConnection connection = new SqlConnection(Logic.Users.connectionString))
            {
                connection.Open();
                String sql = "Insert into GoodsDeduction values(@goodsDeducted, @Disaster);";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@goodsDeducted", goods);
                    command.Parameters.AddWithValue("@Disaster", disaster);
                    command.ExecuteNonQuery();

                    Response.Redirect("AllocateGoods");

                }
            }
        }
    }
}
