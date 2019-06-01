using SIS.Demo.Controllers;
using SIS.HTTP.Enums;
using SIS.WebServer;
using SIS.WebServer.Routing;
using System;

namespace SIS.Demo
{
    public class Launcher
    {
        static void Main(string[] args)
        {
            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            serverRoutingTable.Add(
                HttpRequestMethod.Get,
                path: "/", 
                httpRequest => new HomeController().Home(httpRequest));

            serverRoutingTable.Add(HttpRequestMethod.Get,
                path: "/login",
                httpRequest => new HomeController().Login(httpRequest));

            Server server = new Server(8000, serverRoutingTable);

            server.Run();
        }
    }
}
