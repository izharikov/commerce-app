using System.Collections.Generic;
using Commerce.Core.Models;

namespace Commerce.Feature.Catalog.Models
{
    public class Product : BaseCommerceModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public IList<Sku> Skus { get; set; }
        public IList<Image> Images { get; set; }
        public IList<string> ParentCategoryIds { get; set; }
    }
}