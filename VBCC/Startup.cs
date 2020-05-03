using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VBCC.Startup))]
namespace VBCC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
