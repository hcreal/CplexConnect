using CplexConnect.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CplexConnect
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public class CustomDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {

                //Initialize user/role managers
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));


                //Create admin roles
                var adminRoleName = "admin";
                if (!RoleManager.RoleExists(adminRoleName))
                {
                    var createRoleResult = RoleManager.Create(new IdentityRole(adminRoleName));
                }

                var advisorRoleName = "advisor";
                if (!RoleManager.RoleExists(advisorRoleName))
                {
                    var createRoleResult = RoleManager.Create(new IdentityRole(advisorRoleName));
                }

                //Create admin user
                var adminUser = new ApplicationUser { UserName = "admin", Email = "hcreal@crimson.ua.edu" };
                var createUserResult = UserManager.Create(adminUser, "Alabama2017");

                //Add to admin role
                if (createUserResult.Succeeded)
                {
                    var result = UserManager.AddToRole(adminUser.Id, adminRoleName);
                }

                base.Seed(context);
            }
        }

    }
}
