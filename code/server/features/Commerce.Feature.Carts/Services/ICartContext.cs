using Commerce.Feature.Carts.Models;

namespace Commerce.Feature.Carts.Services
{
    public interface ICartContext
    {
        Cart CurrentCart { get; }
    }
}