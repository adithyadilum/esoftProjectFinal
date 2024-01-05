using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Esoft_Project.Forms
{
    public partial class formInventory : Form
    {
        private readonly SqlConnection sqlConnection = new SqlConnection("Data Source=ROG-STRIX-G15;Initial Catalog=HMSdb;Integrated Security=True;Encrypt=True; TrustServerCertificate=true;");
        private SqlDataAdapter dataAdapter;
        private DataTable dataTable = new DataTable();

        public formInventory()
        {
            InitializeComponent();
            ComboItemCode.SelectedIndexChanged += ComboItemCode_SelectedIndexChanged;
            InitializeDataGridView();
            LoadData();
        }

        // Initialize DataGridView
        private void InitializeDataGridView()
        {
            // Remove the lines that manually add columns

            // Set up the data adapter with a SELECT query
            dataAdapter = new SqlDataAdapter("SELECT * FROM Inventory", sqlConnection);

            // Build a SqlCommandBuilder to automatically generate SQL update commands
            SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(dataAdapter);

            // Bind the DataGridView to the DataTable
            DataGridViewInventory.DataSource = dataTable;
        }

        // Load data based on ComboBox selection
        private void LoadData()
        {
            if (ComboItemCode.SelectedItem == null)
                return;

            int selectedItemId = Convert.ToInt32(ComboItemCode.SelectedItem.ToString());

            try
            {
                sqlConnection.Open();

                // Use a parameterized query to filter data based on ItemID
                string query = "SELECT * FROM Inventory WHERE ItemID = @ItemID";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ItemID", selectedItemId);

                // Clear existing data in the DataTable
                dataTable.Clear();

                // Fill the DataTable with the filtered data
                dataAdapter.SelectCommand = sqlCommand;
                dataAdapter.Fill(dataTable);

                // Re-bind the DataGridView to the DataTable
                DataGridViewInventory.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        // Event handler for ComboBox selection change
        private void ComboItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        // Load ComboBox data
        private void LoadComboBoxData()
        {
            try
            {
                sqlConnection.Open();

                // Clear existing items before adding new ones
                ComboItemCode.Items.Clear();

                // Get the maximum ItemID value from the Inventory table
                string queryMaxItemID = "SELECT MAX(ItemID) AS LastValue FROM Inventory";
                SqlCommand sqlCommandMaxItemID = new SqlCommand(queryMaxItemID, sqlConnection);
                var lastValue = sqlCommandMaxItemID.ExecuteScalar();

                // Set the default value for ComboBoxItemID based on the maximum ItemID
                if (lastValue != DBNull.Value)
                {
                    int defaultValue = Convert.ToInt32(lastValue) + 1;
                    ComboItemCode.Text = defaultValue.ToString();
                }

                // Retrieve all ItemID values from the Inventory table and populate ComboBoxItemID
                string queryAllItemIDs = "SELECT ItemID FROM Inventory";
                SqlCommand sqlCommandAllItemIDs = new SqlCommand(queryAllItemIDs, sqlConnection);
                SqlDataReader reader = sqlCommandAllItemIDs.ExecuteReader();

                while (reader.Read())
                {
                    ComboItemCode.Items.Add(reader["ItemID"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        // Load initial data and display in the chart
        private void formInventory_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
            LoadData();
            InitializeChart();
        }

        // Initialize the chart with data from DataTable
        private void InitializeChart()
        {
            try
            {
                // Clear existing series in the Chart
                chartInventory.Series.Clear();

                // Create a new series
                Series series = new Series("InventoryItems");

                // Load data from the Inventory table
                DataTable chartDataTable = new DataTable();
                using (SqlDataAdapter chartDataAdapter = new SqlDataAdapter("SELECT ItemName, QuantityInStock FROM Inventory", sqlConnection))
                {
                    chartDataAdapter.Fill(chartDataTable);
                }

                // Add data points for each item in the DataTable
                foreach (DataRow row in chartDataTable.Rows)
                {
                    // Assuming the 'ItemName' column contains the item names
                    string itemName = row["ItemName"].ToString();

                    // Assuming the 'QuantityInStock' column contains the quantities
                    int quantityInStock = Convert.ToInt32(row["QuantityInStock"]);

                    // Add a data point for each item
                    series.Points.AddXY(itemName, quantityInStock);
                }

                // Set the chart type to Column
                series.ChartType = SeriesChartType.Column;

                // Add the series to the Chart
                chartInventory.Series.Add(series);

                // Set chart properties if needed
                chartInventory.ChartAreas[0].AxisX.Interval = 1;

                // Refresh the Chart to apply changes
                chartInventory.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error displaying chart data: " + ex.Message);
            }
        }


        // Add a new row to the DataTable
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Open the connection
                sqlConnection.Open();

                // Update the database with changes from the DataTable
                dataAdapter.Update(dataTable);

                // Refresh the DataTable to reflect changes
                dataTable.Clear();
                dataAdapter.Fill(dataTable);

                MessageBox.Show("Item added successfully!");
                // Update the chart with the latest data
                InitializeChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving changes: " + ex.Message);
            }
            finally
            {
                // Close the connection
                sqlConnection.Close();
            }
            LoadComboBoxData();
        }

        // Helper method to check if a DataRow is empty
        private bool IsRowEmpty(DataRow row)
        {
            foreach (var item in row.ItemArray)
            {
                if (item != null && item != DBNull.Value && !string.IsNullOrWhiteSpace(item.ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        // Save changes to the database
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Open the connection
                sqlConnection.Open();

                // Update the database with changes from the DataTable
                dataAdapter.Update(dataTable);

                // Refresh the DataTable to reflect changes
                dataTable.Clear();
                dataAdapter.Fill(dataTable);

                MessageBox.Show("Changes saved successfully!");
                // Update the chart with the latest data
                InitializeChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving changes: " + ex.Message);
            }
            finally
            {
                // Close the connection
                sqlConnection.Close();
            }
            LoadComboBoxData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ComboItemCode.SelectedItem == null)
            {
                MessageBox.Show("Please select an item to delete.");
                return;
            }

            int selectedItemID = Convert.ToInt32(ComboItemCode.SelectedItem.ToString());

            try
            {
                sqlConnection.Open();

                // Use a parameterized query to delete the record based on ItemID
                string deleteQuery = "DELETE FROM Inventory WHERE ItemID = @ItemID";
                SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlConnection);
                deleteCommand.Parameters.AddWithValue("@ItemID", selectedItemID);

                // Execute the delete command
                int rowsAffected = deleteCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Record deleted successfully!");

                    // Reload data to refresh DataGridView and Chart
                    
                }
                else
                {
                    MessageBox.Show("Record not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting record: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
                LoadData();
                InitializeChart();
                LoadComboBoxData();
            }
        }
    }
}
