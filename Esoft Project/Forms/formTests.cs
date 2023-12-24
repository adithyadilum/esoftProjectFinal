using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Esoft_Project.Forms
{
    public partial class formTests : Form
    {
        SqlConnection sqlConnection = new SqlConnection("Data Source=ROG-STRIX-G15;Initial Catalog=HMSdb;Integrated Security=True;Encrypt=True; TrustServerCertificate=true;");
        string connectionString = "Data Source=ROG-STRIX-G15;Initial Catalog=HMSdb;Integrated Security=True;Encrypt=True; TrustServerCertificate=true;";
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
                    case "comboboxReportCode":
                        HandleReportCodeSelection();
                        break;

                    case "comboboxPatientID":
                        HandlePatientIDSelection();
                        break;

                    case "ComboBoxEmpID":
                        HandleEmpIDSelection();
                        break;

                    case "comboboxTestType":
                        HandleTestTypeSelection();
                        break;

                    // Add more cases for other ComboBoxes if needed

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

        private void HandleReportCodeSelection()
        {
            // Your logic for comboboxReportCode selection
            if (comboboxReportCode.SelectedItem == null)
            {
                return;
            }

            string selectedReportCode = comboboxReportCode.SelectedItem.ToString();

            try
            {
                // Open the connection only once for the entire method
                sqlConnection.Open();

                // Retrieve data based on the selected ReportCode
                string query = "SELECT Date, empID, PatientID, PriorityLevel, TestType, BillingStatus, Status " +
                               "FROM Reports WHERE ReportCode = @SelectedReportCode";
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@SelectedReportCode", selectedReportCode);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Auto-fill other ComboBoxes and TextBoxes with the retrieved data
                            DatePicker.Value = GetDateTime(reader, "Date");
                            ComboBoxEmpID.SelectedItem = GetString(reader, "empID");
                            comboboxPatientID.SelectedItem = GetString(reader, "PatientID");
                            comboBoxPriority.SelectedItem = GetString(reader, "PriorityLevel");
                            comboboxTestType.SelectedItem = GetString(reader, "TestType");
                            comboBoxBilling.SelectedItem = GetString(reader, "BillingStatus");

                            // Set radio button based on the Status
                            string status = GetString(reader, "Status");
                            rbtnRequested.Checked = status == "Requested";
                            rbtnInProgress.Checked = status == "InProgress";
                            rbtnCompleted.Checked = status == "Completed";
                        }
                        else
                        {
                            // Clear other ComboBoxes and TextBoxes if no data is found for the selected ReportCode
                            DatePicker.Value = DateTime.Now; // Set a default value for the date, you can change it accordingly
                            ComboBoxEmpID.SelectedItem = null;
                            comboboxPatientID.SelectedItem = null;
                            comboBoxPriority.SelectedItem = null;
                            comboboxTestType.SelectedItem = null;
                            comboBoxBilling.SelectedItem = null;

                            // Clear radio button selection
                            rbtnRequested.Checked = false;
                            rbtnInProgress.Checked = false;
                            rbtnCompleted.Checked = false;
                        }
                    }
                }
                // Retrieve data from MedicalTests based on TestType
                string testType = comboboxTestType.SelectedItem.ToString();
                string medicalTestsQuery = "SELECT Description, Cost FROM MedicalTests WHERE TestType = @TestType";
                using (SqlCommand medicalTestsCommand = new SqlCommand(medicalTestsQuery, sqlConnection))
                {
                    medicalTestsCommand.Parameters.AddWithValue("@TestType", testType);

                    using (SqlDataReader medicalTestsReader = medicalTestsCommand.ExecuteReader())
                    {
                        if (medicalTestsReader.Read())
                        {
                            // Auto-fill txtDescription and txtCost with data from MedicalTests
                            txtDescription.Text = GetString(medicalTestsReader, "Description");
                            txtCost.Text = GetString(medicalTestsReader, "Cost");
                        }
                        else
                        {
                            // Clear txtDescription and txtCost if no data is found for the selected TestType
                            txtDescription.Text = "";
                            txtCost.Text = "";
                        }
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
                                // Auto-fill txtName with data from PatientRecords
                                txtName.Text = GetString(patientReader, "Name");
                            }
                            else
                            {
                                // Clear txtName if no data is found for the selected PatientID
                                txtName.Text = "";
                            }
                        }
                    }
                }

                // Retrieve data from Users based on empID
                if (ComboBoxEmpID.SelectedItem != null)
                {
                    string selectedEmpID = ComboBoxEmpID.SelectedItem.ToString();
                    string userQuery = "SELECT Name FROM Users WHERE empID = @SelectedEmpID";
                    using (SqlCommand userCommand = new SqlCommand(userQuery, sqlConnection))
                    {
                        userCommand.Parameters.AddWithValue("@SelectedEmpID", selectedEmpID);

                        using (SqlDataReader userReader = userCommand.ExecuteReader())
                        {
                            if (userReader.Read())
                            {
                                // Auto-fill txtempName with data from Users
                                txtempName.Text = GetString(userReader, "Name");
                            }
                            else
                            {
                                // Clear txtempName if no data is found for the selected empID
                                txtempName.Text = "";
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
                    string query = "SELECT Name FROM Users WHERE empID = @SelectedEmpID";
                    using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                    {
                        sqlCommand.Parameters.AddWithValue("@SelectedEmpID", selectedEmpID);

                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Auto-fill the patient name text box with the retrieved data
                                txtempName.Text = GetString(reader, "Name");
                            }
                            else
                            {
                                // Clear the text box if no data is found for the selected EmpID
                                txtempName.Text = "";
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

        private void HandleTestTypeSelection()
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
                    else if (reader.GetFieldType(columnIndex) == typeof(int))
                    {
                        return reader.GetInt32(columnIndex).ToString();
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

        // Helper method to get DateTime value from SqlDataReader
        private DateTime GetDateTime(SqlDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return reader.IsDBNull(columnIndex) ? DateTime.MinValue : reader.GetDateTime(columnIndex);
        }

        private bool IsReportCodeValid(string reportCode)
        {
            // Check if the ReportCode already exists in the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Reports WHERE ReportCode = @ReportCode";
                using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@ReportCode", reportCode);

                    int count = (int)sqlCommand.ExecuteScalar();

                    // If count is greater than 0, the ReportCode already exists
                    return count == 0;
                }
            }
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if any of the required ComboBoxes is not selected
                if (string.IsNullOrEmpty(comboboxReportCode.Text) ||
                    ComboBoxEmpID.SelectedItem == null ||
                    comboboxPatientID.SelectedItem == null ||
                    comboboxTestType.SelectedItem == null ||
                    comboBoxPriority.SelectedItem == null ||
                    comboBoxBilling.SelectedItem == null)
                {
                    MessageBox.Show("Please select values for all required fields before submitting a request.");
                    return;
                }

                // Check if ReportCode is valid
                string reportCode = comboboxReportCode.Text.Trim();
                if (!IsReportCodeValid(reportCode))
                {
                    MessageBox.Show("ReportCode already exists in the database. Please enter a different ReportCode.");
                    return;
                }

                sqlConnection.Open();

                // Insert data into the database
                string query = "INSERT INTO Reports (ReportCode, Date, empID, PatientID, PriorityLevel, TestType, BillingStatus, Status) " +
                               "VALUES (@ReportCode, @Date, @EmpID, @PatientID, @PriorityLevel, @TestType, @BillingStatus, @Status)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Set parameters with the values from the form controls
                sqlCommand.Parameters.AddWithValue("@ReportCode", reportCode);
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
                LoadComboBoxData();
            }
        }


        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if the ComboBox or radio button is not selected
                if (comboboxReportCode.SelectedItem == null)
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
                string query = "UPDATE Reports SET Status = @Status WHERE ReportCode = @ReportCode";
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    // Set parameters with the values from the form controls
                    sqlCommand.Parameters.AddWithValue("@Status", rbtnRequested.Checked ? "Requested" : rbtnInProgress.Checked ? "InProgress" : "Completed");
                    sqlCommand.Parameters.AddWithValue("@ReportCode", comboboxReportCode.SelectedItem.ToString());

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
