using System.Web.Mvc;

namespace cmspro.Areas.Officer
{
    public class OfficerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Officer";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Officer_default",
                "Officer/{controller}/{action}/{id}",
                new {Controller="Officer", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}