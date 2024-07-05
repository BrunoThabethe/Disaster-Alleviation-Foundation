using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DisasterAlleviationFoundation.Models;
using Microsoft.EntityFrameworkCore;

namespace DisasterAlleviationFoundation.Data
{
    public class DonationContext : DbContext
    {
        public DonationContext(DbContextOptions<DonationContext> options)
            : base(options)
        {
        }

        public DbSet<Donations> Donations { get; set; }
    }
}
