using AutoMapper;
using ECommerceStore.Core.Products.Commands;
using ECommerceStore.Core.Products.Queries.ViewProduct;
using ECommerceStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceStore.Core.AutomapperProfiles
{
    public class ECommerceStoreAutomapperProfile : Profile
    {
        public ECommerceStoreAutomapperProfile()
        {
            CreateMap<CreateProductDto, Product>();
            CreateMap<ProductDto, Product>().ReverseMap();
        }
    }
}
