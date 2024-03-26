using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace EntitFrameworkNorthwind
{
    public partial class Form1 : Form  
    {
        public Form1()
        {
            InitializeComponent();
        }

        NORTHWNDEntities nwe = new NORTHWNDEntities();
        private void button1_Click(object sender, EventArgs e)
        {

            
            var result = (from p in nwe.Products
                          join s in nwe.Suppliers on p.SupplierID equals s.SupplierID
                          group new { p, s.ContactName } by p.SupplierID into g
                          select new
                          {
                              Supplier = g.FirstOrDefault().ContactName,
                              Total = g.Sum(x => x.p.UnitsInStock)

                          }).OrderBy(o => o.Total);
            
            dataGridView1.DataSource = result.ToList();





            //TOP
            var result2 = (from ord in nwe.Orders.Take(10)
                          select new
                          {
                              ord.Freight
                          }).OrderByDescending(o=>o.Freight);


            dataGridView2.DataSource = result2.ToList();



            //Count
            var result3 = from c in nwe.Customers
                          group new { c.Country } by c.Country into g
                          select new
                          {
                              Ulke = g.Key,
                              Adet = g.Count()
                          };

            dataGridView3.DataSource = result3.ToList();





            //Count + Max + join
            var result32 = from c in nwe.Customers
                           join o in nwe.Orders on c.CustomerID equals o.CustomerID
                          group new { c.Country, o.ShipVia } by c.Country into g
                          select new
                          {
                              Ulke = g.Key,
                              Adet = g.Count(),
                              Maks = g.Max(o=>o.ShipVia)
                          };

            dataGridView4.DataSource = result32.ToList();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            int min = Convert.ToInt32(textBox1.Text);
            int max = Convert.ToInt32(textBox3.Text);
            dataGridView5.DataSource = nwe.sp_priceRange(min, max).ToList();
        }
 
        private void button3_Click(object sender, EventArgs e)
        {
          label3.Text = nwe.Database.SqlQuery<int>(@"select  [dbo].[fn_TLKURU]({0})",textBox4.Text).FirstOrDefault().ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {  
            dataGridView6.DataSource = nwe.fn_shippingTerm(dateTimePicker1.Value, dateTimePicker2.Value).ToList();

        }
    }
}
