using SIS.HTTP.Enums;

namespace SIS.WebServer.Attributes
{
    public class HttpPostAttribute : BaseHttpAttribute
    {
        public override HttpRequestMethod Method => HttpRequestMethod.Post;
    }
}
