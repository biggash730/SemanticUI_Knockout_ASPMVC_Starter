using BackEnd.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BackEnd.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BackEnd.Models.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataContext context)
        {
            var userManager = new UserManager<MyUser>(new UserStore<MyUser>(context));
            userManager.UserValidator = new UserValidator<MyUser>(userManager) { AllowOnlyAlphanumericUserNames = false };
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //Create Roles if they do not exist
            if (!roleManager.RoleExists("Administrator"))
            {
                roleManager.Create(new IdentityRole("Administrator"));
            }


            //Create admin and super admin users with password=123456
            var admin = new MyUser
            {
                UserName = "administrator",
                FullName = "Francis Miah",
                Email = "francis@axoninfosystems.com",
                PhoneNumber = "0201234567",
                DateOfBirth = DateTime.Now.AddYears(-20),
                Updated = DateTime.Now,
                Created = DateTime.Now,
                IsActive = true
            };

            if (userManager.FindByName(admin.UserName) == null)
            {
                //Add User Admin to Role Admin
                var result = userManager.Create(admin, "password");
                if (result.Succeeded)
                {
                    userManager.AddToRole(admin.Id, "Administrator");
                }
            }

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
