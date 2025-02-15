using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

// Base class for all goals
public abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _basePoints;
    protected bool _isComplete;

    public Goal(string name, string description, int basePoints)
    {
        _name = name;
        _description = description;
        _basePoints = basePoints;
        _isComplete = false;
    }

    public abstract int RecordEvent();
    public abstract string GetDisplayString();
    
    public string GetName() => _name;
    public bool IsComplete() => _isComplete;
}

// Simple one-time goals
public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points) 
        : base(name, description, points) { }

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            return _basePoints;
        }
        return 0;
    }

    public override string GetDisplayString()
    {
        return $"[{(_isComplete ? "X" : " ")}] {_name} - {_description}";
    }
}

// Eternal goals that can be repeated
public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) 
        : base(name, description, points) { }

    public override int RecordEvent()
    {
        return _basePoints;
    }

    public override string GetDisplayString()
    {
        return $"[ ] {_name} - {_description} (Eternal)";
    }
}

// Checklist goals that need to be done multiple times
public class ChecklistGoal : Goal
{
    private int _target;
    private int _timesCompleted;
    private int _bonusPoints;

    public ChecklistGoal(string name, string description, int points, int target, int bonus) 
        : base(name, description, points)
    {
        _target = target;
        _bonusPoints = bonus;
        _timesCompleted = 0;
    }

    public override int RecordEvent()
    {
        _timesCompleted++;
        if (_timesCompleted == _target)
        {
            _isComplete = true;
            return _basePoints + _bonusPoints;
        }
        return _basePoints;
    }

    public override string GetDisplayString()
    {
        return $"[{(_isComplete ? "X" : " ")}] {_name} - {_description} (Completed {_timesCompleted}/{_target} times)";
    }
}

// Quest manager to handle all game logic
public class QuestManager
{
    private List<Goal> _goals;
    private int _score;
    private int _level;
    private string _title;

    public QuestManager()
    {
        _goals = new List<Goal>();
        _score = 0;
        _level = 1;
        _title = "Novice Quester";
    }

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void RecordEvent(int index)
    {
        if (index >= 0 && index < _goals.Count)
        {
            int points = _goals[index].RecordEvent();
            _score += points;
            CheckLevelUp();
            Console.WriteLine($"Congratulations! You earned {points} points!");
        }
    }

    private void CheckLevelUp()
    {
        int newLevel = (_score / 1000) + 1;
        if (newLevel > _level)
        {
            _level = newLevel;
            UpdateTitle();
            Console.WriteLine($"\nLEVEL UP! You are now level {_level}: {_title}");
        }
    }

    private void UpdateTitle()
    {
        _title = _level switch
        {
            1 => "Novice Quester",
            2 => "Adventurous Spirit",
            3 => "Determined Seeker",
            4 => "Valiant Warrior",
            5 => "Noble Champion",
            _ => "Legendary Hero"
        };
    }

    public void DisplayGoals()
    {
        Console.WriteLine($"\nLevel {_level} {_title} - Total Score: {_score}\n");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDisplayString()}");
        }
    }

    public void SaveToFile(string filename)
    {
        var data = new { Score = _score, Level = _level, Title = _title, Goals = _goals };
        string jsonString = JsonSerializer.Serialize(data);
        File.WriteAllText(filename, jsonString);
    }

    public void LoadFromFile(string filename)
    {
        if (File.Exists(filename))
        {
            string jsonString = File.ReadAllText(filename);
            var data = JsonSerializer.Deserialize<QuestManager>(jsonString);
            _score = data._score;
            _level = data._level;
            _title = data._title;
            _goals = data._goals;
        }
    }
}

// Main program
public class Program
{
    static QuestManager questManager = new QuestManager();

    public static void Main()
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateNewGoal();
                    break;
                case "2":
                    RecordEvent();
                    break;
                case "3":
                    questManager.DisplayGoals();
                    break;
                case "4":
                    SaveProgress();
                    break;
                case "5":
                    LoadProgress();
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            if (running)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }

    private static void DisplayMenu()
    {
        Console.WriteLine("Eternal Quest - Goal Tracker");
        Console.WriteLine("1. Create New Goal");
        Console.WriteLine("2. Record Event");
        Console.WriteLine("3. Display Goals");
        Console.WriteLine("4. Save Progress");
        Console.WriteLine("5. Load Progress");
        Console.WriteLine("6. Quit");
        Console.Write("\nSelect an option: ");
    }

    private static void CreateNewGoal()
    {
        Console.WriteLine("\nSelect goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        
        string type = Console.ReadLine();
        
        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        
        Console.Write("Enter goal description: ");
        string description = Console.ReadLine();
        
        Console.Write("Enter base points: ");
        int points = int.Parse(Console.ReadLine());

        Goal goal;
        switch (type)
        {
            case "1":
                goal = new SimpleGoal(name, description, points);
                break;
            case "2":
                goal = new EternalGoal(name, description, points);
                break;
            case "3":
                Console.Write("Enter target count: ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("Enter bonus points: ");
                int bonus = int.Parse(Console.ReadLine());
                goal = new ChecklistGoal(name, description, points, target, bonus);
                break;
            default:
                Console.WriteLine("Invalid goal type.");
                return;
        }

        questManager.AddGoal(goal);
        Console.WriteLine("Goal created successfully!");
    }

    private static void RecordEvent()
    {
        questManager.DisplayGoals();
        Console.Write("\nEnter the number of the goal you completed: ");
        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            questManager.RecordEvent(choice - 1);
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }
    }

    private static void SaveProgress()
    {
        Console.Write("Enter filename to save: ");
        string filename = Console.ReadLine();
        questManager.SaveToFile(filename);
        Console.WriteLine("Progress saved successfully!");
    }

    private static void LoadProgress()
    {
        Console.Write("Enter filename to load: ");
        string filename = Console.ReadLine();
        questManager.LoadFromFile(filename);
        Console.WriteLine("Progress loaded successfully!");
    }
}