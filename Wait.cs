using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMSProject
{
    public partial class Wait : Form
    {

        public Wait()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            CenterToScreen();
          
        }

        private void Wait_Load(object sender, EventArgs e)
        {

        }
    }
}
