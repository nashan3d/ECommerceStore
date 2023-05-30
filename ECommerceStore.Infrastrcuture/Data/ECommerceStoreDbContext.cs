using ECommerceStore.Core.Context;
using ECommerceStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceStore.Infrastrcuture.Data
{
    public class ECommerceStoreDbContext : DbContext, IECommerceDbContext
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

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int records = 0;
            IDbContextTransaction tx = null;
            try
            {
                using (tx = base.Database.BeginTransaction())
                {
                    records = await base.SaveChangesAsync(cancellationToken);
                    tx.Commit();
                    return records;
                }
               
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach(var entry in ex.Entries) 
                {
                    if(entry.Entity is Product)
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = entry.GetDatabaseValues();

                        foreach (var property in proposedValues.Properties)
                        {
                            var propsedValue = proposedValues[property];
                            var databaseValue = databaseValues[property];
                        }

                        entry.OriginalValues.SetValues(databaseValues);
                    }
                    else
                    {
                        throw new NotSupportedException("Unable to save changes. Product Stock is Updated by another user");
                    }                   


                }

                throw ex;
            }
            catch (DbUpdateException ex)
            {
                tx.Rollback();
            }
            return records;
        }

    }
}
