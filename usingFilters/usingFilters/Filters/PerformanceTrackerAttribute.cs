using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace usingFilters.Filters
{
    public class PerformanceTrackerAttribute : ActionFilterAttribute
    {
        private Stopwatch stopwatch;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            stopwatch = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            stopwatch.Stop();

            var elapsedMiliSeconds = stopwatch.ElapsedMilliseconds;
            if (context.Result != null)
            {
                //oluşan sonucu al:
                var okResult = (OkObjectResult)context.Result;
              
                var info = (ModelInfo)okResult.Value;
                if (info != null) {
                    info.LogInfo = $"{context.ActionDescriptor.DisplayName} action'u, {elapsedMiliSeconds} milisaniyede yanıt üretti";
                }
                context.Result = new OkObjectResult(info);
            }
        }
    }
}
