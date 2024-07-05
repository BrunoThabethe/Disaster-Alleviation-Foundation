using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DisasterAlleviationFoundation.Pages
{
    public class PurchaseGoodsModel : PageModel
    {
        public List<String> DisastersFrmDB = new List<string>();
        public Decimal Total, TotalDeductions;
        public List<String> DisastersFrmDBLocation = new List<string>();
        public List<int> Amounts = new List<int>();
        public List<String> descriptions = new List<String>();
        public List<String> disasters = new List<String>();
        public String Message ="";
        String conString = "Server=tcp:nigelserver.database.windows.net,1433;Initial Catalog=disaster;Persist Security Info=False;User ID=nigel;Password=85235180Tnc;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public void OnGet()
        {
            
             using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    String sql3 = "SELECT * FROM Disasters;";
                    using (SqlCommand command = new SqlCommand(sql3, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DisastersFrmDB.Add(reader.GetString(3));
                                DisastersFrmDBLocation.Add(reader.GetString(2));
                            }
                        }
                    }
                    connection.Close();
                    connection.Open();
                    String sql = "SELECT * FROM GoodsInventory;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TotalDeductions = TotalDeductions + reader.GetInt32(1);
                                Amounts.Add(reader.GetInt32(1));
                                descriptions.Add(reader.GetString(0));
                                disasters.Add(reader.GetString(2));

                            }
                        }
                    }
                    connection.Close();
                    connection.Open();
                    String sql1 = "SELECT * FROM MonetaryDonations;";
                    using (SqlCommand command = new SqlCommand(sql1, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Total = Total + Convert.ToDecimal(reader.GetDecimal(2));

                            }
                        }
                    }
                    Total = Total - TotalDeductions;
                }
           
        }

        public void OnPost()
        {
            try
            {
                String length = "";
                length = Request.Form["amountForGoods"];
                if (length.Length==0)
                {
                    Logic.Users.goodsIssue = "Insert all fields";
                    Message = Logic.Users.goodsIssue;
                    Response.Redirect("PurchaseGoods");
                    
                }
                else
                {
                    int amount = Convert.ToInt32(Request.Form["amountForGoods"]);
                    String Disaster = Request.Form["theSelection"];
                    String descrption = Request.Form["descrGoods"];

                 
                        using (SqlConnection connection = new SqlConnection(conString))
                        {
                            connection.Open();
                            String sql = "Insert into GoodsInventory values(@description, @amount,@disaster );";

                            using (SqlCommand command = new SqlCommand(sql, connection))
                            {
                                command.Parameters.AddWithValue("@amount", amount);
                                command.Parameters.AddWithValue("@description", descrption);
                                command.Parameters.AddWithValue("@disaster", Disaster);
                                command.ExecuteNonQuery();
                                Response.Redirect("PurchaseGoods");
                            }
                        }
                    
                    
                }
               
            }
            catch (Exception err)
            {

               throw;
            }
           
        }
    }
}
