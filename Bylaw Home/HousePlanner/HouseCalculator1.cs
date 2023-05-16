//Syed Kaab Surkhi
//April 14, 2023
//Mr. A. MAyer
//A House calculator that calculates possible house dimensions according to lot dimensions

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HousePlanner
{
    public partial class HouseCalculator1 : Form
    {
        public HouseCalculator1()
        {
            InitializeComponent();
        }
        //Create variables to store inputted dimensions
        public string lotLength;
        public string lotWidth;
        public double houseLength;
        public double houseWidth;
        //Set the maximum pixels to draw for the lot width and height in the follwoing varibales
        public int MaxWidthLot = 500;
        public int MaxLengthLot = 250;
        //Create a pen to draw the lot
        Pen p = new Pen(Color.Blue, 3);
        //Create variables to store the midpoint of the length and width of the lot
        public double midpointX;
        public double midpointY;
        //Create a pen to draw the house
        Pen w = new Pen(Color.Green, 3);
        //Create a condition variable that stores wether imput values are good or bad
        public string condition;

        private void Verify_btn_Click(object sender, EventArgs e)
        {
            //Try the following code to make sure user has not imputted non integers
            try
            {
                //If the inputted code is "00", then end the program
                if (LengthLot_txt.Text == "00" || WidthLot_txt.Text == "00" || HouseLength_txt.Text == "00" || HouseWidth_txt.Text == "00")
                {
                    this.Close();
                }
                //Take user input and store it
                lotLength = LengthLot_txt.Text;
                lotWidth = WidthLot_txt.Text;
                houseWidth = Convert.ToDouble(HouseWidth_txt.Text);
                houseLength = Convert.ToDouble(HouseLength_txt.Text);
                //Set start condition as "Right"
                condition = "Right";
                //Cear the items in itembox
                OutputBox_txt.Items.Clear();

                //Check if inputted values meet the requirements to build a house and set the condition as "wrong" if they dont match
                //If there is less than 2 m (4 m total on vertical and 4 m on horizontal) of space between the lot and the house
                if ((houseWidth > (Convert.ToDouble(lotWidth) - 4)) || (houseLength > (Convert.ToDouble(lotLength) - 4)))
                {
                    condition = "Wrong";
                    OutputBox_txt.Items.Add("Clearance Requirements not met");
                }
                //IF house area is less than 25% of lot area
                if ((houseLength * houseWidth) < 0.25 * ((Convert.ToDouble(lotWidth)) * (Convert.ToDouble(lotLength))))
                {
                    condition = "Wrong";
                    OutputBox_txt.Items.Add("Does not Meet Min Area");
                }
                //If house area is greater than 40% of lot area
                if ((houseLength * houseWidth) > 0.4 * ((Convert.ToDouble(lotWidth)) * (Convert.ToDouble(lotLength))))
                {
                    condition = "Wrong";
                    OutputBox_txt.Items.Add("Max Area exceeded");
                }
                //If condition stays as "Right"
                if (condition == "Right")
                {
                    OutputBox_txt.Items.Add("Fits the lot, congrats!");
                    OutputBox_txt.Items.Add("Better call Saul!");
                    FaceBox_img.Image = Properties.Resources.happy4;
                    w.Color = Color.Green;
                }
                //IF condition is "Wrong"
                if (condition == "Wrong")
                {
                    FaceBox_img.Image = Properties.Resources.sad2;
                    OutputBox_txt.Items.Add("Let's change dimensions!");
                    w.Color = Color.Red;

                }

                //Variables that will store the ratios of length to width for the lot
                double Lengthratio = Convert.ToDouble(lotLength) / Convert.ToDouble(lotWidth);
                double Widthratio = Convert.ToDouble(lotWidth) / Convert.ToDouble(lotLength);
                //Variables that will store the rations of length to width of the house
                double houseWidthRatio = Convert.ToDouble(houseWidth) / Convert.ToDouble(lotWidth);
                double houseLengthRatio = Convert.ToDouble(houseLength) / Convert.ToDouble(lotLength);

                //Drawing calculations
                if (Lengthratio <= 0.5)
                {
                    int legnth = Convert.ToInt32(Math.Round(MaxWidthLot * Lengthratio, 0));
                    Graphics g = this.CreateGraphics();
                    Rectangle shape = new Rectangle(25, 25, MaxWidthLot, legnth);
                    g.DrawRectangle(p, shape);

                    midpointX = Math.Round((Convert.ToDouble(25 + 25 + MaxWidthLot) / 2), 0);
                    midpointY = Math.Round((Convert.ToDouble(25 + 25 + legnth) / 2), 0);


                    Graphics l = this.CreateGraphics();
                    Rectangle shapes = new Rectangle(Convert.ToInt32(Math.Round((midpointX - ((houseWidthRatio * MaxWidthLot) / 2)), 0)), Convert.ToInt32(Math.Round((midpointY - ((houseLengthRatio * legnth) / 2)), 0)), Convert.ToInt32(houseWidthRatio * MaxWidthLot), Convert.ToInt32(houseLengthRatio * legnth));
                    l.DrawRectangle(w, shapes);
                }
                else if (Widthratio <= 2)
                {
                    int width = Convert.ToInt32(Math.Round(MaxLengthLot * Widthratio, 0));
                    Graphics g = this.CreateGraphics();
                    Rectangle shape = new Rectangle(25, 25, width, MaxLengthLot);
                    g.DrawRectangle(p, shape);

                    midpointX = Math.Round((Convert.ToDouble(25 + 25 + width) / 2), 0);
                    midpointY = Math.Round((Convert.ToDouble(25 + 25 + MaxLengthLot) / 2), 0);

                    Graphics l = this.CreateGraphics();
                    Rectangle shapes = new Rectangle(Convert.ToInt32(Math.Round((midpointX - ((houseWidthRatio * width) / 2)), 0)), Convert.ToInt32(Math.Round((midpointY - ((houseLengthRatio * MaxLengthLot) / 2)), 0)), Convert.ToInt32(houseWidthRatio * width), Convert.ToInt32(houseLengthRatio * MaxLengthLot));
                    l.DrawRectangle(w, shapes);
                }
                else if (Lengthratio == 1)
                {
                    Graphics g = this.CreateGraphics();
                    Rectangle shape = new Rectangle(25, 25, 250, 250);
                    g.DrawRectangle(p, shape);
                    midpointX = Math.Round((Convert.ToDouble(25 + 25 + 250) / 2), 0);

                }
                //Disbale the verify button
                Verify_btn.Enabled = false;
            }
            //If inputted values are non-integers
            catch
            {
                OutputBox_txt.Items.Clear();
                OutputBox_txt.Items.Add("Please input proper dimensions!");
            }
        }
        //If the "New" button is clicked
        private void NewCalc_btn_Click(object sender, EventArgs e)
        {
            //Clear the drawing 
            this.Invalidate();
            OutputBox_txt.Items.Clear();
            Verify_btn.Enabled = true;
            FaceBox_img.Image = Properties.Resources.happy4;
            OutputBox_txt.Items.Add("Welcome!");
            OutputBox_txt.Items.Add("I'm here to help you!");
        }

        private void HouseCalculator1_Load(object sender, EventArgs e)
        {
            FaceBox_img.Image = Properties.Resources.happy4;
            OutputBox_txt.Items.Add("Welcome!");
            OutputBox_txt.Items.Add("I'm here to help you!");
        }
    }
}
