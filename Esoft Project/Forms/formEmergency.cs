using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Esoft_Project.Forms
{
    public partial class formEmergency : Form
    {
        SqlConnection sqlConnection = new SqlConnection("Data Source=ROG-STRIX-G15;Initial Catalog=HMSdb;Integrated Security=True;Encrypt=True; TrustServerCertificate=true;");
        string connectionString = "Data Source=ROG-STRIX-G15;Initial Catalog=HMSdb;Integrated Security=True;Encrypt=True; TrustServerCertificate=true;";
        public formEmergency()
        {
            InitializeComponent();
        }

        private void formEmergency_Load(object sender, EventArgs e)
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

                string queryAllAlertIDs = "SELECT AlertID FROM Alerts";
                SqlCommand sqlCommandAllAlertIDs = new SqlCommand(queryAllAlertIDs, sqlConnection);
                SqlDataReader reader = sqlCommandAllAlertIDs.ExecuteReader();

                while (reader.Read())
                {
                    comboboxAlertID.Items.Add(reader["AlertID"].ToString());
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

                string queryAllEmergencyCodes = "SELECT EmergencyCode FROM Emergency";
                SqlCommand sqlCommandAllEmergencyCodes = new SqlCommand(queryAllEmergencyCodes, sqlConnection);
                SqlDataReader reader = sqlCommandAllEmergencyCodes.ExecuteReader();

                while (reader.Read())
                {
                    comboboxEmergencyCode.Items.Add(reader["EmergencyCode"].ToString());
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
                    case "comboboxAlertID":
                        HandleReportCodeSelection();
                        break;

                    case "comboboxPatientID":
                        HandlePatientIDSelection();
                        break;

                    case "ComboBoxEmpID":
                        HandleEmpIDSelection();
                        break;

                    case "comboboxEmergencyCode":
                        HandleEmergencyCodeSelection();
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
            if (comboboxAlertID.SelectedItem == null)
            {
                return;
            }

            string selectedValue = comboboxAlertID.SelectedItem.ToString();

            try
            {
                sqlConnection.Open();

                // Retrieve user data based on the selected comboboxAlertID
                string query = "SELECT Date, empID, PatientID, SeverityLevel, EmergencyCode FROM Alerts WHERE AlertID = @SelectedValue";
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@SelectedValue", selectedValue);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            // Populate the form controls with the retrieved data
                            ComboBoxEmpID.SelectedItem = GetString(reader, "empID");
                            comboboxPatientID.SelectedItem = GetString(reader, "PatientID");
                            comboboxEmergencyCode.SelectedItem = GetString(reader, "EmergencyCode");
                            DatePicker.Value = GetDateTime(reader, "Date");
                            if (GetString(reader, "SeverityLevel") == "Low")
                            {
                                rbtnLow.Checked = true;
                                rbtnHigh.Checked = false;
                                rbtnModerate.Checked = false;
                            }
                            else if (GetString(reader, "SeverityLevel") == "Moderate")
                            {
                                rbtnLow.Checked = false;
                                rbtnHigh.Checked = false;
                                rbtnModerate.Checked = true;
                            }
                            else
                            {
                                rbtnLow.Checked = false;
                                rbtnHigh.Checked = true;
                                rbtnModerate.Checked = false;
                            }

                        }
                        else
                        {
                            MessageBox.Show("No data found for the selected Alert ID!");
                        }
                    }

                    // Retrieve data from EmergencyCode based on TestType
                    string SelectedEmergencyCode = comboboxEmergencyCode.SelectedItem.ToString();
                    string EmergencyCodeQuery = "SELECT EmergencyType, ColorCode FROM Emergency WHERE EmergencyCode = @SelectedEmergencyCode";
                    using (SqlCommand EmergencyCodeCommand = new SqlCommand(EmergencyCodeQuery, sqlConnection))
                    {
                        EmergencyCodeCommand.Parameters.AddWithValue("@SelectedEmergencyCode", SelectedEmergencyCode);

                        using (SqlDataReader EmergencyCodeReader = EmergencyCodeCommand.ExecuteReader())
                        {
                            if (EmergencyCodeReader.Read())
                            {
                                // Auto-fill the text box with the EmergencyType
                                txtEmergencyType.Text = GetString(EmergencyCodeReader, "EmergencyType");
                                string colorCodeString = GetString(EmergencyCodeReader, "ColorCode");
                                Color backgroundColor = Color.FromArgb(
                             Convert.ToInt32(colorCodeString.Split(',')[0]), // Red
                             Convert.ToInt32(colorCodeString.Split(',')[1]), // Green
                             Convert.ToInt32(colorCodeString.Split(',')[2])); // Blue 
                                                                              // Set additional properties for the text box
                                txtEmergencyType.StateCommon.Back.Color1 = backgroundColor;
                                txtEmergencyType.StateCommon.Content.Color1 = Color.White;
                                txtEmergencyType.StateCommon.Content.Font = new Font(txtEmergencyType.Font, FontStyle.Bold);
                            }
                            else
                            {

                                // Clear the text box if no data is found for the selected EmergencyCode
                                txtEmergencyType.Text = "";
                                txtEmergencyType.StateCommon.Back.Color1 = Color.FromArgb(247, 248, 243); // Set the default back color
                                txtEmergencyType.StateCommon.Content.Color1 = SystemColors.ControlText; // Set the default fore color
                                txtEmergencyType.StateCommon.Content.Font = new Font(txtEmergencyType.Font, FontStyle.Regular); // Set the default font style
                            }
                        }
                    }

                    //// Retrieve data from PatientRecords based on PatientID
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
                        string userQuery = "SELECT Name FROM Users WHERE empID = @SelectedEmpID";
                        using (SqlCommand userCommand = new SqlCommand(userQuery, sqlConnection))
                        {
                            userCommand.Parameters.AddWithValue("@SelectedEmpID", selectedEmpID);

                            using (SqlDataReader userReader = userCommand.ExecuteReader())
                            {
                                if (userReader.Read())
                                {
                                    //Auto - fill txtempName with data from Users
                                    txtempName.Text = GetString(userReader, "Name");
                                }
                                else
                                {
                                    //Clear txtempName if no data is found for the selected empID

                                    txtempName.Text = "";
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

        private void HandleEmergencyCodeSelection()
        {
            if (comboboxEmergencyCode.SelectedItem == null)
            {
                return;
            }

            string selectedEmergencyCode = comboboxEmergencyCode.SelectedItem.ToString();

            try
            {
                sqlConnection.Open();

                // Retrieve emergency data based on the selected EmergencyCode
                string query = "SELECT EmergencyType, ColorCode FROM Emergency WHERE EmergencyCode = @SelectedEmergencyCode";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@SelectedEmergencyCode", selectedEmergencyCode);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    // Auto-fill the text box with the EmergencyType
                    txtEmergencyType.Text = GetString(reader, "EmergencyType");
                    string colorCodeString = GetString(reader, "ColorCode");
                    Color backgroundColor = Color.FromArgb(
                 Convert.ToInt32(colorCodeString.Split(',')[0]), // Red
                 Convert.ToInt32(colorCodeString.Split(',')[1]), // Green
                 Convert.ToInt32(colorCodeString.Split(',')[2])  // Blue
             );

                    // Set additional properties for the text box
                    txtEmergencyType.StateCommon.Back.Color1 = backgroundColor;
                    txtEmergencyType.StateCommon.Content.Color1 = Color.White;
                    txtEmergencyType.StateCommon.Content.Font = new Font(txtEmergencyType.Font, FontStyle.Bold);
                }
                else
                {
                    // Clear the text box if no data is found for the selected EmergencyCode
                    txtEmergencyType.Text = "";
                    txtEmergencyType.StateCommon.Back.Color1 = Color.FromArgb(247, 248, 243); // Set the default back color
                    txtEmergencyType.StateCommon.Content.Color1 = SystemColors.ControlText; // Set the default fore color
                    txtEmergencyType.StateCommon.Content.Font = new Font(txtEmergencyType.Font, FontStyle.Regular); // Set the default font style
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error: {ex.Message}\n{ex.StackTrace}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving emergency data: {ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        // Helper method to get string value from SqlDataReader
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

        private void btnInitiate_Click(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();

                // Insert a new record into the Alerts table
                string query = "INSERT INTO Alerts (Date, empID, PatientID, SeverityLevel, EmergencyCode) VALUES (@Date, @EmpID, @PatientID, @SeverityLevel, @EmergencyCode)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@Date", DatePicker.Value);
                sqlCommand.Parameters.AddWithValue("@EmpID", ComboBoxEmpID.Text);
                sqlCommand.Parameters.AddWithValue("@PatientID", comboboxPatientID.Text);

                // Assuming the SeverityLevel is stored as a string, adjust the SqlDbType if it's different
                sqlCommand.Parameters.AddWithValue("@SeverityLevel", rbtnLow.Checked ? "Low" : (rbtnModerate.Checked ? "Moderate" : "High"));

                sqlCommand.Parameters.AddWithValue("@EmergencyCode", comboboxEmergencyCode.Text);

                sqlCommand.ExecuteNonQuery();


                MessageBox.Show("Alert Initiated successfully!");

                // Clear form controls or perform other actions after deletion
                comboboxPatientID.Text = "";
                ComboBoxEmpID.Text = "";
                comboboxEmergencyCode.Text = "";
                DatePicker.Value = DateTime.Now;
                rbtnLow.Checked = false;
                rbtnModerate.Checked = false;
                rbtnHigh.Checked = false;



            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error: {ex.Message}\n{ex.StackTrace}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initiating a new alert: {ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                sqlConnection.Close();
                LoadComboBoxData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (comboboxAlertID.SelectedItem == null)
            {
                return;
            }

            string selectedValue = comboboxAlertID.SelectedItem.ToString();

            try
            {
                sqlConnection.Open();

                // Delete the record based on the selected AlertID
                string query = "DELETE FROM Alerts WHERE AlertID = @SelectedValue";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@SelectedValue", selectedValue);

                int rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Alert removed successfully!");

                    // Clear form controls or perform other actions after deletion
                    comboboxPatientID.Text = "";
                    ComboBoxEmpID.Text = "";
                    comboboxEmergencyCode.Text = "";
                    DatePicker.Value = DateTime.Now;
                    rbtnLow.Checked = false;
                    rbtnModerate.Checked = false;
                    rbtnHigh.Checked = false;

                    // Optionally, reload the ComboBox data after deletion
                    LoadComboBoxData();
                }
                else
                {
                    MessageBox.Show("No record found for the selected Alert ID!");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SQL Error: {ex.Message}\n{ex.StackTrace}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting the record: {ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            comboboxEmergencyCode.SelectedItem = null;
            comboboxAlertID.SelectedItem = null;
            comboboxPatientID.SelectedItem = null;
            ComboBoxEmpID.SelectedItem = null;

            // Clear the text box if no data is found for the selected EmergencyCode
            txtEmergencyType.Text = "";
            txtEmergencyType.StateCommon.Back.Color1 = Color.FromArgb(247, 248, 243); // Set the default back color
            txtEmergencyType.StateCommon.Content.Color1 = SystemColors.ControlText; // Set the default fore color
            txtEmergencyType.StateCommon.Content.Font = new Font(txtEmergencyType.Font, FontStyle.Regular); // Set the default font style

            txtName.Text = "";
            txtempName.Text = "";
            rbtnHigh.Checked = false;
            rbtnLow.Checked = false;
            rbtnModerate.Checked = false;
            DatePicker.Value = DateTime.Today;
        }
    }
}
