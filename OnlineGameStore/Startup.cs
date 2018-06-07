using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineGameStore.Startup))]
namespace OnlineGameStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
