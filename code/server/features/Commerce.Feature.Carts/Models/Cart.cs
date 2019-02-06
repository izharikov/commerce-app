using System.Collections.Generic;
using Commerce.Core.Models;

namespace Commerce.Feature.Carts.Models
{
    public class Cart : BaseCommerceModel
    {
        public IList<CartLine> CartLines { get; set; }
        public string CartId { get; set; }
        public bool IsPersisted { get; set; }
        public CartTotals CartTotals { get; set; }
    }
}