using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esoft_Project.Forms
{
    public partial class formEmergency : Form
    {
        SqlConnection sqlConnection = new SqlConnection("Data Source=ROG-STRIX-G15;Initial Catalog=HMSdb;Integrated Security=True;Encrypt=True; TrustServerCertificate=true;");
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

        private void btnView_Click(object sender, EventArgs e)
        {
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
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@SelectedValue", selectedValue);

                SqlDataReader reader = sqlCommand.ExecuteReader();

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
                    MessageBox.Show("Data loaded successfully!");
                }
                else
                {
                    MessageBox.Show("No data found for the selected Alert ID!");
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

        private void comboboxEmergencyCode_SelectedIndexChanged(object sender, EventArgs e)
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
    }
}
