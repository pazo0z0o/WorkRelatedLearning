using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WevAPP.Data
{
    public class ApplicationDbContext : IdentityDbContext //Entity + Identity for the dbcontext. (User,Roles and other classes
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base (options) { 
        
        
        
        
        }
    }
}
