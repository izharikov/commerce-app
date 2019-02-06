using Commerce.Core.Models;

namespace Commerce.Feature.Carts.Models
{
    public class CartLine : BaseCommerceModel
    {
        public string CartLineId { get; set; }
        public string ProductId { get; set; }
        public string SkuId { get; set; }
        public int Quantity { get; set; }
        public CartLineTotals Total { get; set; }
        public bool ShouldValidateStock { get; set; }
    }
}