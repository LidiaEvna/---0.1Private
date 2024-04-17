using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flat_rental_agency_0._1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "rental_agency_for_flatsDataSet2.Квартира". При необходимости она может быть перемещена или удалена.
            this.квартираTableAdapter.Fill(this.rental_agency_for_flatsDataSet2.Квартира);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 frm = new Form4();
            frm.Show();
            Close();
        }
    }
}
