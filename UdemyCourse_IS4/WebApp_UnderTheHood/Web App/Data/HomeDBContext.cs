using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Identity.Client;

namespace Web_App.Data
{
    public class HomeDBContext : IdentityDbContext
    {   
        public HomeDBContext(DbContextOptions<HomeDBContext> options) : base(options)
        {
        
        
        }        
    }

    public class dbContextFactory : IDesignTimeDbContextFactory<HomeDBContext>
    {
        public HomeDBContext CreateDbContext(string[] args) 
        { 
        var optionsBuilder = new DbContextOptionsBuilder<HomeDBContext>();
        optionsBuilder.UseSqlServer("Home");
        
        return new HomeDBContext(optionsBuilder.Options);
        }


    }
    
}
