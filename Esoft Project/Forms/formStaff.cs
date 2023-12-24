using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Esoft_Project.Forms
{
    public partial class formStaff : Form
    {
        // Database connection string
        SqlConnection sqlConnection = new SqlConnection("Data Source=ROG-STRIX-G15;Initial Catalog=HMSdb;Integrated Security=True;Encrypt=True; TrustServerCertificate=true;");
        private string originalComboBoxEmpIDValue;

        public formStaff()
        {
            InitializeComponent();
        }

        // Load event when the form is first loaded
        private void formStaff_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
            originalComboBoxEmpIDValue = ComboBoxEmpID.Text;
        }

        // Event handler for validating the ComboBoxEmpID value
        private void ComboBoxEmpID_Validating(object sender, CancelEventArgs e)
        {
            string enteredValue = ComboBoxEmpID.Text;

            // If the entered value is not in the list, revert to the original value
            if (!ComboBoxEmpID.Items.Contains(enteredValue))
            {
                ComboBoxEmpID.Text = originalComboBoxEmpIDValue;
            }
        }

        // Method to load data into ComboBoxEmpID
        private void LoadComboBoxData()
        {
            try
            {
                sqlConnection.Open();

                // Get the maximum empID value from the Users table
                string queryMaxEmpID = "SELECT MAX(empID) AS LastValue FROM Users";
                SqlCommand sqlCommandMaxEmpID = new SqlCommand(queryMaxEmpID, sqlConnection);
                var lastValue = sqlCommandMaxEmpID.ExecuteScalar();

                // Set the default value for ComboBoxEmpID based on the maximum empID
                if (lastValue != DBNull.Value)
                {
                    int defaultValue = Convert.ToInt32(lastValue) + 1;
                    ComboBoxEmpID.Text = defaultValue.ToString();
                }

                // Retrieve all empID values from the Users table and populate ComboBoxEmpID
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
        }

        // Event handler for the selection change in ComboBoxEmpID
        private void ComboBoxEmpID_SelectedIndexChanged(object sender, EventArgs e)
        {
            originalComboBoxEmpIDValue = ComboBoxEmpID.Text;
            if (ComboBoxEmpID.SelectedItem == null)
            {
                return;
            }

            string selectedValue = ComboBoxEmpID.SelectedItem.ToString();

            try
            {
                sqlConnection.Open();

                // Retrieve user data based on the selected empID
                string query = "SELECT Name, isMale, DateOfBirth, MaritalStatus, NICno, Language, AddressLine1, AddressLine2, Phone, Email, JobTitle, Department, Username, Password FROM Users WHERE empID = @SelectedValue";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@SelectedValue", selectedValue);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    // Populate the form controls with the retrieved data
                    txtName.Text = GetString(reader, "Name");
                    rbtnMale.Checked = GetBoolean(reader, "isMale");
                    rbtnFemale.Checked = !rbtnMale.Checked;
                    DateTimeDob.Value = GetDateTime(reader, "DateOfBirth");

                    string maritalStatus = GetString(reader, "MaritalStatus");
                    if (maritalStatus == "True")
                    {
                        maritalStatus = "Married";
                    }
                    else if (maritalStatus == "False")
                    {
                        maritalStatus = "Single";
                    }

                    comboBoxMarital.Text = maritalStatus;
                    txtNic.Text = GetString(reader, "NICno");
                    comboBoxLanguage.Text = GetString(reader, "Language");
                    txtAddressLine1.Text = GetString(reader, "AddressLine1");
                    txtAddressLine2.Text = GetString(reader, "AddressLine2");
                    txtPhone.Text = GetString(reader, "Phone");
                    txtEmail.Text = GetString(reader, "Email");
                    txtJobTitle.Text = GetString(reader, "JobTitle");
                    comboboxDepartment.Text = GetString(reader, "Department");
                    txtUsername.Text = GetString(reader, "Username");
                    txtPassword.Text = "********";

                    MessageBox.Show("Data loaded successfully!");
                }
                else
                {
                    MessageBox.Show("No data found for the selected empID!");
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

        // Event handler for the Clear button
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        // Method to clear all form controls
        private void ClearForm()
        {
            txtName.Clear();
            txtNic.Clear();
            txtAddressLine1.Clear();
            txtAddressLine2.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtJobTitle.Clear();
            txtUsername.Clear();
            txtPassword.Clear();

            rbtnMale.Checked = false;
            rbtnFemale.Checked = false;

            DateTimeDob.Value = DateTime.Now;

            ComboBoxEmpID.SelectedIndex = -1;
            ComboBoxEmpID.Text = originalComboBoxEmpIDValue;
            comboBoxLanguage.SelectedIndex = -1;
            comboBoxLanguage.Text = string.Empty;
            comboboxDepartment.SelectedIndex = -1;
            comboboxDepartment.Text = string.Empty;
            comboBoxMarital.SelectedIndex = -1;
            comboBoxMarital.Text = string.Empty;
        }

        // Event handler for the Register button
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Get values from form controls
            string name = txtName.Text;
            bool isMale = rbtnMale.Checked;
            DateTime dateOfBirth = DateTimeDob.Value;
            bool maritalStatus = (comboBoxMarital.Text == "Married");
            string nic = txtNic.Text;
            string language = comboBoxLanguage.Text;
            string addressLine1 = txtAddressLine1.Text;
            string addressLine2 = txtAddressLine2.Text;
            string phone = txtPhone.Text;
            string email = txtEmail.Text;
            string jobTitle = txtJobTitle.Text;
            string department = comboboxDepartment.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            try
            {
                sqlConnection.Open();

                // Insert a new record into the Users table
                string query = "INSERT INTO Users (Name, isMale, DateOfBirth, MaritalStatus, NICno, Language, AddressLine1, AddressLine2, Phone, Email, JobTitle, Department, Username, Password) " +
                               "VALUES (@Name, @IsMale, @DateOfBirth, @MaritalStatus, @NICno, @Language, @AddressLine1, @AddressLine2, @Phone, @Email, @JobTitle, @Department, @Username, @Password)";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Name", name);
                sqlCommand.Parameters.AddWithValue("@IsMale", isMale);
                sqlCommand.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                sqlCommand.Parameters.AddWithValue("@MaritalStatus", maritalStatus);
                sqlCommand.Parameters.AddWithValue("@NICno", nic);
                sqlCommand.Parameters.AddWithValue("@Language", language);
                sqlCommand.Parameters.AddWithValue("@AddressLine1", addressLine1);
                sqlCommand.Parameters.AddWithValue("@AddressLine2", addressLine2);
                sqlCommand.Parameters.AddWithValue("@Phone", phone);
                sqlCommand.Parameters.AddWithValue("@Email", email);
                sqlCommand.Parameters.AddWithValue("@JobTitle", jobTitle);
                sqlCommand.Parameters.AddWithValue("@Department", department);
                sqlCommand.Parameters.AddWithValue("@Username", username);
                sqlCommand.Parameters.AddWithValue("@Password", password);

                sqlCommand.ExecuteNonQuery();

                MessageBox.Show("Registration successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error registering user: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
                ClearForm();
            }
        }

        // Event handler for the Delete button
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string selectedValue = ComboBoxEmpID.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedValue))
            {
                MessageBox.Show("Please select a user to delete.");
                return;
            }

            try
            {
                sqlConnection.Open();

                // Delete the record from the Users table based on the selected empID
                string query = "DELETE FROM Users WHERE empID = @SelectedValue";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@SelectedValue", selectedValue);

                int rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Record deleted successfully!");

                    ClearForm();
                }
                else
                {
                    MessageBox.Show("No records deleted. User not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting user: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string selectedEmpID = ComboBoxEmpID.SelectedItem?.ToString();

            // Ensure a value is selected
            if (string.IsNullOrEmpty(selectedEmpID))
            {
                MessageBox.Show("Please select a patient to update.");
                return;
            }

            try
            {
                sqlConnection.Open();

                // Update the existing record in the Users table
                string updateQuery = "UPDATE Users SET Name = @Name, isMale = @IsMale, DateOfBirth = @DateOfBirth, MaritalStatus = @MaritalStatus, " +
                                     "NICno = @NICno, Language = @Language, AddressLine1 = @AddressLine1, AddressLine2 = @AddressLine2, Phone = @Phone, " +
                                     "Email = @Email, JobTitle = @JobTitle, Department = @Department, Username = @Username, Password = @Password " +
                                     "WHERE PatientID = @PatientID";

                SqlCommand updateCommand = new SqlCommand(updateQuery, sqlConnection);

                // Set parameters for the update
                updateCommand.Parameters.AddWithValue("@Name", txtName.Text);
                updateCommand.Parameters.AddWithValue("@IsMale", rbtnMale.Checked);
                updateCommand.Parameters.AddWithValue("@DateOfBirth", DateTimeDob.Value);
                updateCommand.Parameters.AddWithValue("@MaritalStatus", comboBoxMarital.Text);
                updateCommand.Parameters.AddWithValue("@NICno", txtNic.Text);
                updateCommand.Parameters.AddWithValue("@Language", comboBoxLanguage.Text);
                updateCommand.Parameters.AddWithValue("@AddressLine1", txtAddressLine1.Text);
                updateCommand.Parameters.AddWithValue("@AddressLine2", txtAddressLine2.Text);
                updateCommand.Parameters.AddWithValue("@Phone", txtPhone.Text);
                updateCommand.Parameters.AddWithValue("@Email", txtEmail.Text);
                updateCommand.Parameters.AddWithValue("@JobTitle", txtJobTitle.Text);
                updateCommand.Parameters.AddWithValue("@Department", comboboxDepartment.Text);
                updateCommand.Parameters.AddWithValue("@Username", txtUsername.Text);
                updateCommand.Parameters.AddWithValue("@Password", txtPassword.Text);

                int rowsAffected = updateCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Update successful! Record updated in Users.");
                }
                else
                {
                    MessageBox.Show("Update failed. Please check your input or select a valid User.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating User: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
