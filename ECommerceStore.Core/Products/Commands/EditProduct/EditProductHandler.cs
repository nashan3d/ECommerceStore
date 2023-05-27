using MediatR;
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
        public async Task<bool> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
