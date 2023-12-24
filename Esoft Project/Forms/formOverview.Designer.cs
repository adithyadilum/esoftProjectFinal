namespace Esoft_Project.Forms
{
    partial class formOverview
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 30D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2D, 11D);
            this.customPanel7 = new Esoft_Project.CustomPanel();
            this.barChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.customPanel6 = new Esoft_Project.CustomPanel();
            this.lblNurNo = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblPhyNo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblStaffTotal = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.customPanel5 = new Esoft_Project.CustomPanel();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.label20 = new System.Windows.Forms.Label();
            this.lblCompleted = new System.Windows.Forms.Label();
            this.lblInprogress = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.customPanel4 = new Esoft_Project.CustomPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.lblAlertNo = new System.Windows.Forms.Label();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.customPanel3 = new Esoft_Project.CustomPanel();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.label10 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblTotalAdd = new System.Windows.Forms.Label();
            this.customPanel2 = new Esoft_Project.CustomPanel();
            this.donutChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.customPanel1 = new Esoft_Project.CustomPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelEsoftHospitals = new System.Windows.Forms.Label();
            this.customPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barChart)).BeginInit();
            this.customPanel6.SuspendLayout();
            this.customPanel5.SuspendLayout();
            this.customPanel4.SuspendLayout();
            this.customPanel3.SuspendLayout();
            this.customPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.donutChart)).BeginInit();
            this.customPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // customPanel7
            // 
            this.customPanel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(243)))));
            this.customPanel7.BottomLeftCornerRadius = 20;
            this.customPanel7.BottomRightCornerRadius = 20;
            this.customPanel7.Controls.Add(this.barChart);
            this.customPanel7.Controls.Add(this.kryptonLabel6);
            this.customPanel7.EndColor = System.Drawing.Color.White;
            this.customPanel7.GradientAngle = 0F;
            this.customPanel7.GradientCenter = new System.Drawing.Point(100, 50);
            this.customPanel7.GradientType = Esoft_Project.GradientType.Linear;
            this.customPanel7.Location = new System.Drawing.Point(359, 194);
            this.customPanel7.Name = "customPanel7";
            this.customPanel7.Size = new System.Drawing.Size(350, 233);
            this.customPanel7.StartColor = System.Drawing.Color.White;
            this.customPanel7.TabIndex = 83;
            this.customPanel7.TopLeftCornerRadius = 20;
            this.customPanel7.TopRightCornerRadius = 20;
            this.customPanel7.UseGradientFill = true;
            // 
            // barChart
            // 
            chartArea3.AxisX.MajorGrid.Enabled = false;
            chartArea3.AxisY.MajorGrid.Enabled = false;
            chartArea3.Name = "ChartArea1";
            this.barChart.ChartAreas.Add(chartArea3);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.barChart.Legends.Add(legend2);
            this.barChart.Location = new System.Drawing.Point(21, 66);
            this.barChart.Name = "barChart";
            this.barChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.barChart.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(68)))), ((int)(((byte)(78)))))};
            this.barChart.Size = new System.Drawing.Size(314, 140);
            this.barChart.TabIndex = 82;
            this.barChart.Text = "chart2";
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(3, 12);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonLabel6.Size = new System.Drawing.Size(123, 26);
            this.kryptonLabel6.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(62)))));
            this.kryptonLabel6.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(62)))));
            this.kryptonLabel6.StateCommon.ShortText.Font = new System.Drawing.Font("Ivy Mode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel6.TabIndex = 81;
            this.kryptonLabel6.Values.Text = "Demographics:";
            // 
            // customPanel6
            // 
            this.customPanel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(243)))));
            this.customPanel6.BottomLeftCornerRadius = 20;
            this.customPanel6.BottomRightCornerRadius = 20;
            this.customPanel6.Controls.Add(this.lblNurNo);
            this.customPanel6.Controls.Add(this.label8);
            this.customPanel6.Controls.Add(this.lblPhyNo);
            this.customPanel6.Controls.Add(this.label6);
            this.customPanel6.Controls.Add(this.lblStaffTotal);
            this.customPanel6.Controls.Add(this.lblName);
            this.customPanel6.Controls.Add(this.kryptonLabel5);
            this.customPanel6.EndColor = System.Drawing.Color.White;
            this.customPanel6.GradientAngle = 0F;
            this.customPanel6.GradientCenter = new System.Drawing.Point(100, 50);
            this.customPanel6.GradientType = Esoft_Project.GradientType.Linear;
            this.customPanel6.Location = new System.Drawing.Point(359, 442);
            this.customPanel6.Name = "customPanel6";
            this.customPanel6.Size = new System.Drawing.Size(353, 210);
            this.customPanel6.StartColor = System.Drawing.Color.White;
            this.customPanel6.TabIndex = 85;
            this.customPanel6.TopLeftCornerRadius = 20;
            this.customPanel6.TopRightCornerRadius = 20;
            this.customPanel6.UseGradientFill = true;
            // 
            // lblNurNo
            // 
            this.lblNurNo.AutoSize = true;
            this.lblNurNo.BackColor = System.Drawing.Color.White;
            this.lblNurNo.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNurNo.Location = new System.Drawing.Point(298, 151);
            this.lblNurNo.Name = "lblNurNo";
            this.lblNurNo.Size = new System.Drawing.Size(0, 20);
            this.lblNurNo.TabIndex = 87;
            this.lblNurNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.White;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(46, 151);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(136, 18);
            this.label8.TabIndex = 86;
            this.label8.Text = "Number of Nurses";
            // 
            // lblPhyNo
            // 
            this.lblPhyNo.AutoSize = true;
            this.lblPhyNo.BackColor = System.Drawing.Color.White;
            this.lblPhyNo.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhyNo.Location = new System.Drawing.Point(296, 113);
            this.lblPhyNo.Name = "lblPhyNo";
            this.lblPhyNo.Size = new System.Drawing.Size(0, 20);
            this.lblPhyNo.TabIndex = 85;
            this.lblPhyNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(46, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(164, 18);
            this.label6.TabIndex = 84;
            this.label6.Text = "Number of Physicians";
            // 
            // lblStaffTotal
            // 
            this.lblStaffTotal.AutoSize = true;
            this.lblStaffTotal.BackColor = System.Drawing.Color.White;
            this.lblStaffTotal.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStaffTotal.Location = new System.Drawing.Point(296, 75);
            this.lblStaffTotal.Name = "lblStaffTotal";
            this.lblStaffTotal.Size = new System.Drawing.Size(0, 20);
            this.lblStaffTotal.TabIndex = 83;
            this.lblStaffTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.White;
            this.lblName.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(46, 75);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(157, 18);
            this.lblName.TabIndex = 82;
            this.lblName.Text = "Total Number of Staff";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(3, 12);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonLabel5.Size = new System.Drawing.Size(145, 26);
            this.kryptonLabel5.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(62)))));
            this.kryptonLabel5.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(62)))));
            this.kryptonLabel5.StateCommon.ShortText.Font = new System.Drawing.Font("Ivy Mode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel5.TabIndex = 81;
            this.kryptonLabel5.Values.Text = "Staffing Overview:";
            // 
            // customPanel5
            // 
            this.customPanel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(243)))));
            this.customPanel5.BottomLeftCornerRadius = 20;
            this.customPanel5.BottomRightCornerRadius = 20;
            this.customPanel5.Controls.Add(this.kryptonLabel4);
            this.customPanel5.Controls.Add(this.label20);
            this.customPanel5.Controls.Add(this.lblCompleted);
            this.customPanel5.Controls.Add(this.lblInprogress);
            this.customPanel5.Controls.Add(this.label18);
            this.customPanel5.EndColor = System.Drawing.Color.White;
            this.customPanel5.GradientAngle = 0F;
            this.customPanel5.GradientCenter = new System.Drawing.Point(100, 50);
            this.customPanel5.GradientType = Esoft_Project.GradientType.Linear;
            this.customPanel5.Location = new System.Drawing.Point(725, 419);
            this.customPanel5.Name = "customPanel5";
            this.customPanel5.Size = new System.Drawing.Size(244, 163);
            this.customPanel5.StartColor = System.Drawing.Color.White;
            this.customPanel5.TabIndex = 84;
            this.customPanel5.TopLeftCornerRadius = 20;
            this.customPanel5.TopRightCornerRadius = 20;
            this.customPanel5.UseGradientFill = true;
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(3, 12);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonLabel4.Size = new System.Drawing.Size(187, 26);
            this.kryptonLabel4.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(62)))));
            this.kryptonLabel4.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(62)))));
            this.kryptonLabel4.StateCommon.ShortText.Font = new System.Drawing.Font("Ivy Mode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel4.TabIndex = 81;
            this.kryptonLabel4.Values.Text = "Laboratory and Imaging:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.White;
            this.label20.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(58, 71);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(87, 18);
            this.label20.TabIndex = 94;
            this.label20.Text = "In progress";
            // 
            // lblCompleted
            // 
            this.lblCompleted.AutoSize = true;
            this.lblCompleted.BackColor = System.Drawing.Color.White;
            this.lblCompleted.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompleted.Location = new System.Drawing.Point(175, 107);
            this.lblCompleted.Name = "lblCompleted";
            this.lblCompleted.Size = new System.Drawing.Size(17, 20);
            this.lblCompleted.TabIndex = 97;
            this.lblCompleted.Text = "2";
            // 
            // lblInprogress
            // 
            this.lblInprogress.AutoSize = true;
            this.lblInprogress.BackColor = System.Drawing.Color.White;
            this.lblInprogress.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInprogress.Location = new System.Drawing.Point(175, 71);
            this.lblInprogress.Name = "lblInprogress";
            this.lblInprogress.Size = new System.Drawing.Size(17, 20);
            this.lblInprogress.TabIndex = 95;
            this.lblInprogress.Text = "2";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.White;
            this.label18.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(58, 104);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(92, 18);
            this.label18.TabIndex = 96;
            this.label18.Text = "Completed";
            // 
            // customPanel4
            // 
            this.customPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(243)))));
            this.customPanel4.BottomLeftCornerRadius = 20;
            this.customPanel4.BottomRightCornerRadius = 20;
            this.customPanel4.Controls.Add(this.label5);
            this.customPanel4.Controls.Add(this.lblAlertNo);
            this.customPanel4.Controls.Add(this.kryptonLabel2);
            this.customPanel4.EndColor = System.Drawing.Color.White;
            this.customPanel4.GradientAngle = 0F;
            this.customPanel4.GradientCenter = new System.Drawing.Point(100, 50);
            this.customPanel4.GradientType = Esoft_Project.GradientType.Linear;
            this.customPanel4.Location = new System.Drawing.Point(724, 194);
            this.customPanel4.Name = "customPanel4";
            this.customPanel4.Size = new System.Drawing.Size(244, 210);
            this.customPanel4.StartColor = System.Drawing.Color.White;
            this.customPanel4.TabIndex = 84;
            this.customPanel4.TopLeftCornerRadius = 20;
            this.customPanel4.TopRightCornerRadius = 20;
            this.customPanel4.UseGradientFill = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(81, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 20);
            this.label5.TabIndex = 88;
            this.label5.Text = "Alerts Today";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlertNo
            // 
            this.lblAlertNo.AutoSize = true;
            this.lblAlertNo.BackColor = System.Drawing.Color.White;
            this.lblAlertNo.Font = new System.Drawing.Font("Century Gothic", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlertNo.Location = new System.Drawing.Point(99, 82);
            this.lblAlertNo.Name = "lblAlertNo";
            this.lblAlertNo.Size = new System.Drawing.Size(61, 44);
            this.lblAlertNo.TabIndex = 88;
            this.lblAlertNo.Text = "11";
            this.lblAlertNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(3, 12);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonLabel2.Size = new System.Drawing.Size(139, 26);
            this.kryptonLabel2.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(62)))));
            this.kryptonLabel2.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(62)))));
            this.kryptonLabel2.StateCommon.ShortText.Font = new System.Drawing.Font("Ivy Mode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel2.TabIndex = 81;
            this.kryptonLabel2.Values.Text = "Emergency Alerts";
            // 
            // customPanel3
            // 
            this.customPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(243)))));
            this.customPanel3.BottomLeftCornerRadius = 20;
            this.customPanel3.BottomRightCornerRadius = 20;
            this.customPanel3.Controls.Add(this.lblCurrent);
            this.customPanel3.Controls.Add(this.kryptonLabel1);
            this.customPanel3.Controls.Add(this.label10);
            this.customPanel3.Controls.Add(this.label14);
            this.customPanel3.Controls.Add(this.lblTotalAdd);
            this.customPanel3.EndColor = System.Drawing.Color.White;
            this.customPanel3.GradientAngle = 0F;
            this.customPanel3.GradientCenter = new System.Drawing.Point(100, 50);
            this.customPanel3.GradientType = Esoft_Project.GradientType.Linear;
            this.customPanel3.Location = new System.Drawing.Point(12, 477);
            this.customPanel3.Name = "customPanel3";
            this.customPanel3.Size = new System.Drawing.Size(333, 175);
            this.customPanel3.StartColor = System.Drawing.Color.White;
            this.customPanel3.TabIndex = 83;
            this.customPanel3.TopLeftCornerRadius = 20;
            this.customPanel3.TopRightCornerRadius = 20;
            this.customPanel3.UseGradientFill = true;
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.BackColor = System.Drawing.Color.White;
            this.lblCurrent.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrent.Location = new System.Drawing.Point(284, 105);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(0, 20);
            this.lblCurrent.TabIndex = 93;
            this.lblCurrent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(3, 12);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonLabel1.Size = new System.Drawing.Size(213, 26);
            this.kryptonLabel1.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(62)))));
            this.kryptonLabel1.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(62)))));
            this.kryptonLabel1.StateCommon.ShortText.Font = new System.Drawing.Font("Ivy Mode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel1.TabIndex = 81;
            this.kryptonLabel1.Values.Text = "Admissions and Discharges:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(32, 105);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(178, 18);
            this.label10.TabIndex = 92;
            this.label10.Text = "Current Inpatient Count";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.White;
            this.label14.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(32, 68);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(175, 18);
            this.label14.TabIndex = 88;
            this.label14.Text = "Total Admissions Today";
            // 
            // lblTotalAdd
            // 
            this.lblTotalAdd.AutoSize = true;
            this.lblTotalAdd.BackColor = System.Drawing.Color.White;
            this.lblTotalAdd.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAdd.Location = new System.Drawing.Point(282, 68);
            this.lblTotalAdd.Name = "lblTotalAdd";
            this.lblTotalAdd.Size = new System.Drawing.Size(0, 20);
            this.lblTotalAdd.TabIndex = 89;
            this.lblTotalAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // customPanel2
            // 
            this.customPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(243)))));
            this.customPanel2.BottomLeftCornerRadius = 20;
            this.customPanel2.BottomRightCornerRadius = 20;
            this.customPanel2.Controls.Add(this.donutChart);
            this.customPanel2.Controls.Add(this.kryptonLabel3);
            this.customPanel2.EndColor = System.Drawing.Color.White;
            this.customPanel2.GradientAngle = 0F;
            this.customPanel2.GradientCenter = new System.Drawing.Point(100, 50);
            this.customPanel2.GradientType = Esoft_Project.GradientType.Linear;
            this.customPanel2.Location = new System.Drawing.Point(12, 194);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(333, 266);
            this.customPanel2.StartColor = System.Drawing.Color.White;
            this.customPanel2.TabIndex = 32;
            this.customPanel2.TopLeftCornerRadius = 20;
            this.customPanel2.TopRightCornerRadius = 20;
            this.customPanel2.UseGradientFill = true;
            // 
            // donutChart
            // 
            chartArea4.Name = "ChartArea1";
            this.donutChart.ChartAreas.Add(chartArea4);
            this.donutChart.Location = new System.Drawing.Point(57, 35);
            this.donutChart.Name = "donutChart";
            this.donutChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series2.LabelForeColor = System.Drawing.Color.White;
            series2.Name = "Series1";
            dataPoint3.BorderColor = System.Drawing.Color.White;
            dataPoint3.Color = System.Drawing.Color.Red;
            series2.Points.Add(dataPoint3);
            series2.Points.Add(dataPoint4);
            this.donutChart.Series.Add(series2);
            this.donutChart.Size = new System.Drawing.Size(238, 210);
            this.donutChart.TabIndex = 82;
            this.donutChart.Text = "chart1";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(3, 12);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonLabel3.Size = new System.Drawing.Size(132, 26);
            this.kryptonLabel3.StateCommon.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(62)))));
            this.kryptonLabel3.StateCommon.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(62)))));
            this.kryptonLabel3.StateCommon.ShortText.Font = new System.Drawing.Font("Ivy Mode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel3.TabIndex = 81;
            this.kryptonLabel3.Values.Text = "Bed Occupancy:";
            // 
            // customPanel1
            // 
            this.customPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(243)))));
            this.customPanel1.BottomLeftCornerRadius = 20;
            this.customPanel1.BottomRightCornerRadius = 20;
            this.customPanel1.Controls.Add(this.label3);
            this.customPanel1.Controls.Add(this.label2);
            this.customPanel1.Controls.Add(this.label1);
            this.customPanel1.Controls.Add(this.pictureBox1);
            this.customPanel1.Controls.Add(this.labelEsoftHospitals);
            this.customPanel1.EndColor = System.Drawing.Color.White;
            this.customPanel1.GradientAngle = 0F;
            this.customPanel1.GradientCenter = new System.Drawing.Point(100, 50);
            this.customPanel1.GradientType = Esoft_Project.GradientType.Linear;
            this.customPanel1.Location = new System.Drawing.Point(12, 12);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(956, 172);
            this.customPanel1.StartColor = System.Drawing.Color.White;
            this.customPanel1.TabIndex = 31;
            this.customPanel1.TopLeftCornerRadius = 20;
            this.customPanel1.TopRightCornerRadius = 20;
            this.customPanel1.UseGradientFill = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Ivy Mode", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(62)))));
            this.label3.Location = new System.Drawing.Point(365, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(229, 16);
            this.label3.TabIndex = 15;
            this.label3.Text = "+94 81 1 234 567  info@esofthospitals.lk";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Ivy Mode", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(62)))));
            this.label2.Location = new System.Drawing.Point(429, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "Kandy, Sri Lanka";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Ivy Mode", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(62)))));
            this.label1.Location = new System.Drawing.Point(393, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "A   E s o f t   G r o u p   C o m p a n y";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = global::Esoft_Project.Properties.Resources.linear_cross1;
            this.pictureBox1.Location = new System.Drawing.Point(325, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(53, 51);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // labelEsoftHospitals
            // 
            this.labelEsoftHospitals.AutoSize = true;
            this.labelEsoftHospitals.BackColor = System.Drawing.Color.White;
            this.labelEsoftHospitals.Font = new System.Drawing.Font("Ivy Mode", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEsoftHospitals.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(62)))));
            this.labelEsoftHospitals.Location = new System.Drawing.Point(385, 26);
            this.labelEsoftHospitals.Name = "labelEsoftHospitals";
            this.labelEsoftHospitals.Size = new System.Drawing.Size(261, 46);
            this.labelEsoftHospitals.TabIndex = 11;
            this.labelEsoftHospitals.Text = "Esoft Hospitals";
            // 
            // formOverview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(243)))));
            this.ClientSize = new System.Drawing.Size(980, 664);
            this.Controls.Add(this.customPanel7);
            this.Controls.Add(this.customPanel6);
            this.Controls.Add(this.customPanel5);
            this.Controls.Add(this.customPanel4);
            this.Controls.Add(this.customPanel3);
            this.Controls.Add(this.customPanel2);
            this.Controls.Add(this.customPanel1);
            this.Name = "formOverview";
            this.Text = "formOverview";
            this.customPanel7.ResumeLayout(false);
            this.customPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barChart)).EndInit();
            this.customPanel6.ResumeLayout(false);
            this.customPanel6.PerformLayout();
            this.customPanel5.ResumeLayout(false);
            this.customPanel5.PerformLayout();
            this.customPanel4.ResumeLayout(false);
            this.customPanel4.PerformLayout();
            this.customPanel3.ResumeLayout(false);
            this.customPanel3.PerformLayout();
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.donutChart)).EndInit();
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomPanel customPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelEsoftHospitals;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private CustomPanel customPanel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart donutChart;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private CustomPanel customPanel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private CustomPanel customPanel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private CustomPanel customPanel5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private CustomPanel customPanel6;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private CustomPanel customPanel7;
        private System.Windows.Forms.DataVisualization.Charting.Chart barChart;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private System.Windows.Forms.Label lblStaffTotal;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblTotalAdd;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblCompleted;
        private System.Windows.Forms.Label lblInprogress;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblNurNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPhyNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblAlertNo;
    }
}