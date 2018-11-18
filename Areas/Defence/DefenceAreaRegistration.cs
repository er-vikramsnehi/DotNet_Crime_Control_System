using System.Web.Mvc;

namespace cmspro.Areas.Defence
{
    public class DefenceAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Defence";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Defence_default",
                "Defence/{controller}/{action}/{id}",
                new { Controller="Defence" ,action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}