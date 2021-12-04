using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BusBooking.Startup))]
namespace BusBooking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
