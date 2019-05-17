using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleHTTPServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHttpServer server = new HttpServer();
            server.Start();

        }
    }
}

public interface IHttpServer
{
    void Start();
    void Stop();
}

public class HttpServer : IHttpServer
{
    private bool isWorking;
    private readonly TcpListener tcpListener;
    private const string IP = "127.0.0.1";
    private const int PortNumber = 80;

    public HttpServer()
    {
        this.tcpListener = new TcpListener(IPAddress.Parse(IP), PortNumber);

    }


    public void Start()
    {
        isWorking = true;

        tcpListener.Start();

        Console.WriteLine($"Http server started on {IP}:{PortNumber}");


            var client = tcpListener.AcceptTcpClient();
            var stream = client.GetStream();
        while(isWorking)
        {
            var buffer = new byte[10240];
            var readBytes = stream.Read(buffer, 0, buffer.Length);
            var stringRequest = Encoding.UTF8.GetString(buffer, 0, readBytes);//Convert from byte[] to text
            Console.WriteLine(new string('=', 60));
            Console.WriteLine(stringRequest);
            //// Thread.Sleep(10000);
            ///
            /// var responseText = File.ReadAllText("index.html");


            string responseBody;
            string response;

            string[] stringRequestSplit = stringRequest.Split('/');

            if (stringRequestSplit[0].TrimEnd() == "POST")
            {
                string lastResponse = stringRequestSplit[stringRequestSplit.Length - 1].Split(new string[] { "\r\n\r\n" },
                               StringSplitOptions.RemoveEmptyEntries).Last();
                string[] firstSplitResponse = lastResponse.Split('&');

                Dictionary<string, string> dic = new Dictionary<string, string>();

                foreach (string splitted in firstSplitResponse)
                {
                    string[] secondSplit = splitted.Split('=');
                    dic.Add(secondSplit[0], secondSplit[1]);
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("<ul>");

                foreach (KeyValuePair<string, string> kp in dic)
                {
                    sb.Append("<li>" + kp.Key + " = " + kp.Value + "</li>");
                }

                sb.Append("</ul>");

                responseBody = sb.ToString().TrimEnd();

                response = "HTTP/1.0 200 OK" + Environment.NewLine +
                                  "Content-Type: text/html" + Environment.NewLine +
                                  "Location: localhost" + Environment.NewLine +
                                  // "Content-Disposition: attachment; filename=index.html" + NewLine +
                                  "Server: MyCustomServer/1.0" + Environment.NewLine +
                                  $"Content-Length: {responseBody.Length}" + Environment.NewLine + Environment.NewLine +
                                  responseBody;

                byte[] responseBytes = Encoding.UTF8.GetBytes(response);

                stream.Write(responseBytes, 0, responseBytes.Length);
            }

            else
            {
                responseBody = "<form method='post'><input type='text' name='tweet' placeholder='Enter tweet...' /><input name='name' /><input type='submit' /></form>";
                response = "HTTP/1.0 200 OK" + Environment.NewLine +
                                 "Content-Type: text/html" + Environment.NewLine +
                                 // "Location: https://google.com" + NewLine +
                                 // "Content-Disposition: attachment; filename=index.html" + NewLine +
                                 "Server: MyCustomServer/1.0" + Environment.NewLine +
                                 $"Content-Length: {responseBody.Length}" + Environment.NewLine + Environment.NewLine +
                                 responseBody;
                byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                stream.Write(responseBytes, 0, responseBytes.Length);
            }



            //Return something to the client
            ////var responseBytes = Encoding.UTF8.GetBytes(
            ////    "HTTP/1.0 200 OK" + Environment.NewLine
            ////    + "Content-Type: text/html" + Environment.NewLine
            ////    + "Content-Length: " + stringRequest.Length
            ////    + Environment.NewLine
            ////    + Environment.NewLine
            ////    + stringRequest);
            ////stream.Write(responseBytes);
        }
    }

    public void Stop()
    {
        isWorking = false;
    }
}