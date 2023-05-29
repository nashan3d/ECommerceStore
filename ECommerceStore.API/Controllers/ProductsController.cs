using ECommerceStore.Core.Products.Commands;
using ECommerceStore.Core.Products.Commands.EditProduct;
using ECommerceStore.Core.Products.Queries.ViewProduct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto input)
        {
            var result = await Mediator.Send(new CreateProductCommand(input));

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> EditProduct(EditProductDto input)
        {
            var result = await Mediator.Send(new EditProductCommand(input));

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ViewProduct(Guid id)
        {
            var result = await Mediator.Send(new ViewProductQuery(id));

            return Ok(result);
        }
    }
}
