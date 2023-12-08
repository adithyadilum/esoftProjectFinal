using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using FontAwesome.Sharp;

namespace Esoft_Project
{
    public partial class formMain : KryptonForm
    {
        private IconButton currentBtn;
        public formMain()
        {
            InitializeComponent();
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
        }
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(55, 50, 52); 
            public static Color color2 = Color.FromArgb(232, 220, 204); 
            public static Color color3 = Color.FromArgb(243, 236, 228); 
            public static Color color4 = Color.FromArgb(216, 67, 57); 
        }
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                //Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.ForeColor = color;
                currentBtn.IconColor = color;

                lblCurrent.Text = currentBtn.Text;
                CustomPanel parentPanel = (CustomPanel)((Button)senderBtn).Parent;
                parentPanel.TopLeftCornerRadius = 22;
                parentPanel.TopRightCornerRadius = 22;
                parentPanel.BottomLeftCornerRadius = 22;
                parentPanel.BottomRightCornerRadius = 22;
                parentPanel.BackColor = RGBColors.color3;

                CustomPanel grandparentPanel = (CustomPanel)parentPanel.Parent;
                grandparentPanel.BackColor = RGBColors.color3;
                grandparentPanel.UseGradientFill = false;
                grandparentPanel.TopLeftCornerRadius = 1;
                grandparentPanel.TopRightCornerRadius = 1;
                grandparentPanel.BottomLeftCornerRadius = 1;
                grandparentPanel.BottomRightCornerRadius = 1;
            }
        }
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.Transparent;
                currentBtn.ForeColor = RGBColors.color1;
                currentBtn.IconColor = RGBColors.color1;
                CustomPanel parentPanel = (CustomPanel)((Button)currentBtn).Parent;
                CustomPanel grandparentPanel = (CustomPanel)parentPanel.Parent;
            }
        }
        private void formMain_Load(object sender, EventArgs e)
        {

        }

        private void btnPatients_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
        }

        private void btnRecords_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
          
        }

        private void btnAlerts_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
        
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
     
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
          
        }

        private void btnTests_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
           
        }

        private void btnBilling_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
   
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
            Application.Exit();
        }
    }
}
