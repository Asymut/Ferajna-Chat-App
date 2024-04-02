using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ferajna
{
    public partial class Form2 : Form
    {
        public string loginName {set;get;}

        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            label2.Text = loginName;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Logowanie form1 = new Logowanie(); 
            form1.Show(); 
            this.Close();
        }
    }
}
