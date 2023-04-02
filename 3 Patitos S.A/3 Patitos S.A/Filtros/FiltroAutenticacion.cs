using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace _3_Patitos_S.A.Filtros
{
    public class FiltroAutenticacion : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string userJson = filterContext.HttpContext.Session.GetString("User");

            if (string.IsNullOrEmpty(userJson))
                filterContext.Result = new RedirectToActionResult("Login", "Acceso", null);
            

            else
                base.OnActionExecuting(filterContext);
        }
    }
}
