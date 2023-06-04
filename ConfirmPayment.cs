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
    public partial class ConfirmPayment : Form
    {
        public ConfirmPayment()
        {
            InitializeComponent();
        }

        private void btn_verify_Click(object sender, EventArgs e)
        {
            Flights flights = new Flights();

            if (flights.VerifyOtp(txt_otp.Text))
            {
                MessageBox.Show("Payment Succesfull");
                this.Close();
            }
            else
            {
                MessageBox.Show("incorrect otp");
            }
        }

        private void btn_resend_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
