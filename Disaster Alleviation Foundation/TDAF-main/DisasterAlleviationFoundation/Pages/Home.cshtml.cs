using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DisasterAlleviationFoundation.Pages
{
    
    public class HomeModel : PageModel
    {
        
        public List<Monetary> monetary = new List<Monetary>();
        public List<Goods> goods = new List<Goods>(); 
        public String errorMessage = "Not assigned";

        
        public void OnGet()
        {
            Logic.Users.LoggedInCorrectly = true;
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
                                List<Monetary> monetary2 = new List<Monetary>();
                                while (reader.Read())
                                {
                                    Monetary monetaryL2 = new Monetary();
                                    monetaryL2.DonationID = reader.GetInt32(0);
                                    monetaryL2.Date = reader.GetDateTime(1);
                                    monetaryL2.Amount = reader.GetDecimal(2);
                                    if (reader.GetInt32(4) == 0) { monetaryL2.username = "Hidden"; }
                                    else
                                    {
                                        monetaryL2.username = reader.GetString(3);
                                    }
                                    monetaryL2.isAnonymous = reader.GetInt32(4);
                                    monetary.Add(monetaryL2);
                                   
                                }
                               
                            }

                        }
                        connection.Close();
                        connection.Open();
                        String sql2 = "SELECT * FROM GoodsDonations;";
                        using (SqlCommand command = new SqlCommand(sql2, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {

                                while (reader.Read())
                                {
                                    Goods goodsLi = new Goods();

                                    goodsLi.DonationID = reader.GetInt32(0);
                                    goodsLi.DonationDate = reader.GetDateTime(1);
                                    goodsLi.NumberOfItems = reader.GetInt32(2);
                                    goodsLi.Category = reader.GetString(4);
                                    goodsLi.GoodsDescription = reader.GetString(5);
                                    goodsLi.isAnonymous = reader.GetInt32(6);
                                    if (reader.GetInt32(6) == 0) { goodsLi.Username = "Hidden"; } else { goodsLi.Username = reader.GetString(3); }
                                    goods.Add(goodsLi);

                                }
                            }
                        }
                    }

                }
                catch (Exception fur)
                {
                    errorMessage = "Error at : " + fur.Message;
                }
            }
            
        }
        public void OnPost()
        {
            String value = Request.Form["inputByUser"];
            if (value.Equals("allocateMaterial"))
            {
                Response.Redirect("");
            }else if (value.Equals("allocateMaterial"))
            {
                Response.Redirect("");
            }
        }
    }
    public class Monetary
    {
        public DateTime Date;
        public Decimal Amount;
        public int DonationID;
        public String username;
        public int isAnonymous;
    }
    public class goodsAssociated
    {
        public String Disaster;
        public String GoodsDescription;
    } 
    public class MoneyAssociated
    {
        public String Disaster;
        public int amount;
    }
    public class Goods
    {
        public int DonationID;
        public DateTime DonationDate;
        public int NumberOfItems;
        public String Username;
        public String Category;
        public String GoodsDescription;
        public int isAnonymous;
    }
    public class Disasters
    {
        public int ID;
        public String Title;
        public DateTime startDate;
        public DateTime endDate;
        public String location;
        public String status;

    } public class Disasters2
    {
        public int ID;
        public String Title;
        public DateTime startDate;
        public DateTime endDate;
        public String location;
        public String status;

    }
}
