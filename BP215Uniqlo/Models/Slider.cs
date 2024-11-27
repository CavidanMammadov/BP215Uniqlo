using System.ComponentModel.DataAnnotations;

namespace BP215Uniqlo.Models;

public class Slider:BaseEntity
{
    [MaxLength(32)]
    public string Title { get; set; } = null!;
    [MaxLength(64)]
    public string SubTitle { get; set; } = null!;
    public string? Link   { get; set; }
    public string ImageURl { get; set; } = null!;
}
