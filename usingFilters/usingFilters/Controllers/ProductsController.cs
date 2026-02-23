using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using usingFilters.Filters;
using usingFilters.Services;

namespace usingFilters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpDelete("{id:int}")]
        [IsExists]
        public IActionResult Delete(int id)
        {
            //[IsExists] attribute'ü olmasaydı:
            //if (!_productService.IsExists(id))
            //{
            //    return NotFound();
            //}

            return Ok();
        }
    }
}
