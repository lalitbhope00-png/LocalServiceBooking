using System.ComponentModel.DataAnnotations;

namespace LocalServiceBooking.Models;

public class Booking
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Service is required")]
    public string Service { get; set; } = "";

    [Required(ErrorMessage = "Please enter your name")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    public string CustomerName { get; set; } = "";

    [Required(ErrorMessage = "Please enter mobile number")]
    [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Mobile number must be exactly 10 digits")]
    public string Mobile { get; set; } = "";

    [Required(ErrorMessage = "Please enter address")]
    [StringLength(250, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 250 characters")]
    public string Address { get; set; } = "";

    [Required(ErrorMessage = "Please select booking date")]
    public DateTime BookingDate { get; set; }

    [Required(ErrorMessage = "Please select booking time")]
    public TimeSpan BookingTime { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }

    public string Status { get; set; } = "Pending";
    public int? ProviderId { get; set; }
}
