using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(crmInmobiliario.Startup))]
namespace crmInmobiliario
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
