using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Esoft_Project.Forms
{
    public partial class formBilling : Form
    {
        SqlConnection sqlConnection = new SqlConnection("Data Source=ROG-STRIX-G15;Initial Catalog=HMSdb;Integrated Security=True;Encrypt=True; TrustServerCertificate=true;");
        string connectionString = "Data Source=ROG-STRIX-G15;Initial Catalog=HMSdb;Integrated Security=True;Encrypt=True; TrustServerCertificate=true;";
        public formBilling()
        {
            InitializeComponent();
        }
        private string selectedInvoiceNumber;
        private void formBilling_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
        }

        private void LoadComboBoxData()
        {
            try
            {
                sqlConnection.Open();

                // Load empIDs into ComboBoxEmpID
                string queryAllEmpIDs = "SELECT empID FROM Users";
                SqlCommand sqlCommandAllEmpIDs = new SqlCommand(queryAllEmpIDs, sqlConnection);
                SqlDataReader readerEmpIDs = sqlCommandAllEmpIDs.ExecuteReader();

                while (readerEmpIDs.Read())
                {
                    ComboBoxEmpID.Items.Add(readerEmpIDs["empID"].ToString());
                }

                readerEmpIDs.Close();

                // Load patientIDs into comboboxPatientID
                string queryAllPatientIDs = "SELECT PatientID FROM PatientRecords";
                SqlCommand sqlCommandAllPatientIDs = new SqlCommand(queryAllPatientIDs, sqlConnection);
                SqlDataReader readerPatientIDs = sqlCommandAllPatientIDs.ExecuteReader();

                while (readerPatientIDs.Read())
                {
                    comboboxPatientID.Items.Add(readerPatientIDs["PatientID"].ToString());
                }

                readerPatientIDs.Close();

                // Load invoiceNumbers into comboboxInvoiceNo
                string queryAllInvoiceNumbers = "SELECT InvoiceNumber FROM Billing";
                SqlCommand sqlCommandAllInvoiceNumbers = new SqlCommand(queryAllInvoiceNumbers, sqlConnection);
                SqlDataReader readerInvoiceNumbers = sqlCommandAllInvoiceNumbers.ExecuteReader();

                while (readerInvoiceNumbers.Read())
                {
                    comboboxInvoiceNo.Items.Add(readerInvoiceNumbers["InvoiceNumber"].ToString());
                }

                readerInvoiceNumbers.Close();
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

        private bool isHandlingSelection = false;
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isHandlingSelection)
            {
                // Another selection is already being handled, exit to avoid conflicts
                return;
            }

            isHandlingSelection = true;

            try
            {
                ComponentFactory.Krypton.Toolkit.KryptonComboBox comboBox = (ComponentFactory.Krypton.Toolkit.KryptonComboBox)sender;

                switch (comboBox.Name)
                {
                    case "comboboxInvoiceNo":
                        HandleInvoiceNoSelection();
                        break;

                    case "comboboxPatientID":
                        HandlePatientIDSelection();
                        break;

                    case "ComboBoxEmpID":
                        HandleEmpIDSelection();
                        break;

                    default:
                        // Handle unexpected case
                        break;
                }
            }
            finally
            {
                isHandlingSelection = false;
            }
        }
        private void HandleInvoiceNoSelection()
        {

            if (comboboxInvoiceNo.SelectedItem == null)
            {
                return;
            }

            selectedInvoiceNumber = comboboxInvoiceNo.SelectedItem.ToString();

            try
            {
                sqlConnection.Open();

                // Retrieve general data based on the selected InvoiceNumber
                string queryGeneralData = "SELECT empID, PatientID, Date, BillingStatus, Notes, Total FROM Billing WHERE InvoiceNumber = @SelectedInvoiceNumber";
                using (SqlCommand sqlCommandGeneralData = new SqlCommand(queryGeneralData, sqlConnection))
                {
                    sqlCommandGeneralData.Parameters.AddWithValue("@SelectedInvoiceNumber", selectedInvoiceNumber);

                    using (SqlDataReader readerGeneralData = sqlCommandGeneralData.ExecuteReader())
                    {

                        if (readerGeneralData.Read())
                        {

                            // Set the selected items of other controls with the retrieved data
                            ComboBoxEmpID.SelectedItem = GetString(readerGeneralData, "empID");
                            comboboxPatientID.SelectedItem = GetString(readerGeneralData, "PatientID");
                            comboBoxBillingStatus.SelectedItem = GetString(readerGeneralData, "BillingStatus");
                            txtNotes.Text = GetString(readerGeneralData, "Notes");
                            txtTotal.Text = GetString(readerGeneralData, "Total");
                            datepicker.Value = GetDateTime(readerGeneralData, "Date");

                        }
                        else
                        {
                            MessageBox.Show("No data found for the selected Invoice Number!");
                            // Clear other controls if no data is found for the selected InvoiceNumber
                            ComboBoxEmpID.SelectedItem = null;
                            comboboxPatientID.SelectedItem = null;
                            comboBoxBillingStatus.SelectedItem = null;
                            txtNotes.Text = "";
                            txtTotal.Text = "";
                            datepicker.Value = DateTime.Now; // Set to default value
                        }
                    }
                    // Retrieve detailed data for the DataGridView
                    string queryDetailsData = "SELECT r.TestType, mt.Cost FROM Reports r " +
                                               "JOIN MedicalTests mt ON r.TestType = mt.TestType " +
                                               "WHERE r.PatientID = (SELECT PatientID FROM Billing WHERE InvoiceNumber = @SelectedInvoiceNumber)";
                    using (SqlCommand sqlCommandDetailsData = new SqlCommand(queryDetailsData, sqlConnection))
                    {
                        sqlCommandDetailsData.Parameters.AddWithValue("@SelectedInvoiceNumber", selectedInvoiceNumber);

                        using (SqlDataReader readerDetailsData = sqlCommandDetailsData.ExecuteReader())
                        {
                            // Clear previous data in DataGridView1
                            DataGridView1.Rows.Clear();

                            decimal totalCost = 0;

                            while (readerDetailsData.Read())
                            {
                                string testType = GetString(readerDetailsData, "TestType");
                                decimal unitPrice = GetDecimal(readerDetailsData, "Cost");
                                int quantity = 1;
                                decimal cost = unitPrice * quantity;

                                // Add a new row to the DataGridView
                                DataGridView1.Rows.Add(testType, unitPrice, quantity, cost);

                                // Update the total cost
                                totalCost += cost;
                            }

                            DataGridView1.Rows.Add("Room and Board", 35000.00, 1, 35000.00);
                            totalCost = totalCost + 35000;
                            DataGridView1.Rows.Add("Doctor's Fees (per consultation)", 2000.00, 4, 8000.00);
                            totalCost = totalCost + 8000;
                            DataGridView1.Rows.Add("Medical Supplies", 2000.00, 1, 8000.00);
                            totalCost = totalCost + 2000;
                            DataGridView1.Rows.Add("Administrative Fees", 500.00, 1, 500.00);
                            totalCost = totalCost + 500;
                            // Update txtTotal with the calculated total cost
                            txtTotal.Text = totalCost.ToString("C"); // Format as currency
                        }
                    }

                    // Retrieve data from PatientRecords based on PatientID
                    if (comboboxPatientID.SelectedItem != null)
                    {
                        string selectedPatientID = comboboxPatientID.SelectedItem.ToString();
                        string patientQuery = "SELECT Name FROM PatientRecords WHERE PatientID = @SelectedPatientID";
                        using (SqlCommand patientCommand = new SqlCommand(patientQuery, sqlConnection))
                        {
                            patientCommand.Parameters.AddWithValue("@SelectedPatientID", selectedPatientID);

                            using (SqlDataReader patientReader = patientCommand.ExecuteReader())
                            {
                                if (patientReader.Read())
                                {
                                    //Auto - fill txtName with data from PatientRecords
                                    txtName.Text = GetString(patientReader, "Name");
                                }
                                else
                                {
                                    //Clear txtName if no data is found for the selected PatientID

                                    txtName.Text = "";
                                }
                            }
                        }
                    }

                    // Retrieve data from Users based on empID
                    if (ComboBoxEmpID.SelectedItem != null)
                    {
                        string selectedEmpID = ComboBoxEmpID.SelectedItem.ToString();
                        string userQuery = "SELECT Name, Phone FROM Users WHERE empID = @SelectedEmpID";
                        using (SqlCommand userCommand = new SqlCommand(userQuery, sqlConnection))
                        {
                            userCommand.Parameters.AddWithValue("@SelectedEmpID", selectedEmpID);

                            using (SqlDataReader userReader = userCommand.ExecuteReader())
                            {
                                if (userReader.Read())
                                {
                                    //Auto - fill txtempName with data from Users
                                    txtempName.Text = GetString(userReader, "Name");
                                    txtempPhone.Text = GetString(userReader, "Phone");
                                }
                                else
                                {
                                    //Clear txtempName if no data is found for the selected empID

                                    txtempName.Text = "";
                                    txtempPhone.Text = "";
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error: {ex.Message}\n{ex.StackTrace}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving report data: {ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                // Close the connection only once at the end of the method
                sqlConnection.Close();
                MessageBox.Show("Data loaded successfully!");
            }
        }

        private void HandlePatientIDSelection()
        {
            // Your logic for comboboxPatientID selection
            if (comboboxPatientID.SelectedItem == null)
            {
                return;
            }

            string selectedPatientID = comboboxPatientID.SelectedItem.ToString();

            try
            {
                // Check if the connection is already open before trying to open it
                if (sqlConnection.State != ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                // Retrieve patient data based on the selected PatientID
                string query = "SELECT Name FROM PatientRecords WHERE PatientID = @SelectedPatientID";
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@SelectedPatientID", selectedPatientID);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Auto-fill the patient name text box with the retrieved data
                            txtName.Text = GetString(reader, "Name");
                        }
                        else
                        {
                            // Clear the text box if no data is found for the selected PatientID
                            txtName.Text = "";
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error: {ex.Message}\n{ex.StackTrace}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving patient data: {ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                // Close the connection only if it was opened in this block
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        private void HandleEmpIDSelection()
        {
            // Your logic for ComboBoxEmpID selection
            if (ComboBoxEmpID.SelectedItem == null)
            {
                return;
            }

            string selectedEmpID = ComboBoxEmpID.SelectedItem.ToString();

            try
            {
                // Using statement ensures that the connection is closed properly
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Rest of your code to retrieve data
                    string query = "SELECT Name, Phone FROM Users WHERE empID = @SelectedEmpID";
                    using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                    {
                        sqlCommand.Parameters.AddWithValue("@SelectedEmpID", selectedEmpID);

                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Auto-fill the patient name text box with the retrieved data
                                txtempName.Text = GetString(reader, "Name");
                                txtempPhone.Text = GetString(reader, "Phone");
                            }
                            else
                            {
                                // Clear the text box if no data is found for the selected EmpID
                                txtempName.Text = "";
                                txtempPhone.Text = "";
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error: {ex.Message}\n{ex.StackTrace}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving user data: {ex.Message}\n{ex.StackTrace}");
            }
        }


        private string GetString(SqlDataReader reader, string columnName)
        {
            try
            {
                if (!reader.IsClosed)
                {
                    int columnIndex = reader.GetOrdinal(columnName);

                    if (!reader.IsDBNull(columnIndex))
                    {
                        // Check if the data type is string
                        if (reader.GetFieldType(columnIndex) == typeof(string))
                        {
                            return reader.GetString(columnIndex);
                        }
                        else
                        {
                            // Convert non-string types to string
                            return reader.GetValue(columnIndex).ToString();
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                // Handle the case where the SqlDataReader is closed
                MessageBox.Show($"Error retrieving data: {ex.Message}\n{ex.StackTrace}");
            }

            return string.Empty;
        }

        // Helper method to get DateTime value from SqlDataReader
        private DateTime GetDateTime(SqlDataReader reader, string columnName)
        {
            try
            {
                if (!reader.IsClosed)
                {
                    int columnIndex = reader.GetOrdinal(columnName);
                    return reader.IsDBNull(columnIndex) ? DateTime.MinValue : reader.GetDateTime(columnIndex);
                }
            }
            catch (InvalidOperationException ex)
            {
                // Handle the case where the SqlDataReader is closed
                MessageBox.Show($"Error retrieving data: {ex.Message}\n{ex.StackTrace}");
            }

            return DateTime.MinValue;
        }

        // Helper method to get decimal value from SqlDataReader
        private decimal GetDecimal(SqlDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return reader.IsDBNull(columnIndex) ? 0 : reader.GetDecimal(columnIndex);
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if the ComboBox or radio button is not selected
                if (comboBoxBillingStatus.SelectedItem == null)
                {
                    MessageBox.Show("Please select a ReportCode before updating the status.");
                    return;
                }

                // Open the connection only if it is not already open
                if (sqlConnection.State != ConnectionState.Open)
                {
                    sqlConnection.Open();
                }

                // Update Status in the database
                string query = "UPDATE Billing SET BillingStatus = @BillingStatus, Notes = @Notes WHERE InvoiceNumber = @InvoiceNumber";
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    // Set parameters with the values from the form controls
                    sqlCommand.Parameters.AddWithValue("@BillingStatus", comboBoxBillingStatus.SelectedItem.ToString());
                    sqlCommand.Parameters.AddWithValue("@Notes", txtNotes.Text);
                    sqlCommand.Parameters.AddWithValue("@InvoiceNumber", comboboxInvoiceNo.SelectedItem.ToString());

                    sqlCommand.ExecuteNonQuery();
                    MessageBox.Show("Status updated successfully!");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error: {ex.Message}\n{ex.StackTrace}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating status: {ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                // Close the connection only if it was opened in this block
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }

        private PrintDocument printDocument = new PrintDocument();
        private void btnPrint_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.PrintDialog printDialog = new System.Windows.Forms.PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }
    }
}
