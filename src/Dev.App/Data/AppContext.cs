using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Dev.App.ViewModels;

namespace Dev.App.Data
{
    public class AppContext : IdentityDbContext
    {
        public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
        }
        public DbSet<Dev.App.ViewModels.AddressViewModel> AddressViewModel { get; set; }
    }
}