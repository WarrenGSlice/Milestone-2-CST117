using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/* * * * * * * * * * * * *
 * * Warren Peterson * * *
 * This is my Own Work * *
 * CST-117 4/24/2021 * * *
 * * * * * * * * * * * * */
namespace Milestone2
{
    public partial class Inventory : Form
    {
        // Creates an InventoryManager class object that allows the methods from that class to be used--Inheritance
        public InventoryManager im { get; set; } = new InventoryManager(); 
        private CSVManager CSVManager; // Inherits the CSVManager class and its methods and sets them to this variable
        private static string INVENTORY_LOCATION = "../../InventoryList.csv"; // Sets the location of the Inventory CSV File
        public Inventory()
        {
            InitializeComponent();
        }

        // Add Video Button
        private void AddVideoButton_Click(object sender, EventArgs e)
        {
            // Creates a Inventory Item Object
            InventoryItems newItem = new InventoryItems( int.Parse(VideoIDInventoryControlTextBox.Text), VideoNameInventoryTextBox.Text, int.Parse(QuantityInStockInventoryTextBox.Text), 
                int.Parse(QuantityCheckedOutInventoryTextBox.Text), int.Parse(TotalQuantityInventoryTextBox.Text), MediaTypeInventoryEditComboBox.Text, 
                DownloadTypeInventoryEditComboBox.Text, decimal.Parse(PriceInventoryEditTextBox.Text), int.Parse(GenreIDInventoryEditTextBox.Text), 
                GenreInventoryEditComboBox.Text, AdultInventoryEditTextBox.Text);
            im.AddItem(newItem); //Add the new item to the Inventory Manager
            UpdateInventoryList(); //Updates the Inventory List
            VideoIDInventoryControlTextBox.Text = ""; // Makes the add item edit control text boxes blank after adding a new item
            VideoNameInventoryTextBox.Text = "";
            QuantityInStockInventoryTextBox.Text = "";
            QuantityCheckedOutInventoryTextBox.Text = "";
            TotalQuantityInventoryTextBox.Text = "";
            MediaTypeInventoryEditComboBox.Text = "";
            DownloadTypeInventoryEditComboBox.Text = "";
            PriceInventoryEditTextBox.Text = "";
            GenreIDInventoryEditTextBox.Text = "";
            GenreInventoryEditComboBox.Text = "";
            AdultInventoryEditTextBox.Text = "";
        }

        // Method to Update the Inventory List Box and save the changes
        public void UpdateInventoryList()
        {
            VideoIDSearchTextBox.Text = "";
            VideoNameSearchTextBox.Text = "";
            MediaTypeSearchComboBox.Text = "";
            GenreSearchComboBox.Text = "";
            InventoryListBox.ClearSelected();
            InventoryListBox.Items.Clear();
            foreach (InventoryItems item in im.Inventory)
            {
                InventoryListBox.Items.Add(item);
            }
            UpdateSelected();
            CSVManager.saveCSV(INVENTORY_LOCATION, im.GetCSVList());
        }
        // Method to Update the Inventory List in the InventoryItems class
        private void UpdateInventoryList(List<InventoryItems> inventory)
        {
            InventoryListBox.ClearSelected();
            InventoryListBox.Items.Clear();
            foreach(InventoryItems item in inventory)
            {
                InventoryListBox.Items.Add(item);
            }
            UpdateSelected();
        }

        //  Selects the gridView text boxes for showing item details
        private void UpdateSelected()
        {
            VideoIDOutput.Text = "";
            VideoNameOutput.Text = "";
            QtyInStockOutput.Text = "";
            QtyCheckedOutput.Text = "";
            TotalQtyOutput.Text = "";
            MediaTypeOutput.Text = "";
            DownloadTypeOutput.Text = "";
            PriceOutput.Text = "";
            GenreIdOutput.Text = "";
            GenreOutput.Text = "";
            AdultOutput.Text = "";
        }

        // Send the text from the text box to the selectedItem Variable
        private void UpdateSelected(InventoryItems selectedItem)
        {
            VideoIDOutput.Text = selectedItem.VideoID.ToString();
            VideoNameOutput.Text = selectedItem.VideoName;
            QtyInStockOutput.Text = selectedItem.QuantityInStock.ToString();
            QtyCheckedOutput.Text = selectedItem.QuantityCheckedOut.ToString();
            TotalQtyOutput.Text = selectedItem.TotalQuantity.ToString();
            MediaTypeOutput.Text = selectedItem.MediaType;
            DownloadTypeOutput.Text = selectedItem.DownloadType;
            PriceOutput.Text = selectedItem.Price.ToString("C2");
            GenreIdOutput.Text = selectedItem.GenreID.ToString();
            GenreOutput.Text = selectedItem.Genre;
            AdultOutput.Text = selectedItem.Adult;

            VideoIDInventoryControlTextBox.Text = selectedItem.VideoID.ToString();
            VideoNameInventoryTextBox.Text = selectedItem.VideoName;
            QuantityInStockInventoryTextBox.Text = selectedItem.QuantityInStock.ToString();
            QuantityCheckedOutInventoryTextBox.Text = selectedItem.QuantityCheckedOut.ToString();
            TotalQuantityInventoryTextBox.Text = selectedItem.TotalQuantity.ToString();
            MediaTypeInventoryEditComboBox.Text = selectedItem.MediaType;
            DownloadTypeInventoryEditComboBox.Text = selectedItem.DownloadType;
            PriceInventoryEditTextBox.Text = selectedItem.Price.ToString("C2");
            GenreIDInventoryEditTextBox.Text = selectedItem.GenreID.ToString();
            GenreInventoryEditComboBox.Text = selectedItem.Genre;
            AdultInventoryEditTextBox.Text = selectedItem.Adult;
        }

        // First actions to be taken when the program starts
        private void Inventory_Load(object sender, EventArgs e)
        {
            CSVManager = new CSVManager(); //Opens the CSV class methods
            List<string[]> inventory = CSVManager.readCsv(INVENTORY_LOCATION); // Reads the CSV file to the Inventory string array List
            im.AddItems(inventory); // Adds the items from the string array list to the Inventory Manager
            UpdateInventoryList(); // Updates the items shown on the InventoryListBox
        }

        // Shows details about the inventory item when the object is selected in the Inventory List Box
        private void InventoryListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InventoryListBox.SelectedIndex != -1)
            {
                InventoryItems selectedItem = (InventoryItems)InventoryListBox.SelectedItem;
                UpdateSelected(selectedItem);
            }
            else
            {
                UpdateSelected();
            }
        }

        // Clears the fields so that a new item can be created
        private void ClearFieldsButton_Click(object sender, EventArgs e)
        {
            VideoIDInventoryControlTextBox.Text = "";
            VideoNameInventoryTextBox.Text = "";
            QuantityInStockInventoryTextBox.Text = "";
            QuantityCheckedOutInventoryTextBox.Text = "";
            TotalQuantityInventoryTextBox.Text = "";
            MediaTypeInventoryEditComboBox.Text = "";
            DownloadTypeInventoryEditComboBox.Text = "";
            PriceInventoryEditTextBox.Text = "";
            GenreIDInventoryEditTextBox.Text = "";
            GenreInventoryEditComboBox.Text = "";
            AdultInventoryEditTextBox.Text = "";
        }
    }
}
