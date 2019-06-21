using SIS.WebServer.Routing;

namespace SIS.WebServer
{
    public interface IMvcApplication
    {
        void Configure(IServerRoutingTable serverRoutingTable);
        void ConfigureServices(); //DI
    }
}
