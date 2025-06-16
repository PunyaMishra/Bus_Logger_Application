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
using System.Net.Mail;
using System.IO;

namespace Bus_Logger_Application
{
    public partial class Login_Screen : Form
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

        //Variables for reading and processing the user_list csv file
        public const string filePath = "User_List.csv";
        

        //Other Variables
        MainMenuScreenAdmin mainMenuScreenAdmin;
        MainMenuScreenBusSupervisor mainMenuScreenBusSupervisor;


        public Login_Screen()
        {

            InitializeComponent();
            //Removing the Border of the Form
            this.FormBorderStyle = FormBorderStyle.None;
            //Making the form curved
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 45, 45));
            
            
        }

        //Event that checks to see if the user's inputted username and password
        //matches the same present in the User_List.csv file and checks the user's
        //User type and accordingly displays either the Admin Menu if they are an
        //Admin or the Bus Supervisor Menu if they are a Bus Supervisor when the
        //user clicks the login button
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Data Validation
            if (string.IsNullOrWhiteSpace(txtBxPassword.Text) && string.IsNullOrWhiteSpace(txtBxUsername.Text) || string.IsNullOrWhiteSpace(txtBxPassword.Text) || string.IsNullOrWhiteSpace(txtBxUsername.Text) || txtBxPassword.Text == "Password" && txtBxUsername.Text == "Username" || txtBxPassword.Text == "Password" || txtBxUsername.Text == "Username") 
            {
                MessageBox.Show("Cannot have an empty Username or Password");
                return;
            }
            else
            {
                //Calls the ValidateUser Function
                ValidateUser(txtBxUsername.Text, txtBxPassword.Text);
            }
            
            
        }

        //Event that sends user to email support when user presses the can't login button
        private void btnCantLogin_Click(object sender, EventArgs e)
        {
            //Sends user to prompt an email to support with any issues encountered within the application using a hyperlink
            System.Diagnostics.Process.Start("mailto:buslogger_support@gmail.com?subject=Problems%20with%20using%20application%20");
        }

        //Event that allows the user to drag the form based on mouse click
        private void Login_Screen_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);

        }

        //Event that makes the form dragable when the mouse is moving
        private void Login_Screen_MouseMove(object sender, MouseEventArgs e)
        {
            //If the user left clicks on the form it is able to be dragged 
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        //Private Subroutines/Functions

        //A function which checks to see if the user has inputted correct username and passwords
        //and thus checks the user's userType within the .csv file and displays different
        //menus based on the user's userType. 
        private string ValidateUser(string Username, string Password)
        {
            //Reads the csv file
            string[] lines = File.ReadAllLines(filePath);

            //boolean variables for incorrect password/username messagebox display checking
            bool correctUsername = false;
            bool correctPassword = false;

            //Increment variable
            int i = 0;

            //a while loop is utilized
            while (i < lines.Length)
            {
                string line = lines[i];

                //Parse the line
                string[] lineData = line.Split(',');

                //Define variables 
                string username = lineData[0];
                string password = lineData[1];
                string userType = lineData[2];

                //If the username the user has inputted matches
                if(Username == username)
                {
                    correctUsername = true;
                    //If the password the user has inputted matches
                    if(Password == password)
                    {
                        correctPassword = true;
                        //Checks user's userType
                        if(userType == "Admin")
                        {
                          
                            //display MainMenu Admin Screen
                            var Form2 = new MainMenuScreenAdmin();
                            Form2.Show();
                            //Hides Login Screen
                            this.Hide();

                        }
                        else if(userType == "Bus Supervisor")
                        {
                            
                            //display's MainMenu Bus Supervisor Screen
                            var Form2 = new MainMenuScreenBusSupervisor();
                            Form2.Show();
                            //Hides Login Screen
                            this.Hide();
                        }
                    }
                  
                } 
                i++;
            }

            //If usernames and passwords don't match with those in the .csv file a message is displayed
            if (!correctUsername  || !correctPassword)
            {

                MessageBox.Show("Incorrect username or password");
               
            }
            //If none of the fields match, returns Invalid Username or Password inputted by the user
            return "Invalid Username or Password";   
        }
    }
}

