using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DBMSProject
{
    public partial class Forgot : Form
    {
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
        string  username,phone,emailaddress;
        public Forgot()
        {
            InitializeComponent();
            txt_email_f.Hide();
            txt_email_f.Enabled = false;
            btn_resend.Enabled = false;
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

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
            Invoke(new Action(() =>
            {
                s++;
                if (s == 60)
                {
                    t.Stop();
                    btn_resend.ForeColor = Color.Black;
                    btn_resend.Enabled = true;
                    btn_sendotp.Enabled = false;

                }
                lbl_time.Text = m.ToString().PadLeft(2, '0') + ":" + s.ToString().PadLeft(2, '0');

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

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            tabControl_f.SelectedIndex = 1;

        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            tabControl_f.SelectedIndex = 0;

        }

    

        private void bunifuThinButton28_Click(object sender, EventArgs e)
        {
            tabControl_f.SelectedIndex = 2;

        }


        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void bunifuThinButton24_Click_1(object sender, EventArgs e)
        {
            tabControl_f.SelectedIndex = 0;
        }

        private void btn_verify_Click_1(object sender, EventArgs e)
        {
            tabControl_f.SelectedIndex =4;
        }

        private void btn_sendotp_Click_1(object sender, EventArgs e)
        {
            tabControl_f.SelectedIndex = 3;
        }

        private void btn_select_Click_1(object sender, EventArgs e)
        {
            tabControl_f.SelectedIndex = 2;
        }

        private void btn_name_Click_1(object sender, EventArgs e)
        {
            tabControl_f.SelectedIndex = 1;
        }

        private void bunifuThinButton26_Click_1(object sender, EventArgs e)
        {
            tabControl_f.SelectedIndex = 1;

        }

        private void bunifuThinButton28_Click_1(object sender, EventArgs e)
        {
            tabControl_f.SelectedIndex = 2;

        }

        private void bunifuThinButton210_Click(object sender, EventArgs e)
        {
            tabControl_f.SelectedIndex = 3;

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuCustomLabel12_Click(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            username = txt_uname_f.Text;
            SqlConnection con = new SqlConnection(@"Data Source=IRFAN\RAZRIZ;Initial Catalog=AMS;Persist Security Info=True;User ID=admin;Password=Zxc121216");
            con.Open();
            string query = "SELECT * FROM admininfo WHERE admin_uname='" + txt_uname_f.Text + "'";
            string query1 = "SELECT * FROM userinfo WHERE email='" + txt_uname_f.Text + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                emailaddress = Convert.ToString(reader["email_id"]);
                string num = Convert.ToString(reader["admin_pno"]);
                phone = num.ToString().Substring(1);
                tabControl_f.SelectedIndex = 1;
            }
            else
            {
                reader.Close();

                cmd = new SqlCommand(query1, con);
                reader = cmd.ExecuteReader();


                if (reader.Read())
                {
                    emailaddress = Convert.ToString(reader["email"]);
                    string num = Convert.ToString(reader["admin_pno"]);
                    phone = num.ToString().Substring(1);
                    tabControl_f.SelectedIndex = 1;
                }

                else
                {
                    MessageBox.Show("Username And Password Not Match!", "VINSMOKE MJ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
                reader.Close();
                con.Close();
            }
         
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            Login l = new Login();

            this.Hide();
            l.Show();

            Forgot s = new Forgot();
            s.Close();
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            if (combo_forgot.SelectedIndex == -1)
            {
                MessageBox.Show("select a medium first");
            }
            else

            {

                if (combo_forgot.SelectedIndex == 0)
                {
                    radio_email.Text = emailaddress;
                    radioButton2.Text = "Enter another email";
                    tabControl_f.SelectedIndex = 2;


                }
                else if (combo_forgot.SelectedItem.ToString().Equals("WHATSAPP"))
                {
                    radio_email.Text = "+92" + phone;
                    radioButton2.Text = "Enter another number";
                    tabControl_f.SelectedIndex = 2;

                }

            }

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            tabControl_f.SelectedIndex = 0;
        }

        private void btn_sendotp_Click(object sender, EventArgs e)
        {
            Email email = new Email();
            Whatsapp watsapp = new Whatsapp();
            string otp_code = otp();
            if (radio_email.Checked == false && radioButton2.Checked == false)
            {
                MessageBox.Show("please select any option");

            }
            else
            {


                if (combo_forgot.SelectedItem.ToString().Equals("EMAIL"))
                {
                    if (radio_email.Checked == true)
                    {
                        txt_email_f.Enabled = false;
                        email.sendemail(emailaddress,otp_code);
                        tabControl_f.SelectedIndex = 3;
                    }
                    else if (radioButton2.Checked == true)
                    {
                        txt_email_f.Show();
                        txt_email_f.Enabled = true;
                        if (txt_email_f.Text == "")
                        {
                            MessageBox.Show("enter email address to get otp!");
                        }
                        else
                        {
                            email.sendemail(txt_email_f.Text, otp_code);
                            tabControl_f.SelectedIndex = 3;
                        }
                    }

                }
                else if (combo_forgot.SelectedItem.ToString() == "WHATSAPP")
                {
                    radio_email.Text = phone;
                    radioButton2.Text = "Enter another number";
                    if (radio_email.Checked == true)
                    {
                        txt_email_f.Enabled = false;
                        watsapp.send(otp_code, "+92" + phone);
                        tabControl_f.SelectedIndex = 3;
                    }
                    else if (radioButton2.Checked == true)
                    {
                        txt_email_f.Enabled = true;
                        if (txt_email_f.Text == "")
                        {
                            MessageBox.Show("enter email address to get otp!");
                        }

                        watsapp.send(otp_code, txt_email_f.Text);

                        tabControl_f.SelectedIndex = 3;

                    }
                }
                time();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            txt_email_f.Show();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            tabControl_f.SelectedIndex = 1;

        }

        private void btn_verify_Click(object sender, EventArgs e)
        {
          string  otp_code = otp();
            if (txt_otp.Text == "")
            {
                MessageBox.Show("please enter the otp u recieved");
            }
            if (txt_otp.Text.Equals(otp_code))
            {
                tabControl_f.SelectedIndex = 4;
            }
            else
            {
                MessageBox.Show("otp is not valid");
            }
        }

        private void btn_resend_Click(object sender, EventArgs e)
        {
            tabControl_f.SelectedIndex = 2;
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            if (txt_newpass.Text == "" || txt_confirmpass.Text == "")
            {
                MessageBox.Show("kindly enter your new password");
            }
            else
            {
             
                SqlConnection con = new SqlConnection(@"Data Source=IRFAN\RAZRIZ;Initial Catalog=AMS;Persist Security Info=True;User ID=admin;Password=Zxc121216");
                con.Open();
                string query = "SELECT * FROM admininfo WHERE admin_uname='" + txt_uname_f.Text + "'";
                string query1 = "SELECT * FROM userinfo WHERE email='" + txt_uname_f.Text + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string query2 = "UPDATE admin  SET admin_pass='" + txt_newpass.Text + "' WHERE admin_uname='" + username + "'";
                    cmd = new SqlCommand(query2, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("data is updated");
                    Login l = new Login();

                    this.Hide();
                    l.Show();

                    Forgot s = new Forgot();
                    s.Close();
                }
                else
                {
                    reader.Close();

                    cmd = new SqlCommand(query1, con);
                    reader = cmd.ExecuteReader();


                    if (reader.Read())
                    {
                        string query2 = "UPDATE user  SET passcode='" + txt_newpass.Text + "' WHERE admin_uname='" + username + "'";
                        cmd = new SqlCommand(query2, con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("data is updated");
                        Login l = new Login();

                        this.Hide();
                        l.Show();

                        Forgot s = new Forgot();
                        s.Close();
                    }

                    else
                    {
                        MessageBox.Show("Username And Password Not Match!", "VINSMOKE MJ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    reader.Close();
                    con.Close();
                }

            }
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            tabControl_f.SelectedIndex = 3;
        }

        private void bunifuThinButton24_Click_2(object sender, EventArgs e)
        {
            tabControl_f.SelectedIndex = 2;
        }

        private void radio_email_CheckedChanged_1(object sender, EventArgs e)
        {
            txt_email_f.Hide();
        }


       
    }
}
