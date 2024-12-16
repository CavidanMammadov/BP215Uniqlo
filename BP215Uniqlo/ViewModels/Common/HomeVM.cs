using BP215Uniqlo.ViewModels.Basket;
using BP215Uniqlo.ViewModels.Product;
using BP215Uniqlo.ViewModels.Slider;

namespace BP215Uniqlo.ViewModels.Common
{
    public class HomeVM
    {

        public IEnumerable<SliderItemVM> Sliders { get; set; }
        public IEnumerable<ProductItemVm> Products { get; set; }
       
    }
}
