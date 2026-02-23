using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace usingFilters.Filters
{
    public class OutOfRangeAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ArgumentOutOfRangeException)
            {
                var outputmessage = new ObjectResult(new { status = 400, message = context.Exception.Message }) { StatusCode = StatusCodes.Status400BadRequest };

                //Yukarıda genel Object Result nesnesini kullandık.
                var alternatif = new BadRequestObjectResult(new { status = 400, message = context.Exception.Message });

                context.Result = outputmessage;
            }
        }
    }
}
