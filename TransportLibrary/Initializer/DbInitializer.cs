using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportLibrary.Authentication;
using TransportLibrary.Settings;

namespace TransportLibrary.Initializer
{
    public class DbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly TransportDbContext _db;

        public DbInitializer(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, TransportDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public void Initialize()
        {
            ApplyMigrations();
            AddAdminRole();
            AddDriverRole();
            AddAdminAccount();
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

        private void AddAdminAccount()
        {
            if (_userManager.FindByEmailAsync("admin@gmail.com").GetAwaiter().GetResult() == null)
            {
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    PhoneNumber = "0656904086",
                    EmailConfirmed = true,
                }, "admin123").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@gmail.com");

                _userManager.AddToRoleAsync(user, ApplicationRolesConstants.Admin).GetAwaiter().GetResult();
            }
        }

        private void AddDriverRole()
        {
            if (!_roleManager.RoleExistsAsync(ApplicationRolesConstants.Driver).GetAwaiter().GetResult())
                _roleManager.CreateAsync(new ApplicationRole(ApplicationRolesConstants.Driver)).GetAwaiter().GetResult();
        }

        private void AddAdminRole()
        {
            if (!_roleManager.RoleExistsAsync(ApplicationRolesConstants.Admin).GetAwaiter().GetResult())
                _roleManager.CreateAsync(new ApplicationRole(ApplicationRolesConstants.Admin)).GetAwaiter().GetResult();
        }
    }
}
