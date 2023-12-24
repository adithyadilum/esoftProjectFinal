using ComponentFactory.Krypton.Toolkit;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Esoft_Project
{
    public partial class formLogin : KryptonForm
    {
        private const string ConnectionString = "Data Source=ROG-STRIX-G15;Initial Catalog=HMSdb;Integrated Security=True;";
        public formLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IsValidUser(username, password))
            {
                this.Hide();
                formMain obj = new formMain();
                obj.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.StateCommon.Border.Color1 = Color.FromArgb(247, 68, 78);
                txtPassword.StateCommon.Border.Color2 = Color.FromArgb(247, 68, 78);
                txtUsername.StateCommon.Border.Color1 = Color.FromArgb(247, 68, 78);
                txtUsername.StateCommon.Border.Color2 = Color.FromArgb(247, 68, 78);
            }
        }

        private bool IsValidUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }

        private void btnLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Trigger the Button click event
                btnLogin.PerformClick();
                // Optionally, you can prevent the Enter key from being processed by the TextBox
                e.Handled = true;
            }
        }
    }
}
