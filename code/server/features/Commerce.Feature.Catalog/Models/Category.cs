using System.Collections.Generic;

namespace Commerce.Feature.Catalog.Models
{
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public IList<Product> Products { get; set; }
        public IList<string> ParentCategoryIds { get; set; }
        public IList<string> ChildCategoryIds { get; set; }
    }
}