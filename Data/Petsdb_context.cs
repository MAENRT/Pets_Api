using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PetsAPI.Model;

namespace PetsAPI.Data
{
    public class Petsdb_context:DbContext
    {
        public Petsdb_context (DbContextOptions options) : base(options)
        {
        }

        public DbSet<Admin_Login> Admin_Login { get; set; }
        public DbSet<UserRegistration> UserRegistration { get; set; }
        public DbSet<Pets_detail> Pets_detail  { get; set; }
       

    }
}
