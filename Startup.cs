using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GenaroSilvestre.Startup))]
namespace GenaroSilvestre
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
