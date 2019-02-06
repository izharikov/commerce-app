using Commerce.Core.Models;

namespace Commerce.Feature.Carts.Models
{
    public class CartLineTotals : BaseCommerceModel
    {
        public Money UnitPrice { get; set; }
        public bool IsPurchasable { get; set; }
    }
}