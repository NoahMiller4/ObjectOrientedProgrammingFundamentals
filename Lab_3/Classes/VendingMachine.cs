using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_1.Classes
{
    public class VendingMachine
    {
        public static int SerialNumberIncrement { get; private set; } = 1;
        public int SerialNumber { get; private set; }
        public readonly string Barcode;
        private Dictionary<int, int> MoneyFloat { get; set; }
        private Dictionary<string, Product> Inventory { get; set; }

        public VendingMachine(string barcode)
        {
            // using try/catch block to validate barcode and all other methods.
            try
            {
                if (string.IsNullOrEmpty(barcode))
                    throw new ArgumentException("Vending machine barcode cannot be null or empty.");

                SerialNumber = SerialNumberIncrement++;
                Barcode = barcode;
                MoneyFloat = new Dictionary<int, int>
                {
                    { 1, 0 },
                    { 2, 0 },
                    { 5, 0 },
                    { 10, 0 },
                    { 20, 0 }
                };
                Inventory = new Dictionary<string, Product>();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error creating vending machine: {ex.Message}");
                throw;
            }
        }

        public void StockItem(Product product, int quantity)
        {
            try
            {
                if (quantity < 0)
                    throw new ArgumentException("Quantity cannot be negative.");

                if (Inventory.ContainsKey(product.Code))
                {
                    Inventory[product.Code].Quantity += quantity;
                }
                else
                {
                    Inventory.Add(product.Code, product);
                }

                Console.WriteLine($"Stocked {product.Name} - Code: {product.Code}, Price: ${product.Price}, Quantity: {quantity}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error stocking item: {ex.Message}");
                throw;
            }
        }

        public void StockFloat(int moneyDenomination, int quantity)
        {
            try
            {
                if (quantity < 0)
                    throw new ArgumentException("Quantity cannot be negative.");

                if (MoneyFloat.ContainsKey(moneyDenomination))
                {
                    MoneyFloat[moneyDenomination] += quantity;
                }
                else
                {
                    MoneyFloat.Add(moneyDenomination, quantity);
                }

                Console.WriteLine($"Stocked ${moneyDenomination} coins - Quantity: {quantity}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error stocking float: {ex.Message}");
                throw;
            }
        }

        public void VendItem(string code, List<int> money)
        {
            try
            {
                // https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.sum?view=net-7.0
                if (!Inventory.ContainsKey(code))
                    throw new ArgumentException("Invalid item code.");

                if (money.Sum() < Inventory[code].Price)
                    throw new ArgumentException("Insufficient funds.");

                int change = money.Sum() - (int)Inventory[code].Price;
                string changeString = GetChangeString(change);

                Inventory[code].Quantity--;
                Console.WriteLine($"Vended {Inventory[code].Name} - Code: {Inventory[code].Code}, Price: ${Inventory[code].Price}");

                Console.WriteLine($"Change: ${change}\n{changeString}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error vending item: {ex.Message}");
                throw;
            }
        }

        private string GetChangeString(int change)
        {
            // Create dictionary to store number of each denomination for change
            Dictionary<int, int> changeDictionary = new Dictionary<int, int>
        {
            { 100, 0 },
            { 25, 0 },
            { 10, 0 },
            { 5, 0 },
            { 1, 0 }
        };
            // using foreach, iterate through each denomination in change dictionary
            foreach (var denomination in changeDictionary.Keys)
            {
                // while  change is greater than or equal to the denomination, and there are available
                // coins/bills of current denomination in money float, keep looping 
                while (change >= denomination && MoneyFloat[denomination] > 0)
                {
                    // subtract the current denomination/value from the change
                    change -= denomination;
                    // decrease count of current denomination in the money float
                    MoneyFloat[denomination]--;
                    // increase count of current denomination in the change dictionary
                    changeDictionary[denomination]++;
                }
            }
            // Create empty string to store new change string
            List<string> changeStrings = new List<string>();
            // iterate through each denomination in changeDict dictionary
            foreach (var kv in changeDictionary)
            {
                if (kv.Value > 0)
                {
                    // if count of the current denomination is greater than 0, change
                    // the current denomination and its count as a string and append to changeString
                    string currentChangeString = $"{kv.Value} x ${(kv.Key / 100.0)}";
                    changeStrings.Add(currentChangeString);
                }
            }
            // join changeStrings together with comma and space
            string finalChangeString = string.Join(", ", changeStrings);
            // finally, return the amount of change given.
            return $"Change given: {finalChangeString}";
        }
    }
}