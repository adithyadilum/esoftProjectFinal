using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Esoft_Project.Forms
{
    public partial class formOverview : Form
    {
        private string connectionString = "Data Source=ROG-STRIX-G15;Initial Catalog=HMSdb;Integrated Security=True;Encrypt=True; TrustServerCertificate=true;";

        public formOverview()
        {
            InitializeComponent();
            InitializeDonutChart();
            InitializeBarChart();
            UpdateCharts();
            UpdateUserLabels();
            UpdatePatientLabels();
            UpdateAlertLabel();
            UpdateReportLabels();
        }

        private void InitializeDonutChart()
        {
            donutChart.Series.Clear();
            donutChart.Series.Add("PatientRecords");
            donutChart.Series["PatientRecords"].ChartType = SeriesChartType.Doughnut;
            donutChart.Series["PatientRecords"].Points.Add(0); // Placeholder value, will be updated later
            donutChart.Series["PatientRecords"].Points.Add(0); // Placeholder value, will be updated later

            // Set colors
            donutChart.Series["PatientRecords"].Points[0].Color = Color.FromArgb(0, 44, 62);
            donutChart.Series["PatientRecords"].Points[1].Color = Color.FromArgb(247, 68, 78);
            donutChart.Series["PatientRecords"].Points[1].Label = "Available";
            donutChart.Series["PatientRecords"].Points[0].Label = "Occupied";
            donutChart.Series["PatientRecords"].Points[1].LabelForeColor = Color.White;
            donutChart.Series["PatientRecords"].Points[0].LabelForeColor = Color.White;
            donutChart.Series["PatientRecords"].Font = new Font("Century Gothic", 8, FontStyle.Bold);
        }

        private void InitializeBarChart()
        {
            barChart.Series.Clear();
            barChart.Series.Add("GenderDistribution");
            barChart.Series["GenderDistribution"].ChartType = SeriesChartType.Bar;

            // Set colors for male and female bars
            barChart.Series["GenderDistribution"].Points.Add(0); // Placeholder value, will be updated later
            barChart.Series["GenderDistribution"].Points.Add(0); // Placeholder value, will be updated later

            // Set bar colors
            barChart.Series["GenderDistribution"].Points[0].Color = Color.FromArgb(247, 68, 78); // Male
            barChart.Series["GenderDistribution"].Points[1].Color = Color.FromArgb(247, 68, 78); // Female

            // Set label font for the bar chart
            barChart.Series["GenderDistribution"].Font = new Font("Century Gothic", 9, FontStyle.Bold);

            // Add data points
            barChart.Series["GenderDistribution"].Points.AddXY("Male", 0);
            barChart.Series["GenderDistribution"].Points.AddXY("Female", 0);

            // Set axis label font
            barChart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Century Gothic", 9, FontStyle.Regular);
            barChart.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Century Gothic", 9, FontStyle.Regular);
        }

        private void UpdateCharts()
        {
            // Update the donut chart (similar to the previous example)
            UpdateDonutChart();

            // Update the bar chart
            UpdateBarChart();
        }

        private void UpdateDonutChart()
        {
            // Step 1: Connect to the SQL database and retrieve the number of records
            int numberOfRecords = GetNumberOfRecords();

            // Step 2: Calculate the percentage
            double percentage = (double)numberOfRecords / 200 * 100;

            // Step 3: Update the chart
            donutChart.Series["PatientRecords"].Points[0].YValues[0] = percentage;
            donutChart.Series["PatientRecords"].Points[1].YValues[0] = 100 - percentage;
        }

        private void UpdateBarChart()
        {
            // Connect to the SQL database and retrieve male and female counts
            int maleCount = GetGenderCount(1); // 1 represents male
            int femaleCount = GetGenderCount(0); // 0 represents female

            // Clear existing data points
            barChart.Series["GenderDistribution"].Points.Clear();

            // Add updated data points
            barChart.Series["GenderDistribution"].Points.AddXY("Male", maleCount);
            barChart.Series["GenderDistribution"].Points.AddXY("Female", femaleCount);
        }

        private int GetGenderCount(int genderValue)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM PatientRecords WHERE Gender = {genderValue}", connection))
                {
                    // Execute the query and get the result
                    int genderCount = (int)command.ExecuteScalar();
                    return genderCount;
                }
            }
        }

        private int GetNumberOfRecords()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM PatientRecords", connection))
                {
                    // Step 1: Execute the query and get the result
                    int numberOfRecords = (int)command.ExecuteScalar();
                    return numberOfRecords;
                }
            }
        }

        private void UpdateUserLabels()
        {
            // Update the label lblStaffTotal with the total number of records in the Users table
            int totalStaffCount = GetTotalStaffCount();
            lblStaffTotal.Text = totalStaffCount.ToString();

            // Update the label lblNurNo with the count of records where JobTitle is Nurse
            int nurseCount = GetJobTitleCount("Nurse");
            lblNurNo.Text = nurseCount.ToString();

            // Update the label lblPhyNo with the count of records where JobTitle is not Nurse
            int physicianCount = totalStaffCount - nurseCount;
            lblPhyNo.Text = physicianCount.ToString();
        }

        private int GetTotalStaffCount()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Users", connection))
                {
                    // Execute the query and get the result
                    int totalStaffCount = (int)command.ExecuteScalar();
                    return totalStaffCount;
                }
            }
        }

        private int GetJobTitleCount(string jobTitle)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM Users WHERE JobTitle = '{jobTitle}'", connection))
                {
                    // Execute the query and get the result
                    int jobTitleCount = (int)command.ExecuteScalar();
                    return jobTitleCount;
                }
            }
        }

        private void UpdatePatientLabels()
        {
            // Update the label lblCurrent with the total number of records in the PatientRecords table
            int totalPatientCount = GetTotalPatientCount();
            lblCurrent.Text = totalPatientCount.ToString();

            // Update the label lblTotalAdd with the total number of records added today to the PatientRecords table
            int totalAddedToday = GetTotalAddedToday();
            lblTotalAdd.Text = totalAddedToday.ToString();
        }

        private int GetTotalPatientCount()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM PatientRecords", connection))
                {
                    // Execute the query and get the result
                    int totalPatientCount = (int)command.ExecuteScalar();
                    return totalPatientCount;
                }
            }
        }

        private int GetTotalAddedToday()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DateTime today = DateTime.Today;
                using (SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM PatientRecords WHERE CONVERT(date, DateAdded) = @Today", connection))
                {
                    // Use a parameter to prevent SQL injection
                    command.Parameters.AddWithValue("@Today", today);

                    // Execute the query and get the result
                    int totalAddedToday = (int)command.ExecuteScalar();
                    return totalAddedToday;
                }
            }
        }

        private void UpdateAlertLabel()
        {
            // Update the label lblAlertNo with the number of alerts in the Alerts table
            int totalAlerts = GetTotalAlerts();
            lblAlertNo.Text = totalAlerts.ToString();
        }

        private int GetTotalAlerts()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Alerts", connection))
                {
                    // Execute the query and get the result
                    int totalAlerts = (int)command.ExecuteScalar();
                    return totalAlerts;
                }
            }
        }

        private void UpdateReportLabels()
        {
            // Update the label lblInProgress with the number of records with 'InProgress' status in the Reports table
            int inProgressCount = GetReportStatusCount("InProgress");
            lblInprogress.Text = inProgressCount.ToString();

            // Update the label lblCompleted with the number of records with 'Completed' status in the Reports table
            int completedCount = GetReportStatusCount("Completed");
            lblCompleted.Text = completedCount.ToString();
        }

        private int GetReportStatusCount(string status)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM Reports WHERE Status = @Status", connection))
                {
                    // Use a parameter to prevent SQL injection
                    command.Parameters.AddWithValue("@Status", status);

                    // Execute the query and get the result
                    int statusCount = (int)command.ExecuteScalar();
                    return statusCount;
                }
            }
        }
    }
}
