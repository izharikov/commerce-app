using System.Collections.Generic;
using Commerce.Core.Models;

namespace Commerce.Feature.Catalog.Models
{
    public class Category : BaseCommerceModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public IList<Product> Products { get; set; }
        public IList<string> ParentCategoryIds { get; set; }
        public IList<string> ChildCategoryIds { get; set; }
    }
}