using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StAbraamFamily.Startup))]
namespace StAbraamFamily
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
