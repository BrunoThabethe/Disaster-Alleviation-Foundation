using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DisasterAlleviationFoundation.Pages
{
    public class CreateDisasterModel : PageModel
    {
        public String errorMessage = "";
        public void OnGet()
        {
            if (Logic.Users.LoggedInCorrectly == false)
            {
                Logic.Users.Message = Logic.Users.NotLoggedInCorrectlyMessage;
                Response.Redirect("Login");
            }
        }
        public void OnPost()
        {
            String startDate = Request.Form["startDate"];
            String endDate = Request.Form["endDate"];
            String location = Request.Form["location"];
            String title = Request.Form["title"];
            String status = Request.Form["theStatus"];
            try
            {
                using (SqlConnection connection = new SqlConnection(Logic.Users.connectionString))
                {
                    connection.Open();
                    String sql = "Insert into Disasters(Title,startDate,endDate,location,status) values(@Title,@SDate,@EDate,@Location,@status);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Title", title);
                        command.Parameters.AddWithValue("@SDate", startDate);
                        command.Parameters.AddWithValue("@Location", location);
                        command.Parameters.AddWithValue("@EDate", endDate);
                        command.Parameters.AddWithValue("@status", status);


                        command.ExecuteNonQuery();
                        errorMessage = "Succussfully Added " + title + " to Disasters";
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
