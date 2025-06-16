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
    public partial class SummaryDataScreen : Form
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

        public SummaryDataScreen()
        {
            InitializeComponent();
            //Removing the Border of the Form
            this.FormBorderStyle = FormBorderStyle.None;
            //Making the form curved
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 45, 45));
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
            }
        }

        //Event that allows the user to drag the form based on mouse click
        private void SummaryDataScreenMouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);

        }

        //Event that makes the form dragable when the mouse is moving
        private void SummaryDataScreenMouseMove(object sender, MouseEventArgs e)
        {
            //If the user left clicks on the form it is able to be dragged 
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        //An event that reads .XML file and also displays the data within the dataGrid
        private void SummaryDataScreen_Load(object sender, EventArgs e)
        {
            // Reads the .XML file
            ReadXMLFile();

            // Add columns to the DataGridView
            DataGridSummaryData.ColumnCount = 3;
            DataGridSummaryData.Columns[0].Name = "Bus No.";
            DataGridSummaryData.Columns[1].Name = "Last 5 Arrival Times";
            DataGridSummaryData.Columns[2].Name = "Est. Arrival Time";

            // Populate the DataGridView
            foreach (Bus bus in buses)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(DataGridSummaryData);
                row.Cells[0].Value = bus.BusNumber;

                // Display the last 5 arrival times in Column2
                string[] last5ArrivalTimes = bus.ArrivalTimes
                    .OrderByDescending(time => time)
                    .Take(5)
                    .Select(time => time.ToString("HH:mm"))
                    .ToArray();
                row.Cells[1].Value = string.Join(", ", last5ArrivalTimes);

                // Calculate the average expected arrival time
                TimeSpan averageExpectedTime = CalculateAverageExpectedArrivalTime(bus.BusNumber);
                row.Cells[2].Value = averageExpectedTime.ToString(@"hh\:mm");

                DataGridSummaryData.Rows.Add(row);
            }

            // Add the event handler for CellValueChanged
            DataGridSummaryData.CellValueChanged += DataGridSummaryData_CellValueChanged;
        }

        //An event that checks if the user has modified contents in the Arrival Time cells and thus
        //re-calculates Estimated Arrival Time Accordingly additionally also making sure the user input is
        //in the correct format (date time format)
        private void DataGridSummaryData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1) // Column2 (Last 5 Arrival Times)
            {
                // Get the modified arrival times string
                string modifiedArrivalTimes = DataGridSummaryData.Rows[e.RowIndex].Cells[1].Value.ToString();

                // Validate and parse the modified arrival times
                List<DateTime> newArrivalTimes = new List<DateTime>();
                string[] timeStrings = modifiedArrivalTimes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string timeString in timeStrings)
                {
                    DateTime arrivalTime;
                    if (DateTime.TryParseExact(timeString.Trim(), "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out arrivalTime))
                    {
                        newArrivalTimes.Add(arrivalTime);
                    }
                    else
                    {
                        MessageBox.Show("Invalid data format in Last 5 Arrival Times. Please use HH:mm format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Restore the original value
                        DataGridSummaryData.Rows.Clear();

                        //ReDisplays the data within the data grid
                        foreach (Bus bus in buses)
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(DataGridSummaryData);
                            row.Cells[0].Value = bus.BusNumber;

                            // Display the last 5 arrival times in Column2
                            string[] last5ArrivalTimes = bus.ArrivalTimes
                                .OrderByDescending(time => time)
                                .Take(5)
                                .Select(time => time.ToString("HH:mm"))
                                .ToArray();
                            row.Cells[1].Value = string.Join(", ", last5ArrivalTimes);

                            // Calculate the average expected arrival time
                            TimeSpan averageExpectedTime = CalculateAverageExpectedArrivalTime(bus.BusNumber);
                            row.Cells[2].Value = averageExpectedTime.ToString(@"hh\:mm");

                            DataGridSummaryData.Rows.Add(row);
                        }
                        return;
                    }
                }

                // Find the bus with the specified ID
                int busId = Convert.ToInt32(DataGridSummaryData.Rows[e.RowIndex].Cells[0].Value);
                Bus selectedBus = buses.FirstOrDefault(bus => bus.BusNumber == busId);

                if (selectedBus != null)
                {
                    // Update the arrival times of the selected bus
                    selectedBus.ArrivalTimes = newArrivalTimes;
                    // Recalculate the average expected arrival time
                    TimeSpan averageExpectedTime = CalculateAverageExpectedArrivalTime(busId);
                    DataGridSummaryData.Rows[e.RowIndex].Cells[2].Value = averageExpectedTime.ToString(@"hh\:mm");
                    // Show the message box to confirm the modification
                    MessageBox.Show("Time modified successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //An event that saves contents to xml file when button is pressed after user has modified contents
        //in the datagrid
         private void btnSave_Click(object sender, EventArgs e)
         {
             //Validation
             try
             {
                 // Load the XML document
                 XDocument xdoc = XDocument.Load("Bus_Summary_Data.xml");

                 // Iterate through the DataGridView to update the XML
                 foreach (DataGridViewRow row in DataGridSummaryData.Rows)
                 {
                     int busId = Convert.ToInt32(row.Cells[0].Value);
                     Bus selectedBus = buses.FirstOrDefault(bus => bus.BusNumber == busId);

                     if (selectedBus != null)
                     {
                         // Get the Bus element in the XML with the matching ID
                         XElement busNode = xdoc.Descendants("Bus").FirstOrDefault(bus => (int)bus.Attribute("id") == busId);

                         if (busNode != null)
                         {
                             // Remove the last 5 ArrivalTime elements from the XML
                             busNode.Descendants()
                                    .Where(elem => elem.Name.LocalName.StartsWith("ArrivalTime_"))
                                    .OrderByDescending(elem => int.Parse(elem.Name.LocalName.Substring("ArrivalTime_".Length)))
                                    .Take(5)
                                    .Remove();

                             // Add the modified arrival times to the Bus element
                             foreach (DateTime arrivalTime in selectedBus.ArrivalTimes)
                             {
                                 int arrivalTimeNumber = busNode.Descendants()
                                                                .Where(elem => elem.Name.LocalName.StartsWith("ArrivalTime_"))
                                                                .Count() + 1;

                                 XElement arrivalTimeNode = new XElement("ArrivalTime_" + arrivalTimeNumber.ToString("D2"), arrivalTime.ToString("HH:mm"));
                                 busNode.Element("Arrival_Times").Add(arrivalTimeNode);
                             }
                         }
                     }
                 }

                 // Save the modified XML back to the file
                 xdoc.Save("Bus_Summary_Data.xml");

                 MessageBox.Show("Data saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
             }
             catch (Exception ex)
             {
                 MessageBox.Show("An error occurred while saving the data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
         }

        //Event that exits the application when user presses logout button
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Event that takes the user to the MainMenuAdmin screen when the menu button is clicked
        private void btnMenu_Click(object sender, EventArgs e)
        {
            //Define the form
            var Form1 = new MainMenuScreenAdmin();
            //Show the Main menu screen
            Form1.Show();

            //Close the help screen
            this.Close();
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
