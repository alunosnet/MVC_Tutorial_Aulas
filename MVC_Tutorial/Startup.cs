using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_Tutorial.Startup))]
namespace MVC_Tutorial
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
