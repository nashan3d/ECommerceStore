﻿using ECommerceStore.Core.Context;
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

        public CreateProductHandler(IECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<CreateProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}