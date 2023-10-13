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
        public void AddDataIfEmpty()
        {
            var isEmpty = RegionalCenters.Any();
            if (!isEmpty)
                return;
            var newRegionalCenter1 = new RegionalCenter()
            {
                Name = "Вінниця"
            };
            var newRegionalCenter2 = new RegionalCenter()
            {
                Name = "Дніпро"
            };
            var newRegionalCenter3 = new RegionalCenter()
            {
                Name = "Донецьк"
            };
            var newRegionalCenter4 = new RegionalCenter()
            {
                Name = "Житомир"
            };
            var newRegionalCenter5 = new RegionalCenter()
            {
                Name = "Запоріжжя"
            };
            RegionalCenters.Add(newRegionalCenter1);
            RegionalCenters.Add(newRegionalCenter2);
            RegionalCenters.Add(newRegionalCenter3);
            RegionalCenters.Add(newRegionalCenter4);
            RegionalCenters.Add(newRegionalCenter5);
            SaveChanges();
        }
    }
}