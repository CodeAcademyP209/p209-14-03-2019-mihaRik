using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookReading.CustomFilterAttribute
{
    public class AdminAuthentication : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                return;
            }

            if (HttpContext.Current.Session["admin"] == null)
            {
                var returnUrl = filterContext.RequestContext.HttpContext.Request.RawUrl;

                filterContext.Result = new RedirectResult("/Admin/Account/Login?returnUrl=" + returnUrl);
            }
        }
    }
}