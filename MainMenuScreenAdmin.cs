using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;
using System.Linq;


namespace Bus_Logger_Application
{
    public partial class MainMenuScreenAdmin : Form
    {
        //Create a record
        class Bus
        {
            //Assign record attributes
            public int BusNumber;
            public string BusName;
            public string RouteDetails;
            public DateTime ExpectedArrivalTime;
            public List<DateTime> ArrivalTimes;
            public List<DateTime> Dates;
        }

        //Create an empty array of records
        Bus[] buses = null;

        //Fields for displaying Bus Logger Output
        private Timer timerNotification;

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

        public MainMenuScreenAdmin()
        {
            InitializeComponent();
            //Removing the Border of the Form
            this.FormBorderStyle = FormBorderStyle.None;
            //Making the form curved
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 45, 45));

            //Set Bus Logger Output as invisible initially
            pictureBxLogOutput.Visible = false;

            //Adding a timer for my bus logger output to display on button click for 2 seconds
            timerNotification = new Timer();
            timerNotification.Interval = 2000; //2000 milliseconds = 2 seconds
            timerNotification.Tick += TimerNotification_Tick;

        }

        private void MainMenuScreenAdmin_Load(object sender, EventArgs e)
        {
            ReadXMLFile();
            //Making the combo box not able to be typed and keeping the styles/colors the same for aesthetics
            cmbBxBusNumber.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBxBusNumber.FlatStyle = FlatStyle.Flat;
        }

        //Event that allows the user to drag the form based on mouse click
        private void MainMenuScreenAdminMouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);

        }

        //Event that makes the form dragable when the mouse is moving
        private void MainMenuScreenAdminMouseMove(object sender, MouseEventArgs e)
        {
            //If the user left clicks on the form it is able to be dragged 
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        // Event that When the user changes the combo box selection displays Bus details, based on bus number
        //selected
        private void cmbBxBusNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if any item is selected
            if (cmbBxBusNumber.SelectedIndex != -1)
            {
                int selectedBusId = Convert.ToInt32(cmbBxBusNumber.SelectedItem);
                Bus selectedBus = buses.FirstOrDefault(bus => bus.BusNumber == selectedBusId);

                // Check if the selected bus is found
                if (selectedBus != null)
                {
                    // Display bus name and route details
                    lblBusName.Text = selectedBus.BusName;
                    lblRouteDetails.Text = selectedBus.RouteDetails;

                    // Calculate and display expected arrival time
                    TimeSpan averageArrivalTime = CalculateAverageExpectedArrivalTime(selectedBusId);
                    lblExpectedArrivalTime.Text = "Expected Arrival Time: " + averageArrivalTime.ToString(@"hh\:mm");
                }
            }
        }

        //Subroutine that reads the xml file
        private void ReadXMLFile()
        {
            XmlDocument doc = new XmlDocument();
            //Load the xml document
            doc.Load("Bus_Summary_Data.xml");

            //Separate xml contents based on tag, "Bus"
            XmlNodeList xmlBusList = doc.GetElementsByTagName("Bus");

            //Assiging length of the empty record array that was made earlier
            buses = new Bus[xmlBusList.Count];

            //Create an increment variable
            int index = 0;

            //Foreach loop that gets and assigns child attributes in each "Bus" tag
            foreach (XmlNode xmlBus in xmlBusList)
            {
                // Create a new record
                Bus theBus = new Bus();
                // Get and store the Bus Number attribute
                theBus.BusNumber = Convert.ToInt32(xmlBus.Attributes["id"].Value);

                // Get and store children attributes
                theBus.BusName = xmlBus["Bus_Name"].InnerText;
                theBus.RouteDetails = xmlBus["Route_Details"].InnerText;

                // Store Arrival Times
                theBus.ArrivalTimes = new List<DateTime>();
                XmlNodeList xmlArrivalTimes = xmlBus["Arrival_Times"].ChildNodes;
                foreach (XmlNode xmlArrivalTime in xmlArrivalTimes)
                {
                    DateTime arrivalTime;
                    if (DateTime.TryParseExact(xmlArrivalTime.InnerText, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out arrivalTime))
                    {
                        theBus.ArrivalTimes.Add(arrivalTime);
                    }
                }

                // Store Dates
                theBus.Dates = new List<DateTime>();
                XmlNodeList xmlDates = xmlBus["Dates"].ChildNodes;
                foreach (XmlNode xmlDate in xmlDates)
                {
                    DateTime date;
                    if (DateTime.TryParseExact(xmlDate.InnerText, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    {
                        theBus.Dates.Add(date);
                    }
                }

                // Store the record in the array
                buses[index] = theBus;
                index++;

                //Add the bus number to the combo box
                cmbBxBusNumber.Items.Add(theBus.BusNumber);

                // Select the first bus number in the combo box by default
                if (cmbBxBusNumber.Items.Count > 0)
                {
                    cmbBxBusNumber.SelectedIndex = 0;
                }
            }

        }

        //Subroutine that adds new ArrivalTime and Date elements to the XML file
        private void WriteToXML(int busNumber)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Bus_Summary_Data.xml");

            // Find the Bus element with the given busNumber
            XmlNodeList xmlBusList = doc.GetElementsByTagName("Bus");
            foreach (XmlNode xmlBus in xmlBusList)
            {
                int currentBusNumber = Convert.ToInt32(xmlBus.Attributes["id"].Value);
                if (currentBusNumber == busNumber)
                {
                    // Get the Arrival_Times and Dates elements
                    XmlNode arrivalTimesNode = xmlBus.SelectSingleNode("Arrival_Times");
                    XmlNode datesNode = xmlBus.SelectSingleNode("Dates");

                    // Generate new element names with the next sequential number
                    int nextArrivalTimeNumber = arrivalTimesNode.ChildNodes.Count + 1;
                    int nextDateNumber = datesNode.ChildNodes.Count + 1;
                    string newArrivalTimeElementName = "ArrivalTime_" + nextArrivalTimeNumber;
                    string newDateElementName = "Date_" + nextDateNumber;

                    // Create new ArrivalTime and Date elements
                    XmlElement newArrivalTimeElement = doc.CreateElement(newArrivalTimeElementName);
                    XmlElement newDateElement = doc.CreateElement(newDateElementName);

                    // Set the inner text of the elements to the current time and date
                    newArrivalTimeElement.InnerText = DateTime.Now.ToString("HH:mm");
                    newDateElement.InnerText = DateTime.Now.ToString("dd/MM/yyyy");

                    // Append the new elements to Arrival_Times and Dates
                    arrivalTimesNode.AppendChild(newArrivalTimeElement);
                    datesNode.AppendChild(newDateElement);

                    break; // Break the loop since we found the correct bus
                }
            }

            // Save the changes back to the XML file
            doc.Save("Bus_Summary_Data.xml");
        }

        //Event for the button click to add new ArrivalTime and Date elements
        private void btnLog_Click(object sender, EventArgs e)
        {
            //Validation
            try
            {
                // Get the selected bus number from the combo box
                int selectedBusNumber = Convert.ToInt32(cmbBxBusNumber.SelectedItem);

                // Call the function to add new elements
                WriteToXML(selectedBusNumber);

                // Update the display and display Bus Log Output for 2 seconds
                pictureBxLogOutput.Visible = true;
                timerNotification.Start();
            }
            catch
            {
                MessageBox.Show("Unknown Error, Contact Support");
            }
      
        }

        //Event that exits the application when user presses logout button
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Event for making the Bus Logger Output invisible/visible
        private void TimerNotification_Tick(object sender, EventArgs e)
        {
            // Timer elapsed, hide the PictureBox
            pictureBxLogOutput.Visible = false;
            timerNotification.Stop();
        }

        //Event that sends user to email support when the Errors? button is pressed 
        private void btnErrorsHelp_Click(object sender, EventArgs e)
        {
            //Sends user to prompt an email to support with any issues encountered within the application using a hyperlink
            System.Diagnostics.Process.Start("mailto:buslogger_support@gmail.com?subject=Problems%20with%20using%20application%20");
        }

        //Event that takes the user to the HelpAdmin screen when the Help button is clicked
        private void btnHelp_Click(object sender, EventArgs e)
        {
            //Define the form
            var Form1 = new HelpScreenAdmin();
            //Show the Help screen
            Form1.Show();
            //Close the Main Menu screen
            this.Close();

        }

        //Event that takes the user to the SummaryData screen when the View Summary Data button is pressed
        private void btnViewSummaryData_Click(object sender, EventArgs e)
        {
            //Define the form
            var Form1 = new SummaryDataScreen();
            //Show the Summary Data Screen
            Form1.Show();
            //Close the Main Menu Screen
            this.Close();
        }

        //Functions

        //A function that calculates the average expected arrival time for each bus
        private TimeSpan CalculateAverageExpectedArrivalTime(int busId)
        {
            // Find the bus with the specified ID
            Bus selectedBus = buses.FirstOrDefault(bus => bus.BusNumber == busId);

            if (selectedBus == null || selectedBus.ArrivalTimes.Count == 0)
            {
                return TimeSpan.Zero; // Return zero timespan if no matching bus or no arrival times
            }

            // Get the sum of the TimeSpan of all the arrival times
            TimeSpan sum = TimeSpan.Zero;
            foreach (DateTime arrivalTime in selectedBus.ArrivalTimes)
            {
                sum += arrivalTime.TimeOfDay;
            }

            // Calculate the average TimeSpan
            TimeSpan averageTime = TimeSpan.FromTicks(sum.Ticks / selectedBus.ArrivalTimes.Count);

            return averageTime;
        }

    }
}
