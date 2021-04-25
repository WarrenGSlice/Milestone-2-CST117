using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* * * * * * * * * * * * *
 * * Warren Peterson * * *
 * This is my Own Work * *
 * CST-117 4/24/2021 * * *
 * * * * * * * * * * * * */
namespace Milestone2
{
    // Inventory Manager Class that contains the methods to Add, Edit and Remove items from the Inventory
    public class InventoryManager
    {
        // Creates a List of Inventory Items Object that can be called on in the other classes.
        public List<InventoryItems> Inventory { get; private set; } = new List<InventoryItems>();
        public static int DEFAULT_QUANTITY { get; } = 1; // Sets default item property quantity to value of 1

        // Add Item method for adding items to the Item List
        public InventoryItems AddItem(InventoryItems item)
        {
            if (ContainsItem(item))
            {
                InventoryItems foundItem = FindItem(item);
                foundItem.TotalQuantity += item.TotalQuantity;
                return foundItem;
            }
            else
            {
                Inventory.Add(item);
                return item;
            }
        }

        // Add Item method for adding items to the List Array and also creates a new Inventory object 
        // whenever a new item is added
        public void AddItems(List<string[]> items)
        {
            foreach(string[] item in items)
            {
                InventoryItems newItem = new InventoryItems(item);
                AddItem(newItem);
            }
        }

        // Method to be used later to search for items by the item name
        public InventoryItems FindItem(string itemName)
        {
            return Inventory.Find(x => x.Equals(itemName));
        }

        // Method for finding items
        public InventoryItems FindItem(InventoryItems item)
        {
            return Inventory.Find(x => x.Equals(item));
        }

        // Method for finding items to be used later to search for items
        // by Price and sends the results to a new List of items that does
        // not change the original item List
        public List<InventoryItems> FindItemsUnder(decimal price)
        {
            List<InventoryItems> searchResults = new List<InventoryItems>();
            return Inventory.FindAll(x => x.Price < price);
        }

        // Helper Method used for finding items contained in the item List
        public bool ContainsItem(InventoryItems item)
        {
            return Inventory.Contains(item);
        }

        //Method to be used later for Removing Items
        public bool RemoveItem(InventoryItems item)
        {
            if (ContainsItem(item))
            {
                Inventory.Remove(item);
                return true;
            }
            return false;
        }

        // To String method that ensures newly added items are added in the correct format
        public override string ToString()
        {
            string value = "";
            foreach (InventoryItems item in Inventory)
            {
                value += "[" + item.ToString() + "]\n\n";
            }
            return value;
        }

        // Method that takes each line from the CSV List and creates a new list array
        // object for holding them and then returns the results
        public List<string[]> GetCSVList()
        {
            List<string[]> lines = new List<string[]>();
            foreach (InventoryItems item in Inventory)
            {
                lines.Add(item.ToArray());
            }
            return lines;
        }
    }
}
