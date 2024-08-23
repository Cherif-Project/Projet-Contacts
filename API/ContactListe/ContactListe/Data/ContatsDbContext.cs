using ContactListe.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ContactListe.Data
{
    public class ContatsDbContext : DbContext
    {
        public ContatsDbContext(DbContextOptions<ContatsDbContext>options):base(options)
        {
            
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
