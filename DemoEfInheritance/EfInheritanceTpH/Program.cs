using EfInheritanceTpH.Entities;
using EfInheritanceTpH.Infraestructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EfInheritanceTpT
{
    static class Program
    {
        static void Main(string[] args)
        {
            InsertExampleData();
            ReadExampleData();

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        static void InsertExampleData()
        {
            using var context = new DemoDbContext(GetConnectionString());

            // Delete the old content to the context
            context.Contents.RemoveRange(context.Contents.ToList());

            // Save the changes to the database
            context.SaveChanges();

            // Create and seed Article content
            var article = new Article
            {
                Title = "Understanding EF Core TPT Inheritance",
                Author = "Pranaya Rout",
                PublishedDate = DateTime.Now,
                Status = ContentStatus.Published,
                Content = "This is a comprehensive guide on implementing TPT Inheritance...",
                Summary = "EF Core TPT Inheritance",
            };

            // Create and seed Video content
            var video = new Video
            {
                Title = "Learn EF Core with Videos",
                Author = "Rakesh Kumar",
                PublishedDate = DateTime.Now,
                Status = ContentStatus.Published,
                VideoUrl = "http://example.com/learn-efcore.mp4",
                Duration = 3600,
                Resolution = "1080p",
            };

            // Create and seed Image content
            var image = new Image
            {
                Title = "EF Core Infographic",
                Author = "Hina Sharma",
                PublishedDate = DateTime.Now,
                Status = ContentStatus.Published,
                ImageUrl = "http://example.com/efcore-infographic.jpg",
                Dimensions = "1920x1080",
            };

            // Add the new content to the context
            context.Contents.AddRange(article, video, image);

            // Save the changes to the database
            int recordsAdded = context.SaveChanges();

            // Output the result
            Console.WriteLine($"{recordsAdded} records were saved to the database.");

            // Confirm insertion
            Console.WriteLine("Content items have been successfully added.");
        }

        static void ReadExampleData()
        {
            using var context = new DemoDbContext(GetConnectionString());

            // The Contents DbSet will returns records from the base Contents table.
            // Since TPT creates separate tables for each derived class, this query will hit multiple tables(Joining with Articles, Videos, Images).
            // But we can only access the properties which are available in base Content type.
            var contents = context.Contents.ToList();

            //We loop through the contents and display the common properties such as ContentId, Title, ContentType, Author, and PublishedDate.
            Console.WriteLine("----- List of All Content -----");
            foreach (var content in contents)
            {
                Console.WriteLine(
                    $"Content ID: {content.ContentId}, Title: {content.Title}, Author: {content.Author}, Published: {content.PublishedDate.ToShortDateString()}"
                );
            }
        }

        static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true);

            var iConfiguration = builder.Build();

            string? connectionString = iConfiguration["ConnectionString"];

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Connection string not found.");

            return connectionString;
        }
    }
}
