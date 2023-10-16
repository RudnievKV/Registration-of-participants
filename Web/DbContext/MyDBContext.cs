using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Web.Models
{
    public class MyDBContext : DbContext
    {
        public MyDBContext() : base("Name = SqlServer")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<RegionalCenter> RegionalCenters { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegionalCenter>()
                .HasMany(x => x.Users)
                .WithRequired(x => x.RegionalCenter)
                .HasForeignKey(x => x.RegionalCenter_ID);
        }
    }
}