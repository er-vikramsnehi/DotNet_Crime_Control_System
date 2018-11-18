using System.Web.Mvc;

namespace cmspro.Areas.POLICE
{
    public class POLICEAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "POLICE";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "POLICE_default",
                "POLICE/{controller}/{action}/{id}",
                new {Controller="Police", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}