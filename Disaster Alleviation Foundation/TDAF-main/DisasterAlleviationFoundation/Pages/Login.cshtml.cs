using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DisasterAlleviationFoundation.Pages
{
    public class LoginModel : PageModel
    {
        public String loginMessage = "";
       
        public void OnGet()
        {
            Logic.Users.isInWelcomePage = false;
        }

        public void OnPost()
        {
            String Username = Request.Form["username"];
            String password = Request.Form["password"];
            String userType = Request.Form["theSelection"];
            LoginUser(Username,password,userType);
        }
        public Boolean LoginUser(String Username, String Pass, String userType) {
            Boolean Value = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(Logic.Users.connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Users;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Logic.applicationHelperClass ahc = new Logic.applicationHelperClass();
                                //if combo box value is Admin
                                if (userType.Equals("Admin"))
                                {
                                    //zero means false
                                    if (reader.GetString(4).Equals("Admin"))
                                    {
                                        
                                        if (ahc.UsernameAndPassMatch(reader.GetString(2), reader.GetString(3), Username, Pass))
                                        {
                                            ;
                                            loginMessage = "Successfully logged in";
                                            Logic.Users.setUsername(Username);
                                            Logic.Users.setPassword(Pass);
                                            Logic.Users.setUserType(userType);
                                            Logic.Users.ValueUsername = Username;
                                            Logic.Users.ValuePassword = Pass;
                                            //setting loggedInCorrectly to true to confirm user logged in
                                            Logic.Users.LoggedInCorrectly = true;
                                            Logic.Users.isAdmin = true;
                                            //redirecting to login page
                                            Response.Redirect("adminHome");
                                            Value = true;
                                        }

                                    }
                                   
                                }
                                else
                                {
                                    if (ahc.UsernameAndPassMatch(reader.GetString(2), reader.GetString(3), Username, Pass) && reader.GetString(4).Equals("Client"))
                                    {
                                        
                                        loginMessage = "Successfully logged in";
                                        Logic.Users.setUsername(Username);
                                        Logic.Users.setPassword(Pass);
                                        Logic.Users.setUserType(userType);
                                        Logic.Users.ValueUsername = Username;
                                        Logic.Users.ValuePassword = Pass;
                                        //setting loggedInCorrectly to true to confirm user logged in
                                        Logic.Users.LoggedInCorrectly = true;
                                        Logic.Users.isAdmin = false;
                                        //redirecting to login page
                                        Response.Redirect("Home");
                                        Value = true;

                                    }

                                }



                            }
                        }

                      
                    }
                }

            }
            catch (Exception fur)
            {
                Logic.Users.Message = fur.Message;
                Console.WriteLine("{0}", fur.Message);
            }
            return Value;
        }
    }
   

}
