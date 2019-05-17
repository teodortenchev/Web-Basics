using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SoftUniHttpServer
{
    class Program1
    {
        static void Main1(string[] args)
        {
            const string NewLine = "\r\n";
            TcpListener tcpListener = new TcpListener(
                IPAddress.Loopback, 80);

            tcpListener.Start();
            // WebUtility.UrlDecode
            while (true)
            {
                // TCP / UDP
                TcpClient client = tcpListener.AcceptTcpClient();
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] requestBytes = new byte[100000];
                    // Thread.Sleep(10000);
                    int readBytes = stream.Read(requestBytes, 0, requestBytes.Length);
                    var stringRequest = Encoding.UTF8.GetString(requestBytes, 0, readBytes);
                    Console.WriteLine(new string('=', 70));
                    Console.WriteLine(stringRequest);
                    string responseBody;
                    string response;
                    string[] stringRequestSplitted = stringRequest.Split('/');
                    if (stringRequestSplitted[0].TrimEnd() == "POST")
                    {
                        string lastResponse = stringRequestSplitted[stringRequestSplitted.Length - 1].Split(new string[] { "\r\n\r\n" },
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
                        response = "HTTP/1.0 200 OK" + NewLine +
                                          "Content-Type: text/html" + NewLine +
                                          "Location: localhost" + NewLine +
                                          // "Content-Disposition: attachment; filename=index.html" + NewLine +
                                          "Server: MyCustomServer/1.0" + NewLine +
                                          $"Content-Length: {responseBody.Length}" + NewLine + NewLine +
                                          responseBody;
                        byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                        stream.Write(responseBytes, 0, responseBytes.Length);
                    }
                    else
                    {
                        responseBody = "<form method='post'><input type='text' name='tweet' placeholder='Enter tweet...' /><input name='name' /><input type='submit' /></form>";
                        response = "HTTP/1.0 200 OK" + NewLine +
                                         "Content-Type: text/html" + NewLine +
                                         // "Location: https://google.com" + NewLine +
                                         // "Content-Disposition: attachment; filename=index.html" + NewLine +
                                         "Server: MyCustomServer/1.0" + NewLine +
                                         $"Content-Length: {responseBody.Length}" + NewLine + NewLine +
                                         responseBody;
                        byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                        stream.Write(responseBytes, 0, responseBytes.Length);
                    }

                }
            }
        }
    }
}