using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterAlleviationFoundation.Logic
{
    public class MonetaryAllocation
    {
        private static Decimal TotalAvailable;
        //sets the total amount of funds
        public static void setTotal(Decimal username)
        {
            TotalAvailable = username;
        }
        //retrieves the total amount of funds
        public static Decimal getTotal()
        {
            return TotalAvailable;
        }
    }
}
