using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pos.Startup))]
namespace Pos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
