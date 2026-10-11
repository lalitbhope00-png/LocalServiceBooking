using System.ComponentModel.DataAnnotations;

namespace LocalServiceBooking.Models;

public class ServiceProvider
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string ProviderName { get; set; } = "";

    [Required, StringLength(100)]
    public string Service { get; set; } = "";

    [Required, RegularExpression(@"^[0-9]{10}$")]
    public string Mobile { get; set; } = "";

    [Required, StringLength(250)]
    public string Address { get; set; } = "";

    [Required, Range(0, 60)]
    public int Experience { get; set; }

    public bool IsAvailable { get; set; } = true;
}
