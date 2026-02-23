using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using usingFilters.Services;

namespace usingFilters.Filters
{
    public class IsExistsFilter : IActionFilter
    {
        private readonly IProductService productService;

        public IsExistsFilter(IProductService productService)
        {
            this.productService = productService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //eğer action'un id isimli bir parametresi varsa:
            if (context.ActionArguments.ContainsKey("id"))
            {
                //bu parametre int ise
                if (context.ActionArguments["id"] is int id)
                {
                    if (!productService.IsExists(id))
                    {
                        context.Result = new NotFoundObjectResult(new { message = $"{id}  id'li ürün db'de bulunamadı" });
                    }
                }
            }

        }
    }
}
