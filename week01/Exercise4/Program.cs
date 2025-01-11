using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();
        
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        
        while (true)
        {
            Console.Write("Enter number: ");
            int userNumber = int.Parse(Console.ReadLine());
            
            if (userNumber == 0)
                break;
                
            numbers.Add(userNumber);
        }

        if (numbers.Count > 0)
        {
            // Core Requirements
            // 1. Calculate sum
            int sum = numbers.Sum();
            Console.WriteLine($"The sum is: {sum}");

            // 2. Calculate average
            double average = numbers.Average();
            Console.WriteLine($"The average is: {average}");

            // 3. Find maximum
            int max = numbers.Max();
            Console.WriteLine($"The largest number is: {max}");

            // Stretch Challenges
            // 1. Find smallest positive number
            int smallestPositive = numbers
                .Where(n => n > 0)
                .DefaultIfEmpty(0)
                .Min();
            if (smallestPositive > 0)
            {
                Console.WriteLine($"The smallest positive number is: {smallestPositive}");
            }

            // 2. Sort and display the list
            List<int> sortedNumbers = numbers.OrderBy(n => n).ToList();
            Console.WriteLine("The sorted list is:");
            foreach (int number in sortedNumbers)
            {
                Console.WriteLine(number);
            }
        }
        else
        {
            Console.WriteLine("No numbers were entered.");
        }
    }
}