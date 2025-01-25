using System;
using System.Linq;
using System.Collections.Generic;

// Represents a scripture reference (e.g., "John 3:16" or "Proverbs 3:5-6")
public class ScriptureReference
{
    public string Book { get; private set; }
    public int StartChapter { get; private set; }
    public int StartVerse { get; private set; }
    public int? EndChapter { get; private set; }
    public int? EndVerse { get; private set; }

    // Constructor for single verse
    public ScriptureReference(string book, int chapter, int verse)
    {
        Book = book;
        StartChapter = chapter;
        StartVerse = verse;
    }

    // Constructor for verse range
    public ScriptureReference(string book, int startChapter, int startVerse, int? endChapter, int endVerse)
    {
        Book = book;
        StartChapter = startChapter;
        StartVerse = startVerse;
        EndChapter = endChapter;
        EndVerse = endVerse;
    }

    public override string ToString()
    {
        return EndVerse.HasValue 
            ? $"{Book} {StartChapter}:{StartVerse}-{EndVerse}" 
            : $"{Book} {StartChapter}:{StartVerse}";
    }
}

// Represents a single word in the scripture
public class ScriptureWord
{
    public string Text { get; private set; }
    public bool IsHidden { get; private set; }

    public ScriptureWord(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public string GetDisplayText()
    {
        return IsHidden ? new string('_', Text.Length) : Text;
    }

    public void Hide()
    {
        IsHidden = true;
    }
}

// Represents the entire scripture
public class Scripture
{
    private ScriptureReference _reference;
    private List<ScriptureWord> _words;
    private Random _random;

    public Scripture(ScriptureReference reference, string text)
    {
        _reference = reference;
        _random = new Random();
        
        _words = text.Split(' ')
            .Select(word => new ScriptureWord(word.Trim('.',',',';',':')))
            .ToList();
    }

    public void Display()
    {
        Console.Clear();
        Console.WriteLine(_reference.ToString());
        Console.WriteLine(string.Join(" ", _words.Select(w => w.GetDisplayText())));
    }

    public bool HideRandomWord()
    {
        var unhiddenWords = _words.Where(w => !w.IsHidden).ToList();
        
        if (!unhiddenWords.Any())
            return false;

        var wordToHide = unhiddenWords[_random.Next(unhiddenWords.Count)];
        wordToHide.Hide();
        
        return true;
    }

    public bool AllWordsHidden()
    {
        return _words.All(w => w.IsHidden);
    }
}

class Program
{
    static void Main()
    {
        // Create a scripture reference and text
        var reference = new ScriptureReference("Proverbs", 3, 5, null, 6);
        var scripture = new Scripture(reference, 
            "Trust in the Lord with all your heart and lean not on your own understanding; " +
            "in all your ways submit to him, and he will make your paths straight.");

        while (true)
        {
            scripture.Display();

            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");
            var input = Console.ReadLine();

            if (input?.ToLower() == "quit")
                break;

            if (!scripture.HideRandomWord())
                break;

            if (scripture.AllWordsHidden())
            {
                scripture.Display();
                break;
            }
        }
    }
}