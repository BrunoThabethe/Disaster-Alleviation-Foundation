using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterAlleviationFoundation.Logic
{
    public class Users
    {
        private static String Username;
        private static String Password;
        private static String userType;
        public static String ValueUsername;
        public static String ValuePassword;
        public static String connectionString =
            "Server=tcp:nigelserver.database.windows.net,1433;Initial Catalog=disaster;Persist Security Info=False;User ID=nigel;Password=85235180Tnc;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        
        public static void setUsername(String username)
        {
            Username = username;
        }
        public static void setPassword(String password)
        {
            Password = password;
        }public static void setUserType(String user)
        {
            userType = user;
        }
        public static String getUsername()
        {
            return Username;
        }
        public static String getPassword()
        {
            return Password;
        }
        
        public static String getUserType()
        {
            return userType;
        }
        public Users() { }
        public static String Message = "";
        public static Boolean LoggedInCorrectly = false;
        public static Boolean isAdmin = false;
        public static Boolean isInWelcomePage = true;
        public static Boolean isSignedIn = false;
        public static String NotLoggedInCorrectlyMessage = "Please Log In or create an Account";
        public static String goodsIssue = "Please Log In or create an Account";
        //Test values
        public static String DbDummyUsernameTwo="admin";
        public static String DbDummyPasswordTwo="123";
    }
}
