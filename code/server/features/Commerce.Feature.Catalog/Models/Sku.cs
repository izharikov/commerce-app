using System.Collections.Generic;
using Commerce.Core.Models;

namespace Commerce.Feature.Catalog.Models
{
    public class Sku : BaseCommerceModel
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public Money Price { get; set; }
        public IList<Image> Images { get; set; }
        public bool IsPurchasable { get; set; }
        public bool IsRequiredStockValidation { get; set; }
        public StockInformation StockInformation { get; set; }
    }
}