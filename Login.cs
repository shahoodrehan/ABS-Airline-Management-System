using Bunifu.Core.forms;
using Bunifu.Framework.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace DBMSProject
{
    public partial class Login : Form
    {
        public static string admin, id;
        public static Boolean login_status = false;
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
        public Login()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }




        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            Forgot f = new Forgot();

            this.Hide();
            f.Show();

            Login l = new Login();
            l.Close();
        }

        private void bunifuThinButton23_Click_1(object sender, EventArgs e)
        {
            create c = new create();

            this.Hide();
            c.Show();

            Login l = new Login();
            l.Close();

        }


        private async void btn_login_Click_1(object sender, EventArgs e)
        {
            string emailaddress;
            Email email = new Email();
            SqlConnection con = new SqlConnection(@"Data Source=IRFAN\RAZRIZ;Initial Catalog=AMS;Persist Security Info=True;User ID=admin;Password=Zxc121216");
            con.Open();
            string query = "SELECT * FROM admininfo WHERE admin_uname='" + txtUsername.Text + "'AND admin_pass='" + txt_Password.Text + "'";
            string query1 = "SELECT * FROM userinfo WHERE email='" + txtUsername.Text + "'AND passcode='" + txt_Password.Text + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                login_status = true;
                //admin ds = new admin();
                emailaddress = Convert.ToString(reader["email_id"]);
                this.Hide();
                Login l = new Login();
                l.Close();
                //ds.Show();

                //email.send(emailaddress);
            }
            else
            {
                reader.Close();

                cmd = new SqlCommand(query1, con);
                reader = cmd.ExecuteReader();


                if (reader.Read())
                {
                    login_status = true;
                    admin = txtUsername.Text;
                    emailaddress = txtUsername.Text;
                    this.Hide();
                    Login l = new Login();
                    Wait w=new Wait();
                    l.Close();
                    w.Show(); 
                    await Task.Run(()=>email.send(emailaddress));
                    w.Hide();
                   
                    user user = new user();
                    user.Show();
                }

                else
                {
                    MessageBox.Show("Username And Password Not Match!", "ABS ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                txtUsername.Text = string.Empty;
                txt_Password.Text = string.Empty;
                reader.Close();

                con.Close();
            }

        }
    }
}
