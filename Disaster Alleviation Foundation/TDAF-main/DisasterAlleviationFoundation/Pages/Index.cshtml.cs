using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterAlleviationFoundation.Pages
{
    public class IndexModel : PageModel
    {

        public List<Monetary> monetary = new List<Monetary>();
        public List<Goods> goods = new List<Goods>();
        //essentials
        public List<Disasters2> disastersActiveList = new List<Disasters2>();
        public List<goodsAssociated> GoodsAllocatedList = new List<goodsAssociated>();
        public List<MoneyAssociated> MoneyAllocatedList = new List<MoneyAssociated>();

        public String errorMessage = "Not assigned";
        public String whats = "";
        public Decimal totalFunds = 0;
        public int totalResources;
        public int totalActive;


        public void OnGet()
        {
            Logic.Users.LoggedInCorrectly = true;
            Logic.Users.isInWelcomePage = true;

            try
            {
                using (SqlConnection connection = new SqlConnection(Logic.Users.connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM MonetaryDonations;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        String Store = "";
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<Monetary> monetary2 = new List<Monetary>();
                            while (reader.Read())
                            {
                                Monetary monetaryL2 = new Monetary();
                                monetaryL2.DonationID = reader.GetInt32(0);
                                monetaryL2.Date = reader.GetDateTime(1);
                                monetaryL2.Amount = reader.GetDecimal(2);
                                totalFunds = totalFunds + reader.GetDecimal(2);
                                if (reader.GetInt32(4) == 0) { monetaryL2.username = "Hidden"; }
                                else
                                {
                                    monetaryL2.username = reader.GetString(3);
                                }
                                monetaryL2.isAnonymous = reader.GetInt32(4);
                                monetary.Add(monetaryL2);

                            }
                            errorMessage = Store;
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
                                totalResources = totalResources + reader.GetInt32(2);

                            }
                        }
                    }
                    connection.Close();
                    connection.Open();
                    String sql3 = "SELECT * FROM GoodsDeduction;";
                    using (SqlCommand command = new SqlCommand(sql3, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {

                                goodsAssociated goods = new goodsAssociated();
                                goods.Disaster = reader.GetString(0);
                                goods.GoodsDescription = reader.GetString(1);
                                GoodsAllocatedList.Add(goods);


                            }
                        }
                    }
                    connection.Close();
                    connection.Open();
                    String sql4 = "SELECT * FROM Disasters where status ='Active';";
                    using (SqlCommand command = new SqlCommand(sql4, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Disasters2 disaster2 = new Disasters2();
                                disaster2.ID = reader.GetInt32(5);
                                disaster2.startDate = reader.GetDateTime(0);
                                disaster2.endDate = reader.GetDateTime(1);
                                disaster2.location = reader.GetString(2);
                                disaster2.Title = reader.GetString(3);
                                disaster2.status = reader.GetString(4);
                                disastersActiveList.Add(disaster2);
                                totalActive = totalActive + 1;
                            }
                        }
                    }
                    connection.Close();
                    connection.Open();
                    String sql5 = "SELECT * FROM MonetaryDeductions;";
                    using (SqlCommand command = new SqlCommand(sql5, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {

                                MoneyAssociated money = new MoneyAssociated();
                                money.Disaster = reader.GetString(1);
                                money.amount = reader.GetInt32(0);
                                MoneyAllocatedList.Add(money);
                                // totalResources = totalResources + reader.GetInt32(2);

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
