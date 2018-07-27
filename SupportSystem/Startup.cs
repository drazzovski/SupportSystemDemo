using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SupportSystem.Startup))]
namespace SupportSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
