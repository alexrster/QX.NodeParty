using Microsoft.Owin;
using Owin;
using QX.NodeParty.Host.OwinSystemWeb;

[assembly: OwinStartup(typeof(Startup))]

namespace QX.NodeParty.Host.OwinSystemWeb
{
  public class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      //INodeBuilder nodeBuilder = null;
      //nodeBuilder.RegisterService(() => app);

      // Configure registered modules
    }
  }
}
