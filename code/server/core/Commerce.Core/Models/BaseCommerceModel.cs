using System.Collections.Generic;

namespace Commerce.Core.Models
{
    public abstract class BaseCommerceModel
    {
        public IList<CommerceInformation> CommerceInformations { get; set; }
    }
}