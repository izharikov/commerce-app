using System.Collections.Generic;
using Commerce.Core.Models;
using Commerce.Core.Pipelines;
using Commerce.Core.Pipelines.Attributes;
using Commerce.Feature.Carts.Models;

namespace Commerce.Feature.Carts.Pipelines.Blocks
{
    #if DEBUG
    
    [PipelineBlock(Order = 0, Pipeline = typeof(IGetCartPipeline))]
    public class GenerateCartPipelineBlock : DefaultPipelineBlock<string, Cart, CommerceContext>
    {
        public override Cart Run(string cartId, CommerceContext context)
        {
            return new Cart()
            {
                CartId = cartId,
                CartLines = new List<CartLine>()
                {
                    new CartLine()
                    {
                        
                    }
                },
                CartTotals = new CartTotals()
                {
                    ShippingTotal = new Money(1),
                    TaxTotal = new Money(1),
                    DiscountsTotal = new Money(1)
                }
            };
        }
    }
    
    #endif
}