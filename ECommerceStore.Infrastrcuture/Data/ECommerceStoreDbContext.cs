using ECommerceStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceStore.Infrastrcuture.Data
{
    public class ECommerceStoreDbContext : DbContext
    {
        public ECommerceStoreDbContext(DbContextOptions<ECommerceStoreDbContext> options):base(options)
        {          
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
