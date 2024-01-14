using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        double lowNum;
        double highNum;

        // Loop to get a positive low number
        do
        {
            lowNum = GetUserInput("low");
        } while (lowNum <= 0);

        // Loop to get a high number greater than the low number
        do
        {
            highNum = GetUserInput("high");
        } while (highNum <= lowNum);

        List<double> numbersList = GenerateNumbersList(lowNum, highNum);

        string filePath = "numbers.txt";

        WriteNumbersToFile(numbersList, filePath);

        List<double> numbersFromFile = ReadNumbersFromFile(filePath);

        double sum = CalculateSum(numbersFromFile);
        Console.WriteLine($"The sum of the numbers is: {sum}");

        Console.WriteLine("Prime numbers between low and high:");
        foreach (double num in numbersFromFile)
        {
            if (IsPrime(num))
            {
                Console.Write($"{num} ");
            }
        }
    }

    static double GetUserInput(string prompt)
    {
        double userInput;
        while (true)
        {
            Console.Write($"Please enter a {prompt} number: ");
            if (double.TryParse(Console.ReadLine(), out userInput))
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
    }

    static List<double> GenerateNumbersList(double low, double high)
    {
        List<double> numbersList = new List<double>();
        for (double i = low; i <= high; i++)
        {
            numbersList.Add(i);
        }
        return numbersList;
    }

    static void WriteNumbersToFile(List<double> numbersList, string filePath)
    {
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            foreach (double num in numbersList)
            {
                sw.WriteLine(num);
            }
        }
        Console.WriteLine($"Numbers have been written to \"{filePath}\".");
    }

    static List<double> ReadNumbersFromFile(string filePath)
    {
        List<double> numbersList = new List<double>();
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                if (double.TryParse(line, out double number))
                {
                    numbersList.Add(number);
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"File not found: {filePath}");
        }
        return numbersList;
    }

    static double CalculateSum(List<double> numbersList)
    {
        double sum = 0;
        foreach (double num in numbersList)
        {
            sum += num;
        }
        return sum;
    }

    static bool IsPrime(double number)
    {
        if (number <= 1)
        {
            return false;
        }
        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0)
            {
                return false;
            }
        }
        return true;
    }
}

