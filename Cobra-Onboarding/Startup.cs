using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cobra_Onboarding.Startup))]
namespace Cobra_Onboarding
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
