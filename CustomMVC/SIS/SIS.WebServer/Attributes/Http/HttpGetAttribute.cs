using SIS.HTTP.Enums;

namespace SIS.WebServer.Attributes
{
    public class HttpGetAttribute : BaseHttpAttribute
    {
        public override HttpRequestMethod Method => HttpRequestMethod.Get;
    }
}
