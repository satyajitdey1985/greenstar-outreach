using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(GreenStar.API.Startup))]
namespace GreenStar.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseCors(CorsOptions.AllowAll);

            HttpConfiguration config = new HttpConfiguration();

           // ConfigureAuth(app);

            // webapi is registered in the global.asax
            app.UseWebApi(config);

        }
    }
}
