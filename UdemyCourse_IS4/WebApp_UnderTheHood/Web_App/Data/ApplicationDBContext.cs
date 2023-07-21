using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Web_App.Data
{
    public class ApplicationDBContext : IdentityDbContext
    {   
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        { }
        
    }

    
}
