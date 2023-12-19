using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Esoft_Project.Forms
{
    public partial class formBilling : Form
    {
        SqlConnection sqlConnection = new SqlConnection("Data Source=ROG-STRIX-G15;Initial Catalog=HMSdb;Integrated Security=True;Encrypt=True; TrustServerCertificate=true;");
        public formBilling()
        {
            InitializeComponent();
        }

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

        private string selectedInvoiceNumber;
        private void comboboxInvoiceNo_SelectedIndexChanged(object sender, EventArgs e)
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
                    SqlCommand sqlCommandGeneralData = new SqlCommand(queryGeneralData, sqlConnection);
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
                    LoadDataGridViewData();
               
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error: {ex.Message}\n{ex.StackTrace}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving data for the selected Invoice Number: {ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                    sqlConnection.Close();
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

        private void comboboxPatientID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboboxPatientID.SelectedItem == null)
            {
                return;
            }

            string selectedPatientID = comboboxPatientID.SelectedItem.ToString();

            try
            {
                
                    sqlConnection.Open();

                    // Retrieve patient data based on the selected PatientID
                    string query = "SELECT Name FROM PatientRecords WHERE PatientID = @SelectedPatientID";
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@SelectedPatientID", selectedPatientID);

                    SqlDataReader reader = sqlCommand.ExecuteReader();

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
               
                    sqlConnection.Close();
                
            }
        }


        

       
        private void LoadDataGridViewData()
        {
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();

                    // Retrieve detailed data based on the selected InvoiceNumber
                    string queryDetailsData = "SELECT r.TestType, mt.Cost FROM Reports r " +
                                               "JOIN MedicalTests mt ON r.TestType = mt.TestType " +
                                               "WHERE r.PatientID = (SELECT PatientID FROM Billing WHERE InvoiceNumber = @SelectedInvoiceNumber)";
                    SqlCommand sqlCommandDetailsData = new SqlCommand(queryDetailsData, sqlConnection);
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

                        // Update txtTotal with the calculated total cost
                        txtTotal.Text = totalCost.ToString("C"); // Format as currency
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error: {ex.Message}\n{ex.StackTrace}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving detailed data for the selected Invoice Number: {ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }
    }
}
