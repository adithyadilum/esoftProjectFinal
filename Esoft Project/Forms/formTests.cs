using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Esoft_Project.Forms
{
    public partial class formTests : Form
    {
        SqlConnection sqlConnection = new SqlConnection("Data Source=ROG-STRIX-G15;Initial Catalog=HMSdb;Integrated Security=True;Encrypt=True; TrustServerCertificate=true;");
        public formTests()
        {
            InitializeComponent();
        }

        private void formTests_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
        }

        private void LoadComboBoxData()
        {
            try
            {
                sqlConnection.Open();

                string queryAllEmpIDs = "SELECT empID FROM Users";
                SqlCommand sqlCommandAllEmpIDs = new SqlCommand(queryAllEmpIDs, sqlConnection);
                SqlDataReader reader = sqlCommandAllEmpIDs.ExecuteReader();

                while (reader.Read())
                {
                    ComboBoxEmpID.Items.Add(reader["empID"].ToString());
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
            try
            {
                sqlConnection.Open();

                string queryAllPatientIDs = "SELECT PatientID FROM PatientRecords";
                SqlCommand sqlCommandAllPatientIDs = new SqlCommand(queryAllPatientIDs, sqlConnection);
                SqlDataReader reader = sqlCommandAllPatientIDs.ExecuteReader();

                while (reader.Read())
                {
                    comboboxPatientID.Items.Add(reader["PatientID"].ToString());
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
            try
            {
                sqlConnection.Open();

                string queryAllReportCodes = "SELECT ReportCode FROM Reports";
                SqlCommand sqlCommandAllReportCodes = new SqlCommand(queryAllReportCodes, sqlConnection);
                SqlDataReader reader = sqlCommandAllReportCodes.ExecuteReader();

                while (reader.Read())
                {
                    comboboxReportCode.Items.Add(reader["ReportCode"].ToString());
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
            try
            {
                sqlConnection.Open();

                string queryAllTestTypes = "SELECT TestType FROM MedicalTests";
                SqlCommand sqlCommandAllTestTypes = new SqlCommand(queryAllTestTypes, sqlConnection);
                SqlDataReader reader = sqlCommandAllTestTypes.ExecuteReader();

                while (reader.Read())
                {
                    comboboxTestType.Items.Add(reader["TestType"].ToString());
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

        private void comboboxReportCode_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboboxReportCode.SelectedItem == null)
            {
                return;
            }

            string selectedValue = comboboxReportCode.SelectedItem.ToString();

            try
            {
                sqlConnection.Open();

                // Retrieve user data based on the selected comboboxAlertID
                string query = "SELECT Date empID PatientID PriorityLevel TestType BillingStatus Status FROM Reports WHERE ReportCode = @SelectedValue";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@SelectedValue", selectedValue);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    // Populate the form controls with the retrieved data
                    ComboBoxEmpID.SelectedItem = GetString(reader, "empID");
                    comboboxPatientID.SelectedItem = GetString(reader, "PatientID");
                    comboboxTestType.SelectedItem = GetString(reader, "TestType");
                    comboBoxPriority.SelectedItem = GetString(reader, "PriorityLevel");
                    comboBoxBilling.SelectedItem = GetString(reader, "BillingStatus");
                    DatePicker.Value = GetDateTime(reader, "Date");
                    if (GetString(reader, "Status") == "Requested")
                    {
                        rbtnRequested.Checked = true;
                        rbtnInProgress.Checked = false;
                        rbtnCompleted.Checked = false;
                    }
                    else if (GetString(reader, "Status") == "InProgress")
                    {
                        rbtnRequested.Checked = false;
                        rbtnInProgress.Checked = false;
                        rbtnCompleted.Checked = true;
                    }
                    else
                    {
                        rbtnRequested.Checked = false;
                        rbtnInProgress.Checked = true;
                        rbtnCompleted.Checked = false;
                    }
                    MessageBox.Show("Data loaded successfully!");
                }
                else
                {
                    MessageBox.Show("No data found for the selected Report Code!");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error: {ex.Message}\n{ex.StackTrace}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving additional data: {ex.Message}\n{ex.StackTrace}");
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
                int columnIndex;
                try
                {
                    columnIndex = reader.GetOrdinal(columnName);
                }
                catch (IndexOutOfRangeException)
                {
                    // Handle the case where the column is not present in the result set
                    MessageBox.Show($"Column not found: {columnName}");
                    return string.Empty;
                }

                if (!reader.IsDBNull(columnIndex))
                {
                    if (reader.GetFieldType(columnIndex) == typeof(bool))
                    {
                        return reader.GetBoolean(columnIndex).ToString();
                    }
                    else if (reader.GetFieldType(columnIndex) == typeof(decimal))
                    {
                        return reader.GetDecimal(columnIndex).ToString();
                    }

                    return reader.GetString(columnIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving data for column {columnName}: {ex.Message}\n{ex.StackTrace}");
            }

            return string.Empty;
        }



        // Helper method to get boolean value from SqlDataReader
        private bool GetBoolean(SqlDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return reader.IsDBNull(columnIndex) ? false : reader.GetBoolean(columnIndex);
        }

        // Helper method to get DateTime value from SqlDataReader
        private DateTime GetDateTime(SqlDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return reader.IsDBNull(columnIndex) ? DateTime.MinValue : reader.GetDateTime(columnIndex);
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

        private void ComboBoxEmpID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxEmpID.SelectedItem == null)
            {
                return;
            }

            string selectedEmpID = ComboBoxEmpID.SelectedItem.ToString();

            try
            {
                sqlConnection.Open();

                // Retrieve patient data based on the selected PatientID
                string query = "SELECT Name FROM Users WHERE empID = @SelectedEmpID";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@SelectedEmpID", selectedEmpID);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    // Auto-fill the patient name text box with the retrieved data
                    txtempName.Text = GetString(reader, "Name");
                }
                else
                {
                    // Clear the text box if no data is found for the selected PatientID
                    txtempName.Text = "";
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
            finally
            {
                sqlConnection.Close();
            }
        }

        private void comboboxTestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboboxTestType.SelectedItem == null)
            {
                return;
            }

            string selectedTestType = comboboxTestType.SelectedItem.ToString();

            try
            {
                sqlConnection.Open();

                // Retrieve patient data based on the selected PatientID
                string query = "SELECT Description, Cost FROM MedicalTests WHERE TestType = @SelectedTestType";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@SelectedTestType", selectedTestType);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    txtDescription.Text = GetString(reader, "Description");
                    txtCost.Text = GetString(reader, "Cost");
                }
                else
                {

                    txtDescription.Text = "";
                    txtCost.Text = "";
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
            finally
            {
                sqlConnection.Close();
            }
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            try
            {// Check if any of the required ComboBoxes is not selected
                if (comboboxReportCode.Text == null || ComboBoxEmpID.SelectedItem == null || comboboxPatientID.SelectedItem == null ||
                    comboboxTestType.SelectedItem == null || comboBoxPriority.SelectedItem == null ||
                    comboBoxBilling.SelectedItem == null)
                {
                    MessageBox.Show("Please select values for all required fields before submitting a request.");
                    return;
                }
                sqlConnection.Open();

                // Insert data into the database
                string query = "INSERT INTO Reports (ReportCode, Date, empID, PatientID, PriorityLevel, TestType, BillingStatus, Status) " +
                               "VALUES (@ReportCode, @Date, @EmpID, @PatientID, @PriorityLevel, @TestType, @BillingStatus, @Status)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Set parameters with the values from the form controls
                sqlCommand.Parameters.AddWithValue("@ReportCode", comboboxReportCode.Text.ToString());
                sqlCommand.Parameters.AddWithValue("@Date", DatePicker.Value);
                sqlCommand.Parameters.AddWithValue("@EmpID", ComboBoxEmpID.SelectedItem.ToString());
                sqlCommand.Parameters.AddWithValue("@PatientID", comboboxPatientID.SelectedItem.ToString());
                sqlCommand.Parameters.AddWithValue("@PriorityLevel", comboBoxPriority.SelectedItem.ToString());
                sqlCommand.Parameters.AddWithValue("@TestType", comboboxTestType.SelectedItem.ToString());
                sqlCommand.Parameters.AddWithValue("@BillingStatus", comboBoxBilling.SelectedItem.ToString());

                // Determine the Status based on radio button selection
                if (rbtnRequested.Checked)
                {
                    sqlCommand.Parameters.AddWithValue("@Status", "Requested");
                }
                else if (rbtnInProgress.Checked)
                {
                    sqlCommand.Parameters.AddWithValue("@Status", "InProgress");
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("@Status", "Completed");
                }

                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Data inserted successfully!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error: {ex.Message}\n{ex.StackTrace}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting data: {ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            try
            { // Check if the ComboBox or radio button is not selected
                if (comboboxReportCode.SelectedItem == null)
                    sqlConnection.Open();

                // Update Status in the database
                string query = "UPDATE Reports SET Status = @Status WHERE ReportCode = @ReportCode";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Set parameters with the values from the form controls
                sqlCommand.Parameters.AddWithValue("@Status", rbtnRequested.Checked ? "Requested" : rbtnInProgress.Checked ? "InProgress" : "Completed");
                sqlCommand.Parameters.AddWithValue("@ReportCode", comboboxReportCode.SelectedItem.ToString());

                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Status updated successfully!");
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
                sqlConnection.Close();
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
