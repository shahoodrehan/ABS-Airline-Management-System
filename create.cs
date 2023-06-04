using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DBMSProject
{
    public partial class create : Form
    {
        int id;
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
        public create()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
      

        }
        SqlConnection conn = new SqlConnection(@"Data Source=IRFAN\RAZRIZ;Initial Catalog=AMS;Persist Security Info=True;User ID=admin;Password=Zxc121216");
        string img_loc;
        SqlCommand cmd;
        private void bunifuCustomLabel13_Click(object sender, EventArgs e)
        {

        }

        private void create_Load(object sender, EventArgs e)
        {
           
        }

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {

        }

      

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;

        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;

        }

        private void bunifuGradientPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_upload_Click(object sender, EventArgs e)
        {

        }

        

        private void bunifuThinButton21_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void bunifuThinButton22_Click_2(object sender, EventArgs e)
        {
            try
            {
                byte[] image = null;
                FileStream stream = new FileStream(img_loc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(stream);
                image = br.ReadBytes((int)stream.Length);
                conn.Open();
                string query1 = "INSERT INTO users Values('" + txt_dob.Text.ToString() + "','" + Convert.ToInt32(txt_age.Text) + "','" + txt_pno.Text + "','" + txt_fname.Text + "','" + txt_lname.Text + "','" + txt_sex.Text + "','" + txt_address.Text + "','" + txt_city.Text + "','" + Convert.ToInt32(txt_postal.Text) + "','" + txt_email.Text + "','" + txt_pass.Text + "',@image)";


                cmd = new SqlCommand(query1, conn);
                cmd.Parameters.Add(new SqlParameter("@image", image));
                cmd.ExecuteNonQuery();

                MessageBox.Show("successfully added");
                string query = "SELECT user_id FROM users WHERE email='" + txt_email.Text + "'";

                SqlCommand cmd1 = new SqlCommand(query, conn);
                SqlDataReader reader = cmd1.ExecuteReader();
                if (reader.Read())
                {
                    id = Convert.ToInt32(reader["user_id"]);
                }
                conn.Close();
            }
            catch (Exception ee)
            {

                MessageBox.Show(ee.Message);
            }
            tabControl1.SelectedIndex = 2;
        }

        private void guna2ImageButton1_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void guna2ImageButton2_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void btn_createacc_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query1 = "INSERT INTO account_details Values('" + id + "','" + Convert.ToInt64(txt_accno.Text) + "','" + Convert.ToInt64(txt_cardno.Text) + "','" + Convert.ToInt32(txt_cvv.Text) + "','" + Convert.ToDouble(txt_balance.Text) + "','" + txt_cardname.Text + "')";


                cmd = new SqlCommand(query1, conn);

                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("successfully added");
            }
            catch (Exception ee)
            {

                MessageBox.Show(ee.Message);
            }
        }

        private void guna2CirclePictureBox1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png files(*.png)|*.png|jpg file(*.jpg)|*.jpg";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                img_loc = dialog.FileName.ToString();
                guna2CirclePictureBox1.ImageLocation = img_loc;
            }
        }
    }
}
