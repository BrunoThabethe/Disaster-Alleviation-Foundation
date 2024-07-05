using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DisasterAlleviationFoundation.Pages
{
    public class publicPageModel : PageModel
    {
        public List<Monetary> monetaryList = new List<Monetary>();
        public Monetary monetary = new Monetary();
        public String errorMessage = "Not assigned";
        public Decimal totalFunds = 0;

        public void OnGet()
        {
            if (Logic.Users.LoggedInCorrectly == false)
            {
                Logic.Users.Message = Logic.Users.NotLoggedInCorrectlyMessage;
              
            }
            else
            {
                try
                {

                    using (SqlConnection connection = new SqlConnection(Logic.Users.connectionString))
                    {
                        connection.Open();
                        String sql = "SELECT * FROM MonetaryDonations;";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Monetary monetary = new Monetary();
                                    monetary.DonationID = reader.GetInt32(0);
                                    monetary.Date = reader.GetDateTime(1);
                                    monetary.Amount = reader.GetDecimal(2);
                                    //adding up all the money from the database
                                    totalFunds = totalFunds + reader.GetDecimal(2);
                                    if (reader.GetInt32(4) == 0) { monetary.username = "Hidden"; }
                                    else
                                    {
                                        monetary.username = reader.GetString(3);
                                    }
                                    monetary.isAnonymous = reader.GetInt32(4);
                                    monetaryList.Add(monetary);

                                }
                            }

                        }
                    }
                }
                catch (Exception ee)
                {
                    errorMessage = ee.Message;
                }
            }
        }
        public void OnPost()
        {
            String value = Request.Form["inputByUser"];
            if (value.Equals("allocateMaterial"))
            {
                Response.Redirect("");
            }
            else if (value.Equals("allocateMaterial"))
            {
                Response.Redirect("");
            }
        }
    }

}
