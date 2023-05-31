﻿using ECommerceStore.Core.Products.Commands;
using ECommerceStore.Core.Products.Commands.EditProduct;
using ECommerceStore.Core.Products.Queries.ViewProduct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
   
    public class ProductsController : BaseController
    {

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateProduct(CreateProductDto input)
        {
            var result = await Mediator.Send(new CreateProductCommand(input));

            return Ok(result);
        }

        [HttpPut]
        [AllowAnonymous]
        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> EditProduct(EditProductDto input)
        {
            var result = await Mediator.Send(new EditProductCommand(input));

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ViewProduct(Guid id)
        {
            //Guid id = new Guid();
            var result = await Mediator.Send(new ViewProductQuery(id));

            return Ok(result);
        }
    }
}
