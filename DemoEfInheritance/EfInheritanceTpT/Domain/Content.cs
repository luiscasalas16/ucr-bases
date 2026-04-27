namespace EfInheritanceTpT.Entities;

// Base Class representing general content
abstract class Content
{
    public int ContentId { get; set; } // Primary Key
    public required string Title { get; set; } // Title of the content
    public required string Author { get; set; } // Author of the content
    public DateTime PublishedDate { get; set; } // Date when the content was published
    public ContentType ContentType { get; set; } // Type of the content (e.g., Article, Video, Image)
    public ContentStatus Status { get; set; } // Status of the content (Draft, Published, Archived)
}

// Enum for Content Status
enum ContentStatus
{
    Draft,
    Published,
    Archived,
}

// Enum for Content Type
enum ContentType
{
    Article,
    Video,
    Image,
}
