using System;
using System.Collections.Generic;
namespace ExerciseTracking
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create activities
            Running run1 = new Running(new DateTime(2023, 11, 3), 30, 3.0);
            Cycling cycle1 = new Cycling(new DateTime(2023, 11, 3), 45, 15.0);
            Swimming swim1 = new Swimming(new DateTime(2023, 11, 3), 20, 10);
            
            // Add to list
            List<Activity> activities = new List<Activity>();
            activities.Add(run1);
            activities.Add(cycle1);
            activities.Add(swim1);
            
            // Display summaries
            foreach (Activity activity in activities)
            {
                Console.WriteLine(activity.GetSummary());
            }
            
            Console.ReadLine();
        }
    }
}