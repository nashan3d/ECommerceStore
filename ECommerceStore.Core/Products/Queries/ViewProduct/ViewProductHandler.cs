using AutoMapper;
using ECommerceStore.Core.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly IECommerceDbContext _context;
        private IMapper _mapper;

        public ViewProductHandler(IECommerceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(ViewProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == request.id);

            if (product == null) return null;

            return _mapper.Map<ProductDto>(product);
        }
    }
}
