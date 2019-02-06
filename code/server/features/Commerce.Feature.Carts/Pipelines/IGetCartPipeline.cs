using Commerce.Core.Models;
using Commerce.Core.Pipelines;
using Commerce.Core.Pipelines.Attributes;
using Commerce.Feature.Carts.Models;

namespace Commerce.Feature.Carts.Pipelines
{
    [Pipeline(typeof(GetCartPipeline))]
    public interface IGetCartPipeline : IPipeline<string, Cart, CommerceContext>
    {
    }

    public class GetCartPipeline : DefaultPipeline<string, Cart, CommerceContext>, IGetCartPipeline
    {
    }
}