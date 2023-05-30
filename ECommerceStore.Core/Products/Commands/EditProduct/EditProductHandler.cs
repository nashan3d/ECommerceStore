using AutoMapper;
using ECommerceStore.Core.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceStore.Core.Products.Commands.EditProduct
{
    public record EditProductCommand(EditProductDto product):IRequest<bool>;

    public class EditProductHandler : IRequestHandler<EditProductCommand, bool>
    {
        private readonly IECommerceDbContext _context;        

        public EditProductHandler(IECommerceDbContext context)
        {
            _context = context;            
        }
        public async Task<bool> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var currentProduct = await _context.Products.SingleOrDefaultAsync(x => x.Id == request.product.Id);

                if (currentProduct == null) return false;

                currentProduct.ProductName = request.product.ProductName;
                currentProduct.StockQuantity = request.product.StockQuantity;

                _context.Products.Update(currentProduct);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {

                throw new Exception("Unable to save changes. The product detatails are updated by another user. Please get the latest record");
            }  

            return true;

        }
    }
}
