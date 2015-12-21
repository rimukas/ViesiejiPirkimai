using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VP_žurnalas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
           
            Zurnalas ZurnalasForm = new Zurnalas(organizatoriusComboBox.Text);
            ZurnalasForm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'viesiejiDataSet.Organizatorius' table. You can move, or remove it, as needed.
            this.organizatoriusTableAdapter.Fill(this.viesiejiDataSet.Organizatorius);
            // TODO: This line of code loads data into the 'viesiejiDataSet.Organizatorius' table. You can move, or remove it, as needed.
            this.organizatoriusTableAdapter.Fill(this.viesiejiDataSet.Organizatorius);

        }

     
    }
}
