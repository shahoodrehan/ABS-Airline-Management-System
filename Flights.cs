using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace DBMSProject
{
    public partial class Flights : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=IRFAN\RAZRIZ;Initial Catalog=AMS;Persist Security Info=True;User ID=admin;Password=Zxc121216");
        string user_id, seat, otp_code;
        bool verify = false;

        public Flights()
        {

            InitializeComponent();
            guna2Panel8.Hide();
            panel4.Hide();

        }

        System.Timers.Timer t;
        int h, s, m;
        public void time()
        {
            t = new System.Timers.Timer();
            t.Interval = 1000;
            t.Elapsed += OnTimeEvent;
            t.Start();

        }

        private void OnTimeEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            ConfirmPayment c = new ConfirmPayment();
            Invoke(new Action(() =>
            {
                s++;
                if (s == 60)
                {

                    t.Stop();
                    c.btn_resend.ForeColor = Color.Black;
                    c.btn_resend.Enabled = true;
                    c.btn_verify.Enabled = false;

                }
                c.lbl_time.Text = m.ToString().PadLeft(2, '0') + ":" + s.ToString().PadLeft(2, '0');

            }));
            t.AutoReset = true;

        }

        public string otp()
        {
            int len = 4;
            const string ValidChar = "1234567890";
            StringBuilder result = new StringBuilder();
            Random rand = new Random();
            while (0 < len--)
            {
                result.Append(ValidChar[rand.Next(ValidChar.Length)]);

            }
            return result.ToString();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void roundedPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuCustomLabel4_Click(object sender, EventArgs e)
        {

        }

        private void Flights_Load(object sender, EventArgs e)
        {
            // Show the loading page


        }

        private void ShowLoadingPage()
        {
            //// Create a new form to display the loading page
            //Form loadingForm = new Form();
            //loadingForm.StartPosition = FormStartPosition.CenterScreen;
            //loadingForm.FormBorderStyle = FormBorderStyle.None;
            //loadingForm.BackColor = Color.White;
            //loadingForm.TransparencyKey = Color.White;
            //loadingForm.ShowInTaskbar = false;

            //// Create a label to display a message on the loading page
            //Label loadingLabel = new Label();
            //loadingLabel.Text = "Please wait...";
            //loadingLabel.AutoSize = true;
            //loadingLabel.Location = new Point((loadingForm.ClientSize.Width - loadingLabel.Width) / 2,
            //                                   (loadingForm.ClientSize.Height - loadingLabel.Height) / 2);

            //// Add the label to the form's controls collection
            //loadingForm.Controls.Add(loadingLabel);

            //// Show the form as a modal dialog
            //loadingForm.ShowDialog();
        }

        private void HideLoadingPage()
        {
            // Close the loading page form
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name == "LoadingForm")
                {
                    form.Close();
                    break;
                }
            }

        }

        private void roundedButton4_Click(object sender, EventArgs e)
        {

            tabControl1.SelectedIndex = 1;
            if (Login.login_status.Equals(false))
            {
                MessageBox.Show("Kindly Login First to enjoy a smooth process\n you are directed to login page...");
                Login login = new Login();
                login.ShowDialog();
            }
        }

        private void roundedButton3_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "select * from users u inner join passport_details p on p.user_id=u.user_id where u.f_name='" + txt_fname_p.Text + "' and u.l_name='" + lbl_lname_p.Text + "'and u.dob='" + txt_dob_p.Text + "'and p.nationality='" + txt_nationality_p.Text + "'and p.pass_no='" + txt_passportno_p.Text + "'and p.pass_exp='" + txt_exp_p.Text + "'and u.phone_num='" + txt_pno_p.Text + "'and u.email='" + txt_email_p.Text + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                lbl_dob_t.Text = txt_dob_p.Text;
                user_id = Convert.ToString(reader["user_id"]);
                tabControl1.SelectedIndex = 2;

            }
            con.Close();
            reader.Close();
        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuCustomLabel24_Click(object sender, EventArgs e)
        {

        }

        private void roundedButton2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 4;
        }

        private void guna2Panel4_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void roundedButton5_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "SELECT * FROM users u inner join [account_details] a on u.user_id=a.user_id where u.user_id='" + Convert.ToInt32(user_id) + "' and a.card_no='" + Convert.ToInt64(txt_Cardno.Text) + "' and a.account_title='" + txt_cardname.Text + "'and a.pin='" + Convert.ToInt32(txt_cvv.Text) + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string emailaddress = Convert.ToString(reader["email"]);
                string pno = Convert.ToString(reader["phone_num"]); 

                Whatsapp w = new Whatsapp();
                Email em = new Email();
                otp_code = otp();
                em.sendemail(emailaddress,otp_code);
                w.send(pno,otp_code);
            }
            con.Close();
            reader.Close();

        }
        public bool VerifyOtp(string otp_code)
        {
            if (this.otp_code.Equals(otp_code))
            {
                verify = true;
            }
            return verify;
        }
        private void roundedButton1_Click(object sender, EventArgs e)
        {
            con.Open();
            string query= "Select * from tickets where seat_num='"+ confirmbtn.Text +"'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                MessageBox.Show("This seat is already booked");
            }
            else
            {
                MessageBox.Show("The seat has been confirmed");
            }
            con.Close();
            reader.Close();

            seat = bunifuTextBox1.Text;
            tabControl1.SelectedIndex = 3;
            lbl_fname_t.Text = txt_fname_p.Text;
            lbl_lname_t.Text = lbl_lname_p.Text;
            lbl_dob_t.Text = txt_dob_p.Text;
            lbl_nationality_t.Text = txt_nationality_p.Text;
            lbl_ppt_t.Text = txt_passportno_p.Text;
            lbl_exp_t.Text = txt_exp_p.Text;

            //txt_src_t.Text = user.srcfull;
            //lbl_src_tr.Text = user.destfull;
            //lbl_dob_t.Text = user.destfull;
            //lbl_dest_tr.Text = user.srcfull;
            //lbl_totfare.Text = user.totfare;
            lbl_seat_t.Text = seat;
            //lbl_tax.Text = ((int.Parse(user.totfare) *13)/100).ToString();  
            //lbl_grandtot.Text=(int.Parse(user.totfare) + int.Parse(lbl_tax.Text)).ToString();
        }
    }
}
