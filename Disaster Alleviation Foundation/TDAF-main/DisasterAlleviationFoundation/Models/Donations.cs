using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationFoundation.Models
{
    public class Donations
    {
        [Key]
        public int DonationID { get; set; }
        public string DonorName { get; set; }
    }
}
