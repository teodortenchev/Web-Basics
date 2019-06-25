﻿using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using System.Text;

namespace SIS.WebServer.Result
{
    public class JsonResult : ActionResult
    {
        public JsonResult(string jsonContent, HttpResponseStatusCode httpResponseStatusCode = HttpResponseStatusCode.Ok) : base(httpResponseStatusCode)
        {
            this.AddHeader(new HttpHeader(HttpHeader.ContentType, "application/json"));
            this.Content = Encoding.UTF8.GetBytes(jsonContent);
        }
    }
}
