using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(STIUApp.Startup))]
namespace STIUApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
