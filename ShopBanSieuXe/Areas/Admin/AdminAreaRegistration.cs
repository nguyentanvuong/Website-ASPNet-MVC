using System.Web.Mvc;

namespace ShopBanSieuXe.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                 "admin_login",
                 "admin/login",
                 new { Controller = "Auth", action = "Login", id = UrlParameter.Optional }
             );
            context.MapRoute(
                 "admin_logout",
                 "admin/logout",
                 new { Controller = "Auth", action = "Logout", id = UrlParameter.Optional }
             );
            context.MapRoute(
                "admin_default",
                "admin/{controller}/{action}/{id}",
                new { Controller = "Dashboard", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}