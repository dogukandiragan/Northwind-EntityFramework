using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntitFrameworkNorthwind
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        NORTHWNDEntities nwe = new NORTHWNDEntities();
        private void simpleButton1_Click(object sender, EventArgs e)
        {
      
            gridControl1.DataSource = nwe.Customers.ToList();

  
        }
    }
}
