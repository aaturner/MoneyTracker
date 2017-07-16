using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MoneyTracker.Startup))]
namespace MoneyTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
