using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tanks
{
    public partial class AboutObjectsForm : Form
    {
        public AboutObjectsForm()
        {
            InitializeComponent();
        }

        private void AboutObjectsForm_Load(object sender, EventArgs e)
        {

        }

        private void AboutObjectsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
