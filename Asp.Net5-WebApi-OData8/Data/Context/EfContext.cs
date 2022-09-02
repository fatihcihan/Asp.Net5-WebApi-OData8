using Asp.Net5_WebApi_OData8.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Asp.Net5_WebApi_OData8.Data.Context
{
    public class EfContext : DbContext
    {
        public EfContext(DbContextOptions<EfContext> options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectsV13;Database=EfContext;User Id=sa; Password=1");
        }
        public DbSet<Gadget> Gadgets { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
