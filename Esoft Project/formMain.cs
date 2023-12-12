using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Esoft_Project.Forms;
using FontAwesome.Sharp;

namespace Esoft_Project
{
    public partial class formMain : KryptonForm
    {
        private List<CustomPanel> customPanels;
        private IconButton currentBtn;
        private Form currentChildForm;
        public formMain()
        {
            InitializeComponent();
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            customPanels = new List<CustomPanel>
            {
            panelContainer1,
            panelContainer2,
            panelContainer3,
            panelContainer4,
            panelContainer5,
            panelContainer6,
            panelContainer7
            };
        }
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(0, 44, 62); 
            public static Color color2 = Color.FromArgb(120, 188, 196); 
            public static Color color3 = Color.FromArgb(247, 248, 243); 
            public static Color color4 = Color.FromArgb(247, 68, 78); 
        }

  
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton(senderBtn);
                //Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.ForeColor = color;
                currentBtn.IconColor = color;

                lblCurrent.Text = currentBtn.Text;
                CustomPanel parentPanel = (CustomPanel)((Button)senderBtn).Parent;
                parentPanel.BackColor = Color.Transparent;

                CustomPanel grandparentPanel = (CustomPanel)parentPanel.Parent;
                grandparentPanel.BackColor = RGBColors.color3;
                grandparentPanel.UseGradientFill = false;
            }
        }
       
        private void DisableButton(object senderBtn)
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.Transparent;
                currentBtn.ForeColor = RGBColors.color1;
                currentBtn.IconColor = RGBColors.color1;
                CustomPanel parentPanel = (CustomPanel)((Button)currentBtn).Parent;
                parentPanel.BackColor = Color.White;

                CustomPanel grandparentPanel = (CustomPanel)parentPanel.Parent;
                grandparentPanel.BackColor = Color.White;
                grandparentPanel.UseGradientFill = true;
            }
        }
        private void formMain_Load(object sender, EventArgs e)
        {
            btnOverview.PerformClick();
        }

        private void OpenChildForm(Form childForm)
        {
            //open only form
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            //End
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelFormContainer.Controls.Add(childForm);
            panelFormContainer.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnPatients_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            OpenChildForm(new formOverview());
        }

        private void btnRecords_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            OpenChildForm(new formRecords());
        }

        private void btnAlerts_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            OpenChildForm(new formEmergency());
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            OpenChildForm(new formInventory());
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            OpenChildForm(new formStaff());
        }

        private void btnTests_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            OpenChildForm(new formTests());
        }

        private void btnBilling_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            OpenChildForm(new formBilling());
        }

        //Drag form from title panel
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            SystemSounds.Exclamation.Play();
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void userIcon_Click(object sender, EventArgs e)
        {
            SystemSounds.Exclamation.Play();
            DialogResult result = MessageBox.Show("Are you sure you want to Logout?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                formLogin obj = new formLogin();
                obj.Show();
            }
        }
    }
}
