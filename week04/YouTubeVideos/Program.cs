using System;
using System.Collections.Generic;

public class Comment
{
    private string _commenterName;
    private string _text;

    public Comment(string commenterName, string text)
    {
        _commenterName = commenterName;
        _text = text;
    }

    public string GetCommenterName()
    {
        return _commenterName;
    }

    public string GetText()
    {
        return _text;
    }
}

public class Video
{
    private string _title;
    private string _author;
    private int _lengthInSeconds;
    private List<Comment> _comments;

    public Video(string title, string author, int lengthInSeconds)
    {
        _title = title;
        _author = author;
        _lengthInSeconds = lengthInSeconds;
        _comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return _comments.Count;
    }

    public string GetTitle()
    {
        return _title;
    }

    public string GetAuthor()
    {
        return _author;
    }

    public int GetLength()
    {
        return _lengthInSeconds;
    }

    public List<Comment> GetComments()
    {
        return _comments;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a list to store videos
        List<Video> videos = new List<Video>();

        // Create first video and its comments
        Video video1 = new Video("Learn C# in 10 Minutes", "CodeMaster", 600);
        video1.AddComment(new Comment("John123", "Great tutorial, very helpful!"));
        video1.AddComment(new Comment("BeginnerCoder", "Could you explain inheritance more?"));
        video1.AddComment(new Comment("PythonFan", "Switching to C# after this!"));
        videos.Add(video1);

        // Create second video and its comments
        Video video2 = new Video("Making Chocolate Cake", "BakingPro", 480);
        video2.AddComment(new Comment("SweetTooth", "The cake looks delicious!"));
        video2.AddComment(new Comment("HomeChef", "What brand of cocoa do you use?"));
        video2.AddComment(new Comment("BakingNewbie", "Mine turned out perfect!"));
        video2.AddComment(new Comment("CakeQueen", "Love your recipes!"));
        videos.Add(video2);

        // Create third video and its comments
        Video video3 = new Video("Morning Yoga Routine", "YogaLife", 900);
        video3.AddComment(new Comment("ZenMaster", "Perfect way to start the day"));
        video3.AddComment(new Comment("Flexible123", "The stretches helped my back pain"));
        video3.AddComment(new Comment("NewToYoga", "Can you do a beginner version?"));
        videos.Add(video3);

        // Display information for each video
        foreach (Video video in videos)
        {
            Console.WriteLine("\n=== Video Information ===");
            Console.WriteLine($"Title: {video.GetTitle()}");
            Console.WriteLine($"Author: {video.GetAuthor()}");
            Console.WriteLine($"Length: {video.GetLength()} seconds");
            Console.WriteLine($"Number of comments: {video.GetNumberOfComments()}");
            
            Console.WriteLine("\nComments:");
            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"{comment.GetCommenterName()}: {comment.GetText()}");
            }
        }
    }
}