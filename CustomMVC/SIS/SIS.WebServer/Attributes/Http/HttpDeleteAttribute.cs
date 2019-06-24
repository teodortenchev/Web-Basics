using SIS.HTTP.Enums;

namespace SIS.WebServer.Attributes.Http
{
    public class HttpDeleteAttribute : BaseHttpAttribute
    {
        public override HttpRequestMethod Method => HttpRequestMethod.Delete;
    }
}
