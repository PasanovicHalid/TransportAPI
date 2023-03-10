using Domain.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
    public class DbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TransportDbContext _db;

        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, TransportDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public async Task Initialize()
        {
            ApplyMigrations();
            await AddSuperAdminRole();
            await AddAdminRole();
            await AddDriverRole();
            await AddAdminAccount();
        }

        private void ApplyMigrations()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task AddAdminAccount()
        {
            if (await _userManager.FindByEmailAsync("admin@gmail.com") == null)
            {
                await _userManager.CreateAsync(new IdentityUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    PhoneNumber = "0656904086",
                    EmailConfirmed = true,
                }, "admin123");

                IdentityUser user = await _userManager.FindByEmailAsync("admin@gmail.com");

                await _userManager.AddToRoleAsync(user, ApplicationRolesConstants.SuperAdmin);
            }
        }

        private async Task AddDriverRole()
        {
            if (!await _roleManager.RoleExistsAsync(ApplicationRolesConstants.Driver))
                await _roleManager.CreateAsync(new IdentityRole(ApplicationRolesConstants.Driver));
        }

        private async Task AddAdminRole()
        {
            if (!await _roleManager.RoleExistsAsync(ApplicationRolesConstants.Admin))
                await _roleManager.CreateAsync(new IdentityRole(ApplicationRolesConstants.Admin));
        }

        private async Task AddSuperAdminRole()
        {
            if (!await _roleManager.RoleExistsAsync(ApplicationRolesConstants.SuperAdmin))
                await _roleManager.CreateAsync(new IdentityRole(ApplicationRolesConstants.SuperAdmin));
        }
    }
}
