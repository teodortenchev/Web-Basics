using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Headers;
using System;
using System.Collections.Generic;
using SIS.HTTP.Extensions;
using System.Linq;
using SIS.HTTP.Cookies;
using SIS.HTTP.Sessions;

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
            Cookies = new HttpCookieCollection();

            //TODO: Parse request data...
            ParseRequest(requestString);
        }

        public IHttpCookieCollection Cookies { get; private set; }
        public string Path { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        public IHttpSession Session { get; set; }

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

            ParseHeaders(ParsePlainRequestHeaders(splitRequestContent).ToArray());
            ParseCookies();

            ParseRequestParameters(splitRequestContent[splitRequestContent.Length - 1]);

        }

        private IEnumerable<string> ParsePlainRequestHeaders(string[] requestLines)
        {
            for (int i = 1; i < requestLines.Length - 1; i++)
            {
                if (!string.IsNullOrEmpty(requestLines[i]))
                {
                    yield return requestLines[i];
                }
            }
        }
        private void ParseRequestParameters(string requestBody)
        {
            ParseRequestQueryParameters();
            ParseRequestFormDataParameters(requestBody);

        }

        private void ParseRequestFormDataParameters(string requestBody)
        {
            if (requestBody != string.Empty)
            {
                var requestBodyParameteres = requestBody
                    .Split('&')
                    .ToArray();

                foreach (var param in requestBodyParameteres)
                {
                    var paramTokens = param.Split('=');
                    var paramKey = paramTokens[0];
                    var paramValue = paramTokens[1];

                    this.FormData.Add(paramKey, paramValue);
                }
            }
        }

        private void ParseRequestQueryParameters()
        {
            if (HasQueryString())
            {


                string[] urlTokens = Url.Split('?', '#');
                string queryString = urlTokens[1];

                IsValidRequestQueryString(queryString);

                string[] queryStringParameters = queryString.Split('&').ToArray();

                foreach (var parameter in queryStringParameters)
                {
                    string[] paramTokens = parameter.Split('=');
                    string key = paramTokens[0];
                    string value = paramTokens[1];

                    QueryData.Add(key, value);
                }
            }
        }
        private void IsValidRequestQueryString(string queryString)
        {
            CoreValidator.ThrowIfNullOrEmpty(queryString, nameof(queryString));

        }

        private void ParseCookies()
        {
            if (Headers.ContainsHeader(HttpHeader.CookieHeaderName))
            {
                HttpHeader cookieHeader = Headers.GetHeader(HttpHeader.CookieHeaderName);

                string[] cookies = cookieHeader.Value.Split("; ", StringSplitOptions.RemoveEmptyEntries);

                foreach (var cookie in cookies)
                {
                    string[] cookieKeyValues = cookie.Split("=", StringSplitOptions.RemoveEmptyEntries);
                    string key = cookieKeyValues[0];
                    string value = cookieKeyValues[1];

                    HttpCookie httpCookie = new HttpCookie(key, value);

                    Cookies.AddCookie(httpCookie);
                }
            }
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


                Headers.AddHeader(header);

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

        private bool HasQueryString()
        {
            return Url.Split('?').Length > 1;
        }


    }
}
