using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace _ProyectoP5
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(
                new CookieAuthenticationOptions
                {

                    // YOUR LOGIN PATH
                    LoginPath = new PathString("/Inicio/Login")
                }
            );
        }
    }
}