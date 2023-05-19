using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dev.App.Data
{
    public class AppContext : IdentityDbContext
    {
        public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
        }
    }
}