namespace EfInheritanceTpT.Entities;

// Derived Class representing a Video
class Video : Content
{
    public required string VideoUrl { get; set; } // URL of the video
    public int Duration { get; set; } // Duration of the video in seconds
    public required string Resolution { get; set; } // Video resolution (e.g., 1080p)
}
