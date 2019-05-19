using SIS.HTTP.Responses;
using SIS.HTTP.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Headers;

namespace SIS.WebServer.Results
{
    public class RedirectResult : HttpResponse
    {
        public RedirectResult(string location)
            : base(HttpResponseStatusCode.SeeOther)
        {
            Headers.AddHeader(new HttpHeader("Location", location));
        }
    }
}
