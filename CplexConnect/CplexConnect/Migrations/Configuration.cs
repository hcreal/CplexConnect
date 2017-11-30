namespace CplexConnect.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CplexConnect.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CplexConnect.Models.ApplicationDbContext context)
        {

            ////Initialize user/role managers
            //var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));


            ////Create admin roles
            //var adminRoleName = "admin";
            //if (!RoleManager.RoleExists(adminRoleName))
            //{
            //    var createRoleResult = RoleManager.Create(new IdentityRole(adminRoleName));
            //}

            //var advisorRoleName = "advisor";
            //if (!RoleManager.RoleExists(advisorRoleName))
            //{
            //    var createRoleResult = RoleManager.Create(new IdentityRole(advisorRoleName));
            //}

            ////Create admin user
            //var adminUser = new ApplicationUser { UserName = "admin", Email = "hcreal@crimson.ua.edu" };
            //var createUserResult = UserManager.Create(adminUser, "Alabama2017");

            ////Add to admin role
            //if (createUserResult.Succeeded)
            //{
            //    var result = UserManager.AddToRole(adminUser.Id, adminRoleName);
            //}
            //context.Users.AddOrUpdate();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
