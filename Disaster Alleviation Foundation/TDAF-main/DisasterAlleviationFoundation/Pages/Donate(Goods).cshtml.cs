using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DisasterAlleviationFoundation.Pages
{

    public class Donate_Goods_Model : PageModel
    {
        public List<Goods> goodsLis = new List<Goods>();
        public List<String> categoriesFrmDB = new List<string>();
        public Goods goods = new Goods();
        public String Message = "";
        public int totalResources = 0;
        public void OnGet()
        {
            if (Logic.Users.LoggedInCorrectly == false)
            {
                Logic.Users.Message = Logic.Users.NotLoggedInCorrectlyMessage;
                Response.Redirect("Login");
            }
            else
            {

                try
                {


                    using (SqlConnection connection = new SqlConnection(Logic.Users.connectionString))
                    {
                        connection.Open();
                        String sql = "Select * from categories";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    categoriesFrmDB.Add(reader.GetString(0));
                                }

                            }
                        }

                        connection.Close();
                        connection.Open();
                        String sql2 = "Select * from GoodsDonations";
                        using (SqlCommand command = new SqlCommand(sql2, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Goods goods = new Goods();
                                    goods.DonationID = reader.GetInt32(0);
                                    goods.DonationDate = reader.GetDateTime(1);
                                    goods.NumberOfItems = reader.GetInt32(2);
                                    goods.Category = reader.GetString(4);
                                    goods.GoodsDescription = reader.GetString(5);
                                    goods.isAnonymous = reader.GetInt32(6);
                                    if (reader.GetInt32(6) == 0) { goods.Username = "Hidden"; } else { goods.Username = reader.GetString(3); }
                                    goodsLis.Add(goods);
                                    totalResources = totalResources + reader.GetInt32(2);
                                }

                            }
                        }
                    }
                }
                catch (Exception rf)
                {
                    Message = "From OnGet: " + rf.Message;
                }

            }

        }
        public void OnPost()
        {
            try
            {

                String amount = Request.Form["amount"];
                String numItems = Request.Form["NumItems"];
                String confirm = Request.Form["theSelection"];
                String descr = Request.Form["Description"];



                using (SqlConnection connection = new SqlConnection(Logic.Users.connectionString))
                {
                    connection.Open();
                    String sql = "Insert into GoodsDonations(DonationDate,NumberOfItems,Username,Category,GoodsDescription,isAnonymous)" +
                        "values(@date,@itemss,@Username2,@category,@descr,@anonymous); ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        if (confirm.Equals("Yes"))
                        {
                            command.Parameters.AddWithValue("@date", System.DateTime.Today);
                            command.Parameters.AddWithValue("@itemss", numItems);
                            command.Parameters.AddWithValue("@Username2", Logic.Users.getUsername());
                            command.Parameters.AddWithValue("@category", confirm);
                            command.Parameters.AddWithValue("@descr", descr);
                            command.Parameters.AddWithValue("@anonymous", 0);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@date", System.DateTime.Today);
                            command.Parameters.AddWithValue("@itemss", numItems);
                            command.Parameters.AddWithValue("@Username2", Logic.Users.getUsername());
                            command.Parameters.AddWithValue("@category", confirm);
                            command.Parameters.AddWithValue("@descr", descr);
                            command.Parameters.AddWithValue("@anonymous", 1);
                        }
                        command.ExecuteNonQuery();
                    }


                }
                Response.Redirect("Donate(Goods)");


            }
            catch (Exception gift)
            {
                Message = gift.Message;
                throw;
            }
        }
    }
}


