using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceStore.Core.Products.Queries.ViewProduct
{
    public record ViewProductQuery(Guid id):IRequest<ProductDto>;
    public class ViewProductHandler : IRequestHandler<ViewProductQuery, ProductDto>
    {
        public async Task<ProductDto> Handle(ViewProductQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
