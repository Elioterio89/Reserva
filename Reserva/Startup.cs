using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Reserva.Startup))]
namespace Reserva
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
