using Commerce.Feature.Carts.Models;
using Commerce.Feature.Carts.Services.Request;

namespace Commerce.Feature.Carts.Services
{
    public interface ICartService
    {
        Cart GetCart(string cartId);
        Cart UpdateCartLine(Cart currentCart, UpdateCartLineRequest updateCartLineRequest);
        Cart ClearCart(Cart cart);
    }
}