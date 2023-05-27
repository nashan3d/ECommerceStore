using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceStore.Core.Products.Commands
{
    public class CreateProductDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }

        public string ProductCode { get; set; }

        public int StockQuantity { get; set; }
    }
}
