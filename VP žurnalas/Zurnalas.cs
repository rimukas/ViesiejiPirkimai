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
        
        public Zurnalas()
        {
            
            InitializeComponent();
        }

      
      
        bool loaded = false;

        private void Zurnalas_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'viesiejiDataSet1.Preke' table. You can move, or remove it, as needed.
           // this.prekeTableAdapter.Fill(this.viesiejiDataSet1.Preke);
            // 2\015.00.00


            // TODO
            // padaryti serverio pasirinkimo dialoga
            string connectionString = @"server=SERVER\SQLEXPRESS; Trusted_Connection=yes; database=Viesieji2015;connection timeout=30;";
            // Properties.Settings.Default["ViesiejiConnectionString"] = connectionString;
            this.zurnalasTableAdapter.Connection.ConnectionString = connectionString;
            this.zurnalasTableAdapter.ClearBeforeFill = true;
            // this.viesiejiDataSet.Zurnalas.Clear();
            this.zurnalasTableAdapter.Fill(this.viesiejiDataSet.Zurnalas);
            loaded = true;

        }

          
        

        // Issaukiamas kai pereinama i kita virsutinio grido (Zurnalo) eilute. Reikalingas tam, kad atnaujintu apatinio grido (Preke) duomenis.
        private void zurnalasGrid_SelectionChanged(object sender, EventArgs e)
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

        
    }
}
