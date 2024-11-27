using BP215Uniqlo.Models;
using System.ComponentModel.DataAnnotations;

namespace BP215Uniqlo.ViewModels.Slider
{
    public class SliderCreateVM
    {
        [MaxLength(32, ErrorMessage = "Title length must be less than 32"), Required(ErrorMessage = "Basliq yazmaq vacibdir")]
        public string Title { get; set; } = null!;
        [MaxLength(64), Required(ErrorMessage = "Alt basliq yazmaq vacibdir")]
        public string? Subtitle { get; set; }
        public string? Link { get; set; }
        [ Required(ErrorMessage = "sekil secmediniz")]
        public IFormFile File { get; set; }=null!;  
    }

}
