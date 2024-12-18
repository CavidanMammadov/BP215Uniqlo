namespace BP215Uniqlo.ViewModels.Basket
{
    public class BasketVM
    {
        public IEnumerable<ProductBasketItemVM> Products { get; set; }
        public decimal  SubTotal { get; set; }
    }
}
