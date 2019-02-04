using System.Collections.Generic;

namespace Commerce.Core.Models
{
    public class Image
    {
        public string DefaultSrc { get; set; }
        public IDictionary<int, string> Sources { get; set; }
    }
}