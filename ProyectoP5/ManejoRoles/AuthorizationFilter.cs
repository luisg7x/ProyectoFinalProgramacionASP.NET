using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProyectoP5.ManejoRoles
{
    public class AuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {

        public string rol { get; set; }

            public void OnAuthorization(AuthorizationContext filterContext)
            {

                try
                {
                    if (HttpContext.Current.Session["rol"].ToString().Equals(rol))
                    {

                        if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                            || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                        {
                            // Don't check for authorization as AllowAnonymous filter is applied to the action or controller
                            return;
                        }

                        // Check for authorization
                        if (HttpContext.Current.Session["Usuario"] == null)
                        {
                            filterContext.Result = filterContext.Result = new HttpUnauthorizedResult();
                            //filterContext.Result = new RedirectResult("~/Proyecto/Login");
                        }
                    }
                    else
                    {
                        filterContext.Result = filterContext.Result = new HttpUnauthorizedResult();
                        //filterContext.Result = new RedirectResult("~/Proyecto/Login");

                    }
                }
                catch (Exception)
                {
                    filterContext.Result = filterContext.Result = new HttpUnauthorizedResult();
                }  
           }
    
 
    }
}