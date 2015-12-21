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
    public partial class Zurnalas : Form
    {
        private string organizatorius;

        public Zurnalas(string org)
        {
            organizatorius = org;            
            InitializeComponent();
            
        }

      
      
        bool loaded = false;

        private void Zurnalas_Load(object sender, EventArgs e)
        {
            organizatoriusLabel.Text = organizatorius;
            this.kodasTableAdapter.FillByOrganizatorius(this.viesiejiDataSet.Kodas, organizatorius);          

            // TODO
            // padaryti serverio pasirinkimo dialoga
            string connectionString = @"server=RIMAS-WIN7\SQL_RIMO; Trusted_Connection=yes; database=Viesieji2015;connection timeout=30;";
            // Properties.Settings.Default["ViesiejiConnectionString"] = connectionString;            
            this.zurnalasTableAdapter.Connection.ConnectionString = connectionString;
            this.prekeTableAdapter.Connection.ConnectionString = connectionString;
            this.kodasTableAdapter.Connection.ConnectionString = connectionString;

            this.zurnalasTableAdapter.ClearBeforeFill = true;
            this.prekeTableAdapter.ClearBeforeFill = true;
            // this.viesiejiDataSet.Zurnalas.Clear();
            this.zurnalasTableAdapter.FillByKodasID(this.viesiejiDataSet.Zurnalas, kodasTextBox.Text, organizatorius);
            loaded = true;

        }

          
        

        // Issaukiamas kai pereinama i kita virsutinio grido (Zurnalo) eilute. Reikalingas tam, kad atnaujintu apatinio grido (Preke) duomenis.
        private void zurnalasGrid_SelectionChanged(object sender, EventArgs e)
        {
            // Jei yra celes pazymejimas, tai vykdyti toliau (pvz., jei pagal koda nera ne vieno vykdyto pirkimo, tai cele nebuna pazymeta)
            if (zurnalasGrid.CurrentCell != null)
            {
                try
                {

                    // dabartines pazymetos eilutes 0 celes reiksme
                    string str = zurnalasGrid.Rows[zurnalasGrid.CurrentCell.RowIndex].Cells[0].Value.ToString();
                    // uzpildo apatini grida pagal virsutinio grido pazymetos eilutes duomenis (kitaip tariant, gride surisa lenteliu Zurnalas ir Preke duomenis)
                    // FillByZurnalasID SQL: SELECT prekeID, zurnalasID, preke, kiekis, matas, pvm, suma, data FROM dbo.Preke WHERE dbo.Preke.zurnalasID = @zurnalas_id
                    this.prekeTableAdapter.FillByZurnalasID(this.viesiejiDataSet.Preke, ((int)(System.Convert.ChangeType(str, typeof(int)))));


                }
                catch (System.Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                //int c=zurnalasGrid.Rows.Count;
               
            }

        }
        

        // Baigiant redaguoti bet kuria Zurnalo cele iraso pakeitimus i DB
        private void zurnalasGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            this.Validate(); // TODO
            zurnalasBindingSource.EndEdit();
            this.zurnalasTableAdapter.Update(this.viesiejiDataSet);
         
        }

           
        private void zurnalasGrid_Validating(object sender, CancelEventArgs e)
        {
            // TODO  
            //MessageBox.Show("zurnalasGrid_Validating");
        }

      

    

        private void kodasTextBox_TextChanged(object sender, EventArgs e)
        {

            try
            {
                this.zurnalasTableAdapter.FillByKodasID(this.viesiejiDataSet.Zurnalas, kodasTextBox.Text, organizatorius);

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

           

     
        
    }

       
}
