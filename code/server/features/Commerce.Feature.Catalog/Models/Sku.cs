using System.Collections.Generic;
using Commerce.Core.Models;

namespace Commerce.Feature.Catalog.Models
{
    public class Sku
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public Money Price { get; set; }
        public IList<Image> Images { get; set; }
        public StockInformation StockInformation { get; set; }
    }
}