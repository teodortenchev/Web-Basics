using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Headers;
using System;
using System.Collections.Generic;
using SIS.HTTP.Extensions;
using System.Linq;

namespace SIS.HTTP.Requests
{
    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));

            FormData = new Dictionary<string, object>();
            QueryData = new Dictionary<string, object>();
            Headers = new HttpHeaderCollection();

            //TODO: Parse request data...
            ParseRequest(requestString);
        }

        public string Path { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        private void ParseRequest(string requestString)
        {
            string[] splitRequestContent = requestString.Split(new[] { GlobalConstants.HTTPNewLine }, StringSplitOptions.None);

            string[] requestLine = splitRequestContent[0].Trim().
                Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!IsValidRequestLine(requestLine))
            {
                throw new BadRequestException();
            }

            ParseRequestMethod(requestLine);
            ParseRequestUrl(requestLine);
            ParseRequestPath();

            ParseHeaders(splitRequestContent.Skip(1).ToArray());
        }

        private void ParseHeaders(string[] headers)
        {
            string hostHeader = headers[0].Split(' ')[0];

            if (hostHeader != GlobalConstants.HostHederKey + ":")
            {
                throw new BadRequestException();
            }

            foreach (var line in headers)
            {
                if (line == GlobalConstants.HTTPNewLine)
                {
                    break;
                }

                string[] headerElements = line.Split(": ", StringSplitOptions.RemoveEmptyEntries);
                string key = headerElements[0];
                string value = headerElements[1];

                HttpHeader header = new HttpHeader(key, value);

                //TODO: Does this check make sense? See if this causes any issues later on

                if (!Headers.ContainsHeader(key))
                {
                    Headers.AddHeader(header);
                }
            }

        }

        private void ParseRequestMethod(string[] requestLine)
        {
            string requestType = requestLine[0].Capitalize();

            if (Enum.TryParse<HttpRequestMethod>(requestType, out var requestMethod))
            {
                RequestMethod = requestMethod;
            }
            else
            {
                throw new BadRequestException();
            }
        }

        private bool IsValidRequestLine(string[] requestLine)
        {
            if (requestLine.Length != 3)
            {
                return false;
            }

            return requestLine[2] == "HTTP/1.1";
        }

        private void ParseRequestUrl(string[] requestLine)
        {
            Url = requestLine[1];
        }

        private void ParseRequestPath()
        {
            string[] pathParams = Url.Split('?', StringSplitOptions.RemoveEmptyEntries);
            Path = pathParams[0];
        }

        //    private void ParseQueryParameters(string url)
        //    {

        //    }
        //    private bool IsValidRequestQueryString(string[] queryParameters)
        //    {
        //        return new NotImplementedException();
        //    }
    }
}
