namespace EfInheritanceTpT.Entities;

// Derived Class representing an Article
class Article : Content
{
    public required string Content { get; set; } // Full content of the article
    public required string Summary { get; set; } // Brief summary of the article
}
