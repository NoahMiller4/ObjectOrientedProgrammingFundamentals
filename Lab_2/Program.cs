
// Lab 2 Noah Miller

using System;
using System.Collections.Generic;
using System.Linq;
using Lab_1.Classes;

// links: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/constants
// https://learn.microsoft.com/en-us/dotnet/api/system.math.min?view=net-7.0
// referenced in lab 1 https://stackoverflow.com/questions/57813565/dictionary-find-min-max-values-within-a-range-of-given-keys-and-returning-their


// Creating a new vending machines
VendingMachine vendingMachine = new VendingMachine("ABC123");
VendingMachine vendingMachine2 = new VendingMachine("CBA321");
VendingMachine vendingMachine3 = new VendingMachine("BJB007");

// Stocking the vending machine with items and money float
vendingMachine.StockFloat(1, 4);
vendingMachine.StockItem(new Product("Chocolate-covered Beans", 2.0, "A12", 3), 2);
vendingMachine.StockFloat(1, 10);
vendingMachine.StockItem(new Product("Saltine Crackers", 4.0, "B1", 3), 1);

// Prompting the user for the inputs
Console.WriteLine("Enter the amount of money:");
double moneyAmount = double.Parse(Console.ReadLine());

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

        int denomination = int.Parse(input);
        insertedMoney.Add(denomination);
    }

}

// Perform the vending machine operation
string result = vendingMachine.VendItem(itemCode, insertedMoney);

// Prints a message showing the change given back along with the vending machine serial numbers.
Console.WriteLine($"{result}\nThank you for using Vending Machine {vendingMachine.SerialNumber}.\n" +
    $"Please try our other vending machines {vendingMachine2.SerialNumber} or {vendingMachine3.SerialNumber}");

