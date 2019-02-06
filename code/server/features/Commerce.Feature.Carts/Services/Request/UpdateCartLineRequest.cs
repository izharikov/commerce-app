namespace Commerce.Feature.Carts.Services.Request
{
    public class UpdateCartLineRequest
    {
        public string CartLineId { get; set; }
        public string ProductId { get; set; }
        public string SkuId { get; set; }
        public int ResultQty { get; set; }
    }
}