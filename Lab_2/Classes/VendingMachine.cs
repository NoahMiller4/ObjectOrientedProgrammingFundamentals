using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1.Classes
{
    public class VendingMachine
    {
        // must add static property SerialNumberIncrement to track next serial number 
        // initialize at 1
        public static int SerialNumberIncrement { get; private set; } = 1;

        // Unique ID for the machine https://www.delftstack.com/howto/csharp/csharp-private-set/
        public int SerialNumber { get; private set; }

        // Barcode of the machine
        public readonly string Barcode;

        // Dictionary to track the amount of money pieces that the machine has
        private Dictionary<int, int> MoneyFloat { get; set; }

        // Dictionary for the inventory of the products in the machine
        private Dictionary<string, Product> Inventory { get; set; }

        // Constructor to initialize the vending machine with barcode
        public VendingMachine(string barcode)
        {
            SerialNumber = SerialNumberIncrement++;
            Barcode = barcode;

            // Initialize the money float dictionary with default amount of coins
            MoneyFloat = new Dictionary<int, int>
        {
            { 1, 0 },
            { 2, 0 },
            { 5, 0 },
            { 10, 0 },
            { 20, 0 }
        };

            // Initialize the inventory dictionary
            Inventory = new Dictionary<string, Product>();

        }

        // Adding a product to the vending machine's inventory
        public void StockItem(Product product, int quantity)
        {
            if (Inventory.ContainsKey(product.Code))
            {
                // If product is already in inventory, increase quantity
                Inventory[product.Code].Quantity += quantity;
            }
            else
            {
                // If product is not in inventory, add
                Inventory.Add(product.Code, product);
            }

            // Print the information about the stocked product
            Console.WriteLine($"Stocked {product.Name} - Code: {product.Code}, Price: ${product.Price}, Quantity: {quantity}");
        }

        // Adds money pieces of the denomination to machine's money float
        public void StockFloat(int moneyDenomination, int quantity)
        {
            if (MoneyFloat.ContainsKey(moneyDenomination))
            {
                // If denomination is already in float, increase quantity
                MoneyFloat[moneyDenomination] += quantity;
            }
            else
            {
                // If denomination is not in the float, add it with quantity
                MoneyFloat.Add(moneyDenomination, quantity);
            }

            // Print the information about the stocked coins
            Console.WriteLine($"Stocked ${moneyDenomination} coins - Quantity: {quantity}");
        }

        // Performs the vending operation for theproduct code and money
        public string VendItem(string code, List<int> money)
        {
            if (!Inventory.ContainsKey(code))
            {
                // If product code is not found in inventory, return an error message
                return ($"Error: No item with code {code}");
            }

            Product selectedProduct = Inventory[code];

            if (selectedProduct.Quantity == 0)
            {
                // If product is out of stock, return error message
                return "Error: Item is out of stock.";
            }

            double totalPrice = selectedProduct.Price;
            int totalMoney = money.Sum();

            if (totalMoney < totalPrice)
            {
                // If provided money is not enough to buy product, return error message
                return "Error: Insufficient money provided.";
            }

            double changeAmount = totalMoney - totalPrice;

            // Get the coins to return as change
            Dictionary<int, int> coinsToReturn = GetCoinsToReturn(changeAmount);

            if (coinsToReturn == null)
            {
                // If the machine does not have enough change, return an error message
                return "Error: The machine does not have enough change.";
            }

            // Reduce quantity of selected product in the inventory
            selectedProduct.Quantity--;

            // Update money float by deducting the coins returned as change, must use keyvaluepair
            foreach (KeyValuePair<int, int> coin in coinsToReturn)
            {
                MoneyFloat[coin.Key] -= coin.Value;
            }

            // Format change message to be displayed
            string changeMessage = FormatChangeMessage(coinsToReturn);

            // Return vending operation result with the change message
            return $"Please enjoy your '{selectedProduct.Name}' and take your change of ${changeAmount}. {changeMessage}";
        }

        // Calculate coins to return as change for given change amount
        private Dictionary<int, int> GetCoinsToReturn(double changeAmount)
        {
            Dictionary<int, int> coinsToReturn = new Dictionary<int, int>();

            // Iterate over each coin in the MoneyFloat dictionary, starting from the highest denomination
            //https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.orderbydescending?view=net-7.0
            foreach (KeyValuePair<int, int> coin in MoneyFloat.OrderByDescending(c => c.Key))

            // Get denomination of the coin
            {
                int denomination = coin.Key;
                // Get available quantity of the coin
                int availableQuantity = coin.Value;

                // Calculate number of coins needed to cover remaining change amount
                int coinsNeeded = (int)(changeAmount / denomination);

                // Determine the actual number of coins to return (limited by available quantity and coins needed)
                int coinsToReturnQuantity;
                if (coinsNeeded < availableQuantity)
                {
                    coinsToReturnQuantity = coinsNeeded;
                }
                else
                {
                    coinsToReturnQuantity = availableQuantity;
                }

                // If there are coins to return, add them to coinsToReturn dictionary and update the change amount
                if (coinsToReturnQuantity > 0)
                {
                    coinsToReturn.Add(denomination, coinsToReturnQuantity);
                    changeAmount -= coinsToReturnQuantity * denomination;
                }

                // If change amount has been fully covered, return the coinsToReturn dictionary
                if (changeAmount <= 0)
                {
                    // Found enough coins to cover the change amount
                    return coinsToReturn;
                }
            }

            // The machine does not have sufficient change
            return default;
        }

        // Formats the change message to be displayed
        private string FormatChangeMessage(Dictionary<int, int> coinsToReturn)
        {
            List<string> changeParts = new List<string>();

            foreach (KeyValuePair<int, int> coin in coinsToReturn)
            {
                string coinDescription = ($"${coin.Key}");
                changeParts.Add($"{coin.Value} {coinDescription} coins returned");
            }

            return string.Join(", ", changeParts);
        }
    }
}
