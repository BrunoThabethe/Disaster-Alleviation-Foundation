using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace DisasterAlleviationFoundation.Pages
{
    public class AllocateMonetaryModel : PageModel
    {

        public String errorMessage = "";
        public List<String> DisastersFrmDB = new List<string>();
        public List<String> DisastersFrmDBLocation = new List<string>();

        public List<int> Amounts = new List<int>();
        public List<String> locations = new List<String>();
        public Decimal Total;
        public int totalFunds;
        public Decimal TotalDeductions;
        String connectionString = "Server=tcp:nigelserver.database.windows.net,1433;Initial Catalog=disaster;Persist Security Info=False;User ID=nigel;Password=85235180Tnc;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public void OnGet()
        {

            //try
            //{
            using (SqlConnection connection = new SqlConnection(connectionString))
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
                String sql = "SELECT * FROM MonetaryDeductions;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TotalDeductions = TotalDeductions + reader.GetInt32(0);
                            Amounts.Add(reader.GetInt32(0));
                            locations.Add(reader.GetString(1));
                            totalFunds = totalFunds + reader.GetInt32(0);
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
        //  catch (Exception er)
        //  {
        //      errorMessage = er.Message;
        //}


    

        public void OnPost()
        {
            int amount = Convert.ToInt32(Request.Form["amount"]);
            String disaster = Request.Form["Selection"];
           
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "Insert into MonetaryDeductions values(@amount, @disaster);";
                    
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@amount", amount);
                        command.Parameters.AddWithValue("@disaster", disaster);
                        command.ExecuteNonQuery();
                        Response.Redirect("AllocateMonetary");

                    }
                }
           
           
        }
    }

}
