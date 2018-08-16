using System.Linq;
using jce.Common.Slack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace jce.BusinessLayer.Exceptions
{
    public class JsonExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var result = new ObjectResult(new
            {
                code = 500,
                message = "A server error occurred.",
                detailedMessage = context.Exception.Message,
                sqlMessage = context.Exception.InnerException != null ? context.Exception.InnerException.Message : "no sql message"
               
            }) {StatusCode = 500};

            result = new ObjectResult(new
            {
                code = 400,
                message = "Bad Request",
                detailedMessage = context.Exception.Message
                })
                { StatusCode = 400 };

            context.Result = result;
            TestPostMessage(context, result);
        }
        void TestPostMessage(ExceptionContext context, ObjectResult msg)
        {
           
            string urlWithAccessToken = "https://hooks.slack.com/services/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";

            SlackClient client = new SlackClient(urlWithAccessToken);

            client.PostMessage(username: context.ActionDescriptor.AttributeRouteInfo.Template,
                text: msg.Value.ToString(),
                channel: "#dev");
        }

    }
}