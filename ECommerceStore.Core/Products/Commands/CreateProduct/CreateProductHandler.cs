using AutoMapper;
using ECommerceStore.Core.Context;
using ECommerceStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceStore.Core.Products.Commands
{
    public record CreateProductCommand(CreateProductDto product) :IRequest<CreateProductDto>;
    public class CreateProductHandler : IRequestHandler<CreateProductCommand,CreateProductDto>
    {
        private readonly IECommerceDbContext _context;
        private IMapper _mapper;

        public CreateProductHandler(IECommerceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.product);

            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            request.product.Id = product.Id;

            return request.product;
        }
    }
}
