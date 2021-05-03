using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ticari_Otomasyon
{
    public partial class FrmRehber : Form
    {
        public FrmRehber()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmRehber_Load(object sender, EventArgs e)
        {
            //müşteri bilgilerl
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Ad,Soyad,Telefon,Telefon2,Mail from tbl_musteriler", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

            //firma bilgileri
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select Ad,YetkiliAdSoyad,Telefon1,Telefon2,Telefon3,Mail,Faks from tbl_firmalar ", bgl.baglanti());
            da2.Fill(dt2);
            gridControl2.DataSource = dt2;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmMail fr = new FrmMail();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                fr.mail = dr["Mail"].ToString();
            }
                fr.Show();
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            FrmMail fr = new FrmMail();
            DataRow dr = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (dr != null)
            {
                fr.mail = dr["Mail"].ToString();
            }
            fr.Show();
        }
    }
}
