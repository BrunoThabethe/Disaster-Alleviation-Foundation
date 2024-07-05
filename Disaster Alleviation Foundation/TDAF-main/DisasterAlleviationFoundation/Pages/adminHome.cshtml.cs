using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DisasterAlleviationFoundation.Pages
{
    public class adminHomeModel : PageModel
    {
        public List<String> DisastersFrmDB = new List<string>();
        public List<String> GoodsFrmDB = new List<string>();
        public decimal totalFunds = 0;
        public int totalResources = 0;

        public void OnGet()
        {
            try
            {
                //Selecting All data from Disasters table in database
                String sql = "Select * from Disasters";
                using (SqlConnection connection = new SqlConnection(Logic.Users.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //adding disasters to the list
                                DisastersFrmDB.Add(reader.GetString(3));
                            }

                        }
                    }
                    //selecting from monetary
                    connection.Close();
                    connection.Open();
                    String sql2 = "SELECT * FROM MonetaryDonations;";
                    using (SqlCommand command = new SqlCommand(sql2, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                totalFunds = totalFunds + reader.GetDecimal(2);
                            }
                        }

                    }
                    //selecting from Goods Donation
                    connection.Close();
                    connection.Open();
                    String sql3 = "Select * from GoodsDonations";
                    using (SqlCommand command = new SqlCommand(sql3, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GoodsFrmDB.Add(reader.GetString(5));
                                totalResources = totalResources + reader.GetInt32(2);
                            }
                        }

                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void OnPost()
        {
            String value = Request.Form["inputByUser"];
            if (value.Equals("funds"))
            {
                Response.Redirect("AllocateMonetary");
            }
            else if (value.Equals("goods"))
            {
                Response.Redirect("AllocateGoods");
            }
            else if (value.Equals("purchase"))
            {
                Response.Redirect("PurchaseGoods");
            }
            else if (value.Equals("stats"))
            {
                Response.Redirect("Index");
            }
        }
    }
}
