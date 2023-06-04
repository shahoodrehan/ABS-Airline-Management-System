using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMSProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            int seatWidth = 50;
            int seatHeight = 50;
            int numSeatsPerRow = 6;
            int numRows = 20;
            int aisleWidth = 5;
            int xPadding = 10;
            int yPadding = 10;

            // Create the seats map
            for (int row = 0; row < numRows; row++)
            {
                for (int seat = 0; seat < numSeatsPerRow; seat++)
                {
                    int xPos = xPadding + seat * (seatWidth + aisleWidth);
                    int yPos = yPadding + row * seatHeight;

                    if (seat > 2)
                    {
                        xPos = xPadding * 5 + seat * (seatWidth + aisleWidth);
                    }
                    if (row>3)
                    {
                         yPos = yPadding*5 + row * seatHeight;
                    }
                    if (row >7 )
                    {
                        yPos = yPadding *10  + row * seatHeight;
                    }

                    // Determine the position of the seat on the form

                    // Create a button to represent the seat
                    Button seatButton = new Button();
                    //seatButton.Text = $"{(char)('A' + row)}{seat + 1}";
                    seatButton.Size = new Size(seatWidth, seatHeight);
                    seatButton.Location = new Point(xPos, yPos);

                    // Set the background color of the button based on whether the seat is available or not
                    bool isAvailable = true; // Replace with actual availability status
                    if (isAvailable == true)
                    {
                        seatButton.BackColor = Color.Green;
                    }
                    else
                    {
                        seatButton.BackColor = Color.Red;
                        seatButton.Enabled = false;
                    }

                    // Add the button to the form
                    this.Controls.Add(seatButton);
                }
            }
        }
    }
}
