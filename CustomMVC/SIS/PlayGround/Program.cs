using SIS.HTTP.Common;
using System;
using System.Collections.Generic;

namespace PlayGround
{
    class Program
    {
        static void Main(string[] args)
        {
            string request = "GET home/index?search=nissan&category=SUV#hashtag HTTP/1.1"
                             + GlobalConstants.HTTPNewLine
                             + "Host: localhost:8000"
                             + GlobalConstants.HTTPNewLine
                             + "Accept: text/plain"
                             + GlobalConstants.HTTPNewLine
                             + "Authorization: Basic dGVzdHVzZXIwMTpuZXRjb29s"
                             + GlobalConstants.HTTPNewLine
                             + "Connection: keep-alive"
                             + GlobalConstants.HTTPNewLine
                                +"test";

         

            string[] requestPoo = request.Split(new[] { GlobalConstants.HTTPNewLine }, StringSplitOptions.None);

            var test = requestPoo[requestPoo.Length - 1];

            if(test != string.Empty)
            {
                Console.WriteLine("this is not an empty line");
            }
        }
    }
}
