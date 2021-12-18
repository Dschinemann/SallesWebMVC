
using Microsoft.EntityFrameworkCore;
using SallesWebMVC.Models;

namespace SallesWebMVC.Data
{
    public class SallesWebMVCContext : DbContext
    {
        public SallesWebMVCContext (DbContextOptions<SallesWebMVCContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }
        public DbSet<Seller> Seller { get; set; }

    }
}
