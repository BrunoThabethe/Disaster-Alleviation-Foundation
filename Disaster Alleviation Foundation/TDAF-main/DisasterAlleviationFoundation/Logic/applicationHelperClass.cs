using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterAlleviationFoundation.Logic
{
    public class applicationHelperClass
    {
        public Boolean UsernameAndPassMatch(String DbUsername, String DbPassword, String UserUsername, String UserPassword)
        {
            Boolean theyMatch;
            if (DbUsername.Equals(UserUsername) && DbPassword.Equals(UserPassword))
            {
                theyMatch = true;
            }
            else
            {
                theyMatch = false;
            }
            return theyMatch;
        }
        public Boolean isAmountLessThan(decimal DBTotal,int formValue)
        {
            Boolean isGood;
            if (formValue>DBTotal)
            {
                isGood = false;
            }
            else
            {
                isGood = true;
            }
            return isGood;

        }
        public Boolean isNotEmpty(String value1, String value2, String value3, String value4)
        {
            Boolean value;
            if (value1.Length == 0 || value2.Length == 0 || value3.Length == 0 || value4.Length == 0)
            {
                value = false;
            }
            else
            {
                value = true;
            }
            return value;
        }
    }
}
