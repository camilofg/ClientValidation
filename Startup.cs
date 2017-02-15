using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClientValidation.Startup))]
namespace ClientValidation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
