namespace EfInheritanceTpC.Entities;

// Derived Class representing an Image
class Image : Content
{
    public required string ImageUrl { get; set; } // URL of the image
    public required string Dimensions { get; set; } // Dimensions of the image (e.g., 1920x1080)
}
