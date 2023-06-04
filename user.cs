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
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DBMSProject
{
    public partial class user : Form

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
        public user()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

        }
        SqlConnection con = new SqlConnection(@"Data Source=IRFAN\RAZRIZ;Initial Catalog=AMS;Persist Security Info=True;User ID=admin;Password=Zxc121216");
        int tot_flight;
        public static string dest, src, destfull, srcfull, totfare, fare;
        public double tot;

        private void bunifuThinButton23_Click(object sender, EventArgs e)

        {

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {

        }

        private void user_Load(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;

        }

        private void bunifuThinButton22_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        private void txt_source_TextChanged(object sender, EventArgs e)
        {
            LoadData();

        }

        private void txt_dest_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void LoadData()
        {



        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void txt_source_TextChanged_1(object sender, EventArgs e)
        {
            //try
            //{
            //    string query = "SELECT distinct src FROM flights WHERE src LIKE '" + txt_source.Text + "%'";

            //    con.Open();
            //    SqlCommand command = new SqlCommand(query, con);

            //    SqlDataReader reader = command.ExecuteReader();

            //    List<string> suggestions = new List<string>();
            //    while (reader.Read())
            //    {
            //        suggestions.Add(Convert.ToString(reader["src"]));
            //    }

            //    Bind suggestions to text field
            //    txt_source.AutoCompleteMode = AutoCompleteMode.Suggest;
            //    txt_source.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //    txt_source.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            //    txt_source.AutoCompleteCustomSource.AddRange(suggestions.ToArray());

            //    con.Close();
            //    reader.Close();

            //}
            //catch (Exception ee)
            //{

            //    MessageBox.Show(ee.Message.ToString());
            //}


        }

        private void roundtrip_CheckedChanged(object sender, EventArgs e)
        {
            ret_time.Show();
            label7.Show();
            label13.Show();
            Flights f = new Flights();
            f.guna2Panel8.Show();
            f.panel4.Hide();
        }

        private void onewaytrip_CheckedChanged(object sender, EventArgs e)
        {
            label7.Hide();
            ret_time.Hide();
            label13.Hide();
            Flights f = new Flights();
            f.guna2Panel8.Hide();
            f.panel4.Hide();
        }

        private void btn_searchflight_Click(object sender, EventArgs e)
        {
            double p = 0, far = 0.0;

            if (txt_dest.Text.Equals("") || txt_source.Text.Equals("") || passengerclass.Text.Equals("") || noofpassenger.Text.Equals(""))
            {
                MessageBox.Show("field cannot be left empty");
            }
            if (roundtrip.Checked == false && onewaytrip.Checked == false)
            {
                MessageBox.Show("please select your trip first");

            }
            else
            {
                try
                {
                    Flights f = new Flights();

                    con.Open();
                    string query = "SELECT * FROM flights WHERE src='" + txt_source.Text + "'AND dest='" + txt_dest.Text + "'AND flight_date='" + dep_time.Text.ToString() + "'";
                    string query1 = "SELECT * FROM flights WHERE src='" + txt_source.Text + "'AND dest='" + txt_dest.Text + "'AND flight_date='" + ret_time.Text.ToString() + "'";
                    string query2 = "select * from class c inner join flights f on c.flight_num=f.flight_num where c.class_type='" + passengerclass.SelectedItem.ToString() + "' and f.src='" + txt_source.Text + "' and f.dest='" + txt_dest.Text + "'and f.flight_date='" + dep_time.Text.ToString() + "'";

                    SqlCommand cmd = new SqlCommand(query2, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {

                        fare = Convert.ToString(reader["fare"]);
                        p = Convert.ToDouble(noofpassenger.Text);
                        far = Convert.ToDouble(fare);
                        tot = far * p;
                        totfare = Convert.ToString(tot);
                        f.lbl_totfare.Text ="PKR "+ totfare;
                        f.lbl_tot.Text = totfare;
                        double tax = ((tot * 13) / 100);
                        f.lbl_tax.Text = tax.ToString();
                        f.lbl_grandtot.Text=(tot+tax).ToString();
                    }
         
                    reader.Close();
                    cmd = new SqlCommand(query, con);
                    reader = cmd.ExecuteReader();




                    while (reader.Read())
                    {
                        if (Convert.ToString(reader["src"]).Equals("Karachi") )
                        {
                            src = "KHI";

                        }
                        else if (Convert.ToString(reader["src"]).Equals("lahore") )
                        {
                            src = "LHR";
                        }
                        else if (Convert.ToString(reader["src"]).Equals("Islamabad") )
                        {
                            src = "ISB";
                        }
                        else if (Convert.ToString(reader["src"]).Equals("Multan") )

                        {
                            
                            src = "MUX";
                        }
                        else if (Convert.ToString(reader["src"]).Equals("Peshawar") )
                        {
                            src = "PEW";
                        }
                        else if (Convert.ToString(reader["src"]).Equals("Rawalpindi") )
                        {
                            src = "RWP";
                        }
                        else if (Convert.ToString(reader["src"]).Equals("Sialkot") )
                        {
                            src = "SKT";

                        }
                        if (Convert.ToString(reader["dest"]).Equals("Karachi"))
                        {
                            dest = "KHI";

                        }
                        else if (Convert.ToString(reader["dest"]).Equals("lahore"))
                        {
                            dest = "LHR";
                        }
                        else if (Convert.ToString(reader["dest"]).Equals("Islamabad"))
                        {
                            dest = "ISB";
                        }
                        else if (Convert.ToString(reader["dest"]).Equals("Multan"))

                        {

                            dest = "MUX";
                        }
                        else if (Convert.ToString(reader["dest"]).Equals("Peshawar"))
                        {
                            dest = "PEW";
                        }
                        else if (Convert.ToString(reader["dest"]).Equals("Rawalpindi"))
                        {
                            dest = "RWP";
                        }
                        else if (Convert.ToString(reader["dest"]).Equals("Sialkot"))
                        {
                            dest = "SKT";

                        }
                        ucFlightDetails uf = new ucFlightDetails();
                        f.showflight.Controls.Add(uf);
                        uf.price_e.Text = totfare;
                        dep_time.CustomFormat = "MMMM dd,yyyy";
                        f.current.Text = dep_time.Text.ToString();
                        f.current1.Text = dep_time.Value.AddDays(1).ToString().Substring(0, 10);
                        f.current2.Text = dep_time.Value.AddDays(2).ToString().Substring(0, 10);
                        f.current3.Text = dep_time.Value.AddDays(3).ToString().Substring(0, 10);
                        f.current4.Text = dep_time.Value.AddDays(-1).ToString().Substring(0, 10);
                        f.current5.Text = dep_time.Value.AddDays(-2).ToString().Substring(0, 10);
                        f.current6.Text = dep_time.Value.AddDays(-3).ToString().Substring(0, 10);
                        f.label17.Text = "Total fare . " + noofpassenger.Text + " Passenger";
                        f.lbl_src.Text = Convert.ToString(reader["src"]);
                        f.lbl_dest.Text = Convert.ToString(reader["dest"]);
                        f.lbl_fno.Text = Convert.ToString(reader["flight_num"]);
                        f.lbl_Air_name.Text = Convert.ToString(reader["flight_name"]);
                        f.lbl_fldate.Text = dep_time.Text.ToString();
                        f.lbl_fname_t.Text = ret_time.Text.ToString();
                        f.lbl_arr_t.Text = Convert.ToString(reader["arr_time"]).Substring(0, 5);
                        f.lbl_deptime_t.Text = Convert.ToString(reader["dep_time"]).Substring(0, 5); ;
                        f.txt_src_t.Text= Convert.ToString(reader["src"]);
                        f.txt_dest_t.Text = Convert.ToString(reader["dest"]);

                        uf.lbl_src.Text =  src;
                        uf.lbl_dest.Text = dest;
                        uf.lbl_srcfull.Text = Convert.ToString(reader["src"]);
                        uf.lbl_destfull.Text = Convert.ToString(reader["dest"]);
                        uf.label7.Text = passengerclass.Text.ToString();
                       
                        uf.lbl_arr.Text = Convert.ToString(reader["arr_time"]).Substring(0, 5);
                        uf.lbl_dep.Text = Convert.ToString(reader["dep_time"]).Substring(0, 5);
                        uf.lbl_date.Text = Convert.ToString(reader["flight_date"]);
                      
                        uf.lbl_destfull.Text = Convert.ToString(reader["dest"]);
                        uf.lbl_flights.Text = Convert.ToString(reader["flight_num"]);
                        uf.lbl_flightname.Text = Convert.ToString(reader["flight_name"]);
                        

                    }
                    reader.Close();

                    cmd = new SqlCommand(query1, con);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (Convert.ToString(reader["src"]).Equals("Karachi"))
                        {
                            src = "KHI";

                        }
                        else if (Convert.ToString(reader["src"]).Equals("lahore"))
                        {
                            src = "LHR";
                        }
                        else if (Convert.ToString(reader["src"]).Equals("Islamabad"))
                        {
                            src = "ISB";
                        }
                        else if (Convert.ToString(reader["src"]).Equals("Multan"))

                        {

                            src = "MUX";
                        }
                        else if (Convert.ToString(reader["src"]).Equals("Peshawar"))
                        {
                            src = "PEW";
                        }
                        else if (Convert.ToString(reader["src"]).Equals("Rawalpindi"))
                        {
                            src = "RWP";
                        }
                        else if (Convert.ToString(reader["src"]).Equals("Sialkot"))
                        {
                            src = "SKT";

                        }
                        if (Convert.ToString(reader["dest"]).Equals("Karachi"))
                        {
                            dest = "KHI";

                        }
                        else if (Convert.ToString(reader["dest"]).Equals("lahore"))
                        {
                            dest = "LHR";
                        }
                        else if (Convert.ToString(reader["dest"]).Equals("Islamabad"))
                        {
                            dest = "ISB";
                        }
                        else if (Convert.ToString(reader["dest"]).Equals("Multan"))

                        {

                            dest = "MUX";
                        }
                        else if (Convert.ToString(reader["dest"]).Equals("Peshawar"))
                        {
                            dest = "PEW";
                        }
                        else if (Convert.ToString(reader["dest"]).Equals("Rawalpindi"))
                        {
                            dest = "RWP";
                        }
                        else if (Convert.ToString(reader["dest"]).Equals("Sialkot"))
                        {
                            dest = "SKT";

                        }
                        ucFlightDetails uf = new ucFlightDetails();
                        f.showflight.Controls.Add(uf);
                        uf.price_e.Text = totfare;
                        dep_time.CustomFormat = "MMMM dd,yyyy";
                        f.current.Text = dep_time.Text.ToString();
                        f.current1.Text = dep_time.Value.AddDays(1).ToString().Substring(0, 10);
                        f.current2.Text = dep_time.Value.AddDays(2).ToString().Substring(0, 10);
                        f.current3.Text = dep_time.Value.AddDays(3).ToString().Substring(0, 10);
                        f.current4.Text = dep_time.Value.AddDays(-1).ToString().Substring(0, 10);
                        f.current5.Text = dep_time.Value.AddDays(-2).ToString().Substring(0, 10);
                        f.current6.Text = dep_time.Value.AddDays(-3).ToString().Substring(0, 10);
                        f.label17.Text = "Tota  l fare . " + noofpassenger.Text + " Passenger";
                        f.lbl_src.Text = Convert.ToString(reader["src"]);
                        f.lbl_dest.Text = Convert.ToString(reader["dest"]);
                        f.lbl_fno.Text = Convert.ToString(reader["flight_num"]);
                        f.lbl_Air_name.Text = Convert.ToString(reader["flight_name"]);
                        f.lbl_fldate.Text = dep_time.Text.ToString();
                        f.lbl_fname_t.Text = ret_time.Text.ToString();
                        f.lbl_arr_tr.Text = Convert.ToString(reader["arr_time"]).Substring(0, 5);
                        f.lbl_dep_tr.Text = Convert.ToString(reader["dep_time"]).Substring(0, 5); ;
                        f.lbl_src_tr.Text = Convert.ToString(reader["src"]); ;
                        f.lbl_dest_tr.Text = Convert.ToString(reader["dest"]);
                     
                        uf.lbl_src.Text = src;
                        uf.lbl_dest.Text = dest;
                        uf.lbl_srcfull.Text = Convert.ToString(reader["src"]);
                        uf.lbl_destfull.Text = Convert.ToString(reader["dest"]);
                        uf.label7.Text = passengerclass.Text.ToString();

                        uf.lbl_arr.Text = Convert.ToString(reader["arr_time"]).Substring(0, 5);
                        uf.lbl_dep.Text = Convert.ToString(reader["dep_time"]).Substring(0, 5);
                        uf.lbl_date.Text = Convert.ToString(reader["flight_date"]);

                        uf.lbl_destfull.Text = Convert.ToString(reader["dest"]);
                        uf.lbl_flights.Text = Convert.ToString(reader["flight_num"]);
                        uf.lbl_flightname.Text = Convert.ToString(reader["flight_name"]);


                    }
                    this.Hide();
                    f.Show();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.ToString(), "ABS ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }
    }
}
