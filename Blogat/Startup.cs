using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Blogat.Startup))]
namespace Blogat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
