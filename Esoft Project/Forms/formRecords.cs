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
    public partial class formRecords : Form
    {
        SqlConnection sqlConnection = new SqlConnection("Data Source=ROG-STRIX-G15;Initial Catalog=HMSdb;Integrated Security=True;Encrypt=True; TrustServerCertificate=true;");
        private string originalcomboboxPatientIDValue;
        public formRecords()
        {
            InitializeComponent();
        }

        private void formRecords_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
            originalcomboboxPatientIDValue = comboboxPatientID.Text;
        }
        private void LoadComboBoxData()
        {
            try
            {
                sqlConnection.Open();

   
                string queryMaxPatientID = "SELECT MAX(PatientID) AS LastValue FROM PatientRecords";
                SqlCommand sqlCommandMaxPatientID = new SqlCommand(queryMaxPatientID, sqlConnection);
                var lastValue = sqlCommandMaxPatientID.ExecuteScalar();

       
                if (lastValue != DBNull.Value)
                {
                    int defaultValue = Convert.ToInt32(lastValue) + 1;
                    comboboxPatientID.Text = defaultValue.ToString();
                }

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
        }

        private void comboboxPatientID_SelectedIndexChanged(object sender, EventArgs e)
        {
            originalcomboboxPatientIDValue = comboboxPatientID.Text;
            if (comboboxPatientID.SelectedItem == null)
            {
                return;
            }

            string selectedValue = comboboxPatientID.SelectedItem.ToString();

            try
            {
                sqlConnection.Open();

                // Retrieve user data based on the selected empID
                string query = "SELECT Department, Name, Gender, DateOfBirth, MaritalStatus, NICNo, Language, AddressLine1, AddressLine2, Phone, Email, EmergencyContactNo, Height, Weight, HeartRate, Pressure, Temperature, Condition, Tests, SpecialNotes FROM PatientRecords WHERE PatientID = @SelectedValue";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@SelectedValue", selectedValue);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    // Populate the form controls with the retrieved data
                    txtName.Text = GetString(reader, "Name");
                    comboboxDepartment.Text = GetString(reader, "Department");
                    rbtnMale.Checked = GetBoolean(reader, "Gender");
                    rbtnFemale.Checked = !rbtnMale.Checked;
                    DateTimeDob.Value = GetDateTime(reader, "DateOfBirth");
                    comboBoxMarital.Text = GetString(reader, "MaritalStatus");
                    txtNic.Text = GetString(reader, "NICno");
                    comboBoxLanguage.Text = GetString(reader, "Language");
                    txtAddressLine1.Text = GetString(reader, "AddressLine1");
                    txtAddressLine2.Text = GetString(reader, "AddressLine2");
                    txtPhone.Text = GetString(reader, "Phone");
                    txtEmail.Text = GetString(reader, "Email");
                    txtEmergencyPhn.Text = GetString(reader, "EmergencyContactNo");
                    txtHeight.Text = reader["Height"].ToString();
                    txtWeight.Text = reader["Weight"].ToString();
                    txtHeartRate.Text = reader["HeartRate"].ToString();
                    txtBP.Text = reader["Pressure"].ToString();
                    txtTemp.Text = reader["Temperature"].ToString();
                    ComboBoxCondition.Text = reader["Condition"].ToString();
                    ComboBoxLabTests.Text = reader["Tests"].ToString();
                    txtNotes.Text = reader["SpecialNotes"].ToString();
                

                MessageBox.Show("Data loaded successfully!");
                }
                else
                {
                    MessageBox.Show("No data found for the selected Patient ID!");
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
            int columnIndex = reader.GetOrdinal(columnName);

            if (!reader.IsDBNull(columnIndex) && reader.GetFieldType(columnIndex) == typeof(bool))
            {
                return reader.GetBoolean(columnIndex).ToString();
            }

            return reader.IsDBNull(columnIndex) ? string.Empty : reader.GetString(columnIndex);
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllControls();
        }

        private void ClearAllControls()
        {
            txtName.Clear();
            comboboxDepartment.Text = string.Empty;
            rbtnMale.Checked = false;
            rbtnFemale.Checked = false;
            DateTimeDob.Value = DateTime.Now;
            comboBoxMarital.Text = string.Empty;
            txtNic.Clear();
            comboBoxLanguage.Text = string.Empty;
            txtAddressLine1.Clear();
            txtAddressLine2.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtEmergencyPhn.Clear();
            txtHeight.Clear();
            txtWeight.Clear();
            txtHeartRate.Clear();
            txtBP.Clear();
            txtTemp.Clear();
            ComboBoxCondition.Text = string.Empty;
            ComboBoxLabTests.Text = string.Empty;
            txtNotes.Clear();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();

                string insertQuery = "INSERT INTO PatientRecords (Department, Name, Gender, DateOfBirth, MaritalStatus, NICNo, Language, AddressLine1, AddressLine2, Phone, Email, EmergencyContactNo, Height, Weight, HeartRate, Pressure, Temperature, Condition, Tests, SpecialNotes) " +
                                     "VALUES (@Department, @Name, @Gender, @DateOfBirth, @MaritalStatus, @NICno, @Language, @AddressLine1, @AddressLine2, @Phone, @Email, @EmergencyContactNo, @Height, @Weight, @HeartRate, @Pressure, @Temperature, @Condition, @Tests, @SpecialNotes)";

                SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection);

                insertCommand.Parameters.AddWithValue("@Department", comboboxDepartment.Text);
                insertCommand.Parameters.AddWithValue("@Name", txtName.Text);
                insertCommand.Parameters.AddWithValue("@Gender", rbtnMale.Checked);
                insertCommand.Parameters.AddWithValue("@DateOfBirth", DateTimeDob.Value);
                insertCommand.Parameters.AddWithValue("@MaritalStatus", comboBoxMarital.Text);
                insertCommand.Parameters.AddWithValue("@NICno", txtNic.Text);
                insertCommand.Parameters.AddWithValue("@Language", comboBoxLanguage.Text);
                insertCommand.Parameters.AddWithValue("@AddressLine1", txtAddressLine1.Text);
                insertCommand.Parameters.AddWithValue("@AddressLine2", txtAddressLine2.Text);
                insertCommand.Parameters.AddWithValue("@Phone", txtPhone.Text);
                insertCommand.Parameters.AddWithValue("@Email", txtEmail.Text);
                insertCommand.Parameters.AddWithValue("@EmergencyContactNo", txtEmergencyPhn.Text);
                insertCommand.Parameters.AddWithValue("@Height", Convert.ToDecimal(txtHeight.Text));
                insertCommand.Parameters.AddWithValue("@Weight", Convert.ToDecimal(txtWeight.Text));
                insertCommand.Parameters.AddWithValue("@HeartRate", Convert.ToInt32(txtHeartRate.Text));
                insertCommand.Parameters.AddWithValue("@Pressure", txtBP.Text);
                insertCommand.Parameters.AddWithValue("@Temperature", Convert.ToDecimal(txtTemp.Text));
                insertCommand.Parameters.AddWithValue("@Condition", ComboBoxCondition.Text);
                insertCommand.Parameters.AddWithValue("@Tests", ComboBoxLabTests.Text);
                insertCommand.Parameters.AddWithValue("@SpecialNotes", txtNotes.Text);

                int rowsAffected = insertCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Registration successful! New record added to PatientRecords.");
                }
                else
                {
                    MessageBox.Show("Registration failed. Please check your input.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error registering patient: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string selectedPatientID = comboboxPatientID.SelectedItem?.ToString();

            // Ensure a value is selected
            if (string.IsNullOrEmpty(selectedPatientID))
            {
                MessageBox.Show("Please select a patient to update.");
                return;
            }

            try
            {
                sqlConnection.Open();

                // Update the existing record in the PatientRecords table
                string updateQuery = "UPDATE PatientRecords SET Department = @Department, Name = @Name, Gender = @Gender, DateOfBirth = @DateOfBirth, " +
                                     "MaritalStatus = @MaritalStatus, NICNo = @NICno, Language = @Language, AddressLine1 = @AddressLine1, AddressLine2 = @AddressLine2, " +
                                     "Phone = @Phone, Email = @Email, EmergencyContactNo = @EmergencyContactNo, Height = @Height, Weight = @Weight, HeartRate = @HeartRate, " +
                                     "Pressure = @Pressure, Temperature = @Temperature, Condition = @Condition, Tests = @Tests, SpecialNotes = @SpecialNotes " +
                                     "WHERE PatientID = @PatientID";

                SqlCommand updateCommand = new SqlCommand(updateQuery, sqlConnection);

                // Set parameters for the update
                updateCommand.Parameters.AddWithValue("@Department", comboboxDepartment.Text);
                updateCommand.Parameters.AddWithValue("@Name", txtName.Text);
                updateCommand.Parameters.AddWithValue("@Gender", rbtnMale.Checked);
                updateCommand.Parameters.AddWithValue("@DateOfBirth", DateTimeDob.Value);
                updateCommand.Parameters.AddWithValue("@MaritalStatus", comboBoxMarital.Text);
                updateCommand.Parameters.AddWithValue("@NICno", txtNic.Text);
                updateCommand.Parameters.AddWithValue("@Language", comboBoxLanguage.Text);
                updateCommand.Parameters.AddWithValue("@AddressLine1", txtAddressLine1.Text);
                updateCommand.Parameters.AddWithValue("@AddressLine2", txtAddressLine2.Text);
                updateCommand.Parameters.AddWithValue("@Phone", txtPhone.Text);
                updateCommand.Parameters.AddWithValue("@Email", txtEmail.Text);
                updateCommand.Parameters.AddWithValue("@EmergencyContactNo", txtEmergencyPhn.Text);
                updateCommand.Parameters.AddWithValue("@Height", Convert.ToDecimal(txtHeight.Text));
                updateCommand.Parameters.AddWithValue("@Weight", Convert.ToDecimal(txtWeight.Text));
                updateCommand.Parameters.AddWithValue("@HeartRate", Convert.ToInt32(txtHeartRate.Text));
                updateCommand.Parameters.AddWithValue("@Pressure", txtBP.Text);
                updateCommand.Parameters.AddWithValue("@Temperature", Convert.ToDecimal(txtTemp.Text));
                updateCommand.Parameters.AddWithValue("@Condition", ComboBoxCondition.Text);
                updateCommand.Parameters.AddWithValue("@Tests", ComboBoxLabTests.Text);
                updateCommand.Parameters.AddWithValue("@SpecialNotes", txtNotes.Text);
                updateCommand.Parameters.AddWithValue("@PatientID", selectedPatientID);

                int rowsAffected = updateCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Update successful! Record updated in PatientRecords.");
                }
                else
                {
                    MessageBox.Show("Update failed. Please check your input or select a valid patient.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating patient: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Get the selected PatientID from ComboBoxPatientID
            string selectedPatientID = comboboxPatientID.SelectedItem?.ToString();

            // Ensure a value is selected
            if (string.IsNullOrEmpty(selectedPatientID))
            {
                MessageBox.Show("Please select a patient to delete.");
                return;
            }

            try
            {
                sqlConnection.Open();

                // Delete the record from the PatientRecords table
                string deleteQuery = "DELETE FROM PatientRecords WHERE PatientID = @PatientID";

                SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlConnection);
                deleteCommand.Parameters.AddWithValue("@PatientID", selectedPatientID);

                int rowsAffected = deleteCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Delete successful! Record removed from PatientRecords.");
                    // Optionally, clear the controls or perform other actions after successful deletion
                    ClearAllControls();
                }
                else
                {
                    MessageBox.Show("Delete failed. Please check your input or select a valid patient.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting patient: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
