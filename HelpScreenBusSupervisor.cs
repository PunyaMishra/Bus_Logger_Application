using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Net.Mail;


namespace Bus_Logger_Application
{
    public partial class HelpScreenBusSupervisor : Form
    {

        //Fields for making the form curved and dragabble
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
         );
        public Point mouseLocation;

        public HelpScreenBusSupervisor()
        {
            InitializeComponent();
            //Removing the Border of the Form
            this.FormBorderStyle = FormBorderStyle.None;
            //Making the form curved
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 45, 45));
        }

        //Event that allows the user to drag the form based on mouse click
        private void HelpScreenBusSupervisorMouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);

        }

        //Event that makes the form dragable when the mouse is moving
        private void HelpScreenBusSupervisorMouseMove(object sender, MouseEventArgs e)
        {
            //If the user left clicks on the form it is able to be dragged 
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        //Event that exits the application when user presses logout button
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Event that lets user email support when the contact support button is pressed
        private void btnContactSupport_Click(object sender, EventArgs e)
        {
            //Sends user to prompt an email to support with any issues encountered within the application using a hyperlink
            System.Diagnostics.Process.Start("mailto:buslogger_support@gmail.com?subject=Problems%20with%20using%20application%20");
        }

        //Event that takes the user to the MainMenuBusSupervisor screen when the menu button is clicked
        private void btnMenu_Click(object sender, EventArgs e)
        {
            //Define the form
            var Form1 = new MainMenuScreenBusSupervisor();
            //Show the Main menu screen
            Form1.Show();

            //Close the help screen
            this.Close();
        }
    }
}
