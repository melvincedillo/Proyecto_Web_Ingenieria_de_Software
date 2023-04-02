using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoginEntity.Startup))]
namespace LoginEntity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
