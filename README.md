1. Duplicate Numbers
2. using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter numbers separated by spaces:");
        string input = Console.ReadLine();
        string[] parts = input.Split(' ');

        var numbers = new List<int>();

        try
        {
            foreach (var part in parts)
            {
                int num = int.Parse(part);

                if (numbers.Contains(num))
                    throw new Exception($"Duplicate number found: {num}");

                numbers.Add(num);
            }

            Console.WriteLine("No duplicates found!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}



2. String Contains Vowels

3. using System;

class Program
{
    static void CheckVowels(string input)
    {
        string vowels = "aeiouAEIOU";

        foreach (char c in input)
        {
            if (vowels.Contains(c))
                return; // found a vowel, all good
        }

        throw new Exception("String contains no vowels!");
    }

    static void Main()
    {
        Console.WriteLine("Enter a string:");
        string input = Console.ReadLine();

        try
        {
            CheckVowels(input);
            Console.WriteLine("String contains vowels — OK!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
