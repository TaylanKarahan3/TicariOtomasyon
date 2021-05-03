using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ticari_Otomasyon
{
    public partial class FrmRaporlar : Form
    {
        public FrmRaporlar()
        {
            InitializeComponent();
        }

        private void FrmRaporlar_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DboTicariOtomasyonDataSet3.Tbl_Personeller' table. You can move, or remove it, as needed.
            this.Tbl_PersonellerTableAdapter.Fill(this.DboTicariOtomasyonDataSet3.Tbl_Personeller);
            // TODO: This line of code loads data into the 'DboTicariOtomasyonDataSet2.Tbl_Giderler' table. You can move, or remove it, as needed.
            this.Tbl_GiderlerTableAdapter.Fill(this.DboTicariOtomasyonDataSet2.Tbl_Giderler);
            // TODO: This line of code loads data into the 'DboTicariOtomasyonDataSet1.Tbl_Musteriler' table. You can move, or remove it, as needed.
            this.Tbl_MusterilerTableAdapter.Fill(this.DboTicariOtomasyonDataSet1.Tbl_Musteriler);
            // TODO: This line of code loads data into the 'DboTicariOtomasyonDataSet.Tbl_Firmalar' table. You can move, or remove it, as needed.
            this.Tbl_FirmalarTableAdapter.Fill(this.DboTicariOtomasyonDataSet.Tbl_Firmalar);

            this.reportViewer1.RefreshReport();
            this.reportViewer2.RefreshReport();
            this.reportViewer4.RefreshReport();
            this.reportViewer5.RefreshReport();
        }
    }
}
