// Lab 3 Noah Miller

using System;
using System.Collections.Generic;
using System.Linq;
using Lab_1.Classes;

// links: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/constants
// https://learn.microsoft.com/en-us/dotnet/api/system.math.min?view=net-7.0
// referenced in lab 1 https://stackoverflow.com/questions/57813565/dictionary-find-min-max-values-within-a-range-of-given-keys-and-returning-their

// initialize variable result
string result = string.Empty;

try
{
    VendingMachine vendingMachine = new VendingMachine("ABC123");
    VendingMachine vendingMachine2 = new VendingMachine("CBA321");
    VendingMachine vendingMachine3 = new VendingMachine("BJB007");

    vendingMachine.StockFloat(1, 4);
    vendingMachine.StockItem(new Product("Chocolate-covered Beans", 2.0, "A12", 3), 2);
    vendingMachine.StockFloat(1, 10);
    vendingMachine.StockItem(new Product("Saltine Crackers", 4.0, "B1", 3), 1);

    Console.WriteLine("Please insert money:");
    double moneyAmount;
    // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/out-parameter-modifier
    // https://www.geeksforgeeks.org/out-parameter-with-examples-in-c-sharp/
    if (!double.TryParse(Console.ReadLine(), out moneyAmount))
    {
        throw new Exception("Please enter a valid number.");
    }

    Console.WriteLine("Enter the item code:");
    string itemCode = Console.ReadLine();

    List<int> insertedMoney = new List<int>();

    if (moneyAmount > 0)
    {
        Console.WriteLine("Enter the money denominations one by one (press enter when empty to finish):");
        Console.WriteLine("Example; if you entered 5..");
        Console.WriteLine("1");
        Console.WriteLine("5");
        Console.WriteLine("");
        bool running = true;
        while (running)
        {
            string input = Console.ReadLine();
            if (input.ToLower() == "")
                break;

            int denomination;
            if (!int.TryParse(input, out denomination))
            {
                throw new Exception("Invalid money denomination entered.");
            }

            insertedMoney.Add(denomination);
        }
    }

    vendingMachine.VendItem(itemCode, insertedMoney);
    // use variable result to update the result with a success message
    result = "Vending successful"; 

    Console.WriteLine($"{result}\nThank you for using Vending Machine {vendingMachine.SerialNumber}.\n" +
        $"Please try our other vending machines {vendingMachine2.SerialNumber} or {vendingMachine3.SerialNumber}");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}