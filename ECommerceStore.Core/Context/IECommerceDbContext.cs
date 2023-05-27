
using ECommerceStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ECommerceStore.Core.Context
{
    public interface IECommerceDbContext
    {
        public DbSet<Product> Products { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
