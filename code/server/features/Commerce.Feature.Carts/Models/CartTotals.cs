using Commerce.Core.Models;

namespace Commerce.Feature.Carts.Models
{
    public class CartTotals : BaseCommerceModel
    {
        public Money GrandTotal { get; set; }
        public Money LinesTotal { get; set; }
        public Money ShippingTotal { get; set; }
        public Money TaxTotal { get; set; }
        public Money DiscountsTotal { get; set; }
    }
}