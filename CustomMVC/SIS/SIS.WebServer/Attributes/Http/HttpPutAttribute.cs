using SIS.HTTP.Enums;

namespace SIS.WebServer.Attributes.Http
{
    public class HttpPutAttribute : BaseHttpAttribute
    {
        public override HttpRequestMethod Method => HttpRequestMethod.Put;
    }
}
