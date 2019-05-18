﻿using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;

namespace SIS.HTTP.Responses
{
    public class HttpResponse : IHttpResponse
    {
        public HttpResponse()
        {
            Headers = new HttpHeaderCollection();
            Content = new byte[0];
        }

        public HttpResponse(HttpResponseStatusCode statusCode) : this()
        {
            CoreValidator.ThrowIfNull(statusCode, nameof(statusCode));
            StatusCode = statusCode;
        }

        public HttpResponseStatusCode StatusCode { get; set; }

        public IHttpHeaderCollection Headers { get; }

        public byte[] Content { get; set; }

        public void AddHeader(HttpHeader header)
        {
            CoreValidator.ThrowIfNull(header, nameof(header));
            Headers.AddHeader(header);
        }

        public byte[] GetBytes()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result
                .Append($"{GlobalConstants.HttpOneProtocolFragment} {(int)StatusCode} {StatusCode.ToString()}")
                .Append(GlobalConstants.HTTPNewLine)
                .Append(Headers)
                .Append(GlobalConstants.HTTPNewLine);

            result.Append(GlobalConstants.HTTPNewLine);

            return result.ToString();
        }
    }
}
