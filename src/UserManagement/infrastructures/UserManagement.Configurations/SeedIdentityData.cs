using System;
using System.Linq;
using System.Threading.Tasks;
using Framework.Domain.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Domains;
using UserManagement.Domains.Customer;
using UserManagement.Persistance.Context;

namespace UserManagement.Configurations
{
    public static class SeedIdentityData
    {
        public static async Task InitializeUserDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var identityServerDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            identityServerDbContext.Database.Migrate();
            var roleManager = (RoleManager<Role>)serviceScope.ServiceProvider.GetService(typeof(RoleManager<Role>));
            var userManager = (UserManager<User>)serviceScope.ServiceProvider.GetService(typeof(UserManager<User>));
            var genderRepository = serviceScope.ServiceProvider.GetService<IGenderRepository>();

            var superuserRoleIsExist = await roleManager.FindByNameAsync("superuser");
            if (superuserRoleIsExist is null)
            {
                await roleManager.CreateAsync(new Role
                {
                    Name = "superuser"
                });
            }

            var gender = await genderRepository.GetByName("NotSpecified");
            if (gender is null)
            {
                identityServerDbContext.Genders.Add(new Gender("NotSpecified"));
                await identityServerDbContext.SaveChangesAsync();
            }
            
            var superUserIfExist = await userManager.FindByNameAsync("superuser");
            if (superUserIfExist is null)
            {
                var genderValue = await genderRepository.GetByName("NotSpecified");
                var user = new User
                {
                    FirstName = "superuser",
                    LastName = "superuser",
                    Email = "superuser@test.com",
                    UserName = "superuser",
                    PhoneNumber = "+111111111",
                    SubjectId = Guid.NewGuid(),
                    GenderId = genderValue.Id,
                    UserType = UserType.admin
                };
                
                var userIsRegister = await userManager.CreateAsync(user, "_AcRcSwXRTUaRhHe7r?z");
                if (userIsRegister.Succeeded)
                {
                    var superUser = await userManager.FindByNameAsync("superuser");
                    await userManager.AddToRoleAsync(superUser, "superuser");
                }
                    
            }


        }
    }
}