// Grading ID k2780
// Program 2
// CIS 199-02
// Due: 10/16/2019

// This application calculates the earliest registration date
// and time for an undergraduate student given their class standing
// and last name.
// Decisions based on UofL Spring 2020 Priority Registration Schedule

// Solution 3
// This solution keeps the first letter of the last name as a char
// and uses if/else logic for the times.
// It uses defined strings for the dates and times to make it easier
// to maintain.
// It only uses programming elements introduced in the text or
// in class.
// This solution takes advantage of the fact that there really are
// only two different time patterns used. One for juniors and seniors
// and one for sophomores and freshmen. The pattern for sophomores
// and freshmen is complicated by the fact the certain letter ranges
// get one date and other letter ranges get another date.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prog3
{
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }

        // Find and display earliest registration time
        private void FindRegTimeBtn_Click(object sender, EventArgs e)
        {
            const string DAY1 = "November 4"; // 1st day of registration
            const string DAY2 = "November 5"; // 2nd day of registration
            const string DAY3 = "November 6"; // 3rd day of registration
            const string DAY4 = "November 7";  // 4th day of registration
            const string DAY5 = "November 8";  // 5th day of registration
            const string DAY6 = "November 11";  // 6th day of registration

            const string TIME1 = "8:30 AM";  // 1st time block
            const string TIME2 = "10:00 AM"; // 2nd time block
            const string TIME3 = "11:30 AM"; // 3rd time block
            const string TIME4 = "2:00 PM";  // 4th time block
            const string TIME5 = "4:00 PM";  // 5th time block

            char[] upperClass = { 'D', 'I', 'O', 'S', 'Z' };  // First letter of last name for junion and senior
            string[] upperClassTime = { TIME3, TIME4, TIME5, TIME1, TIME2 }; // Time for each first latter of last name to register
            char[] lowerClass = { 'B', 'D', 'F', 'I', 'L', 'O', 'Q', 'S', 'V', 'Z' };   // First letter of last name for freshmen and sophomore
            string[] lowerClassTime = { TIME5, TIME1, TIME2, TIME3, TIME4, TIME5, TIME1, TIME2, TIME3, TIME4 };   // Time for each first latter of last name to register

            const float SOPHOMORE = 30; // Hours needed to be sophomore
            const float JUNIOR = 60;    // Hours needed to be junior
            const float SENIOR = 90;    // Hours needed to be senior

            string lastNameStr;         // Entered last name
            char lastNameLetterCh;      // First letter of last name, as char
            string dateStr = "Error";   // Holds date of registration
            string timeStr = "Error";   // Holds time of registration
            float creditHours;          // Previously earned credit hours
            bool found = false;          // Upperclass or not?
            bool isUpperClass;           // declearing is upper
            int index = 0;               // declearing that index is equal to zero

            lastNameStr = lastNameTxt.Text;
            if (lastNameStr.Length > 0) // Empty string?
            {
                lastNameLetterCh = lastNameStr[0];   // First char of last name
                lastNameLetterCh = char.ToUpper(lastNameLetterCh); // Ensure upper case

                if (float.TryParse(creditHoursTxt.Text, out creditHours) && creditHours >= 0)
                {
                    if (char.IsLetter(lastNameLetterCh)) // Is it a letter?
                    {
                        isUpperClass = (creditHours >= JUNIOR);

                        // Juniors and Seniors share same schedule but different days
                        if (isUpperClass)
                        {
                            if (creditHours >= SENIOR)
                                dateStr = DAY1;
                            else // Must be juniors
                                dateStr = DAY2;

                            while (index <= upperClass.Length && !found)    // if index is less than or equal to upperclass
                                if (lastNameLetterCh >= upperClass[index]) // if last name letter is greater than or equal to upper class refer to index
                                {
                                    found = true;    // if last name and time correspond that it will go through 
                                }
                                else
                                {
                                    ++index;  // add on to index
                                }
                            if (found)
                            {
                                timeStr = upperClassTime[index];   // output 

                            }

                            // Sophomores and Freshmen
                            else // Must be soph/fresh
                            {
                                if (creditHours >= SOPHOMORE)
                                {
                                    // A-B, P-Z on day one
                                    if ((lastNameLetterCh <= 'B') ||  // <= B
                                        (lastNameLetterCh >= 'P'))    // >= P
                                        dateStr = DAY3;
                                    else // All other letters on next day
                                        dateStr = DAY4;
                                }
                                else // must be freshman
                                {
                                    // A-B, P-Z on day one
                                    if ((lastNameLetterCh <= 'B') ||  // <= B
                                        (lastNameLetterCh >= 'P'))    // >= P
                                        dateStr = DAY5;
                                    else // All other letters on next day
                                        dateStr = DAY6;
                                }

                                while (index <= lowerClass.Length && !found)
                                    if (lastNameLetterCh >= lowerClass[index])  // if last name letter is greater than or equal to lower class refer to index
                                    {
                                        found = true;   // if last name and time correspond that it will go through 
                                    }
                                    else
                                    {
                                        ++index;    // add on to index
                                    }
                                if (found)
                                {
                                    timeStr = lowerClassTime[index];  // output

                                }
                            }

                            // Output results
                            dateTimeLbl.Text = $"{dateStr} at {timeStr}";
                        }
                        else // Not A-Z
                            MessageBox.Show("Make sure last name starts with a letter!");
                    }
                    else
                        MessageBox.Show("Enter a valid number of credit hours!");
                }
                else // Empty textbox
                    MessageBox.Show("Please enter last name!");
            }
        }
    }
}
