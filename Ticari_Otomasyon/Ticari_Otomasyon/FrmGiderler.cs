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
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void giderlistesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_giderler order by ID asc", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            cmbay.Text = "";
            cmbyil.Text = "";
            txtdogalgaz.Text = "";
            txtekstra.Text = "";
            txtelektrik.Text = "";
            txtid.Text = "";
            txtinternet.Text = "";
            txtmaaslar.Text = "";
            txtsu.Text = "";
            rchnotlar.Text = "";
        }
        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            giderlistesi();
            temizle();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into tbl_giderler (Ay,Yil,Elektrik,Su,DogalGaz,Internet,Maaslar,Ekstra,Notlar) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbay.Text);
            komut.Parameters.AddWithValue("@p2", cmbyil.Text);
            komut.Parameters.AddWithValue("@p3", Convert.ToDecimal(txtelektrik.Text));
            komut.Parameters.AddWithValue("@p4", Convert.ToDecimal(txtsu.Text));
            komut.Parameters.AddWithValue("@p5", Convert.ToDecimal(txtdogalgaz.Text));
            komut.Parameters.AddWithValue("@p6", Convert.ToDecimal(txtinternet.Text));
            komut.Parameters.AddWithValue("@p7", Convert.ToDecimal(txtmaaslar.Text));
            komut.Parameters.AddWithValue("@p8", Convert.ToDecimal(txtekstra.Text));
            komut.Parameters.AddWithValue("@p9", rchnotlar.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Gider Listeye Eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            giderlistesi();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["ID"].ToString();
                cmbay.Text = dr["Ay"].ToString();
                cmbyil.Text = dr["Yil"].ToString();
                txtelektrik.Text = dr["Elektrik"].ToString();
                txtsu.Text = dr["Su"].ToString();
                txtdogalgaz.Text = dr["DogalGaz"].ToString();
                txtinternet.Text = dr["Internet"].ToString();
                txtmaaslar.Text = dr["Maaslar"].ToString();
                txtekstra.Text = dr["Ekstra"].ToString();
                rchnotlar.Text = dr["Notlar"].ToString();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Gider listeden silinsin mi?", "Gider Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("delete from tbl_giderler where ID=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Gider Listeden Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                giderlistesi();
                temizle();
            }
            else if (secim == DialogResult.No)
            {
                giderlistesi();
            }

        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand ("update tbl_giderler set Ay=@p1,Yil=@p2,Elektrik=@p3,Su=@p4,DogalGaz=@p5,Internet=@p6,Maaslar=@p7,Ekstra=@p8,Notlar=@p9 where Id=@p10",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbay.Text);
            komut.Parameters.AddWithValue("@p2", cmbyil.Text);
            komut.Parameters.AddWithValue("@p3", Convert.ToDecimal(txtelektrik.Text));
            komut.Parameters.AddWithValue("@p4", Convert.ToDecimal(txtsu.Text));
            komut.Parameters.AddWithValue("@p5", Convert.ToDecimal(txtdogalgaz.Text));
            komut.Parameters.AddWithValue("@p6", Convert.ToDecimal(txtinternet.Text));
            komut.Parameters.AddWithValue("@p7", Convert.ToDecimal(txtmaaslar.Text));
            komut.Parameters.AddWithValue("@p8", Convert.ToDecimal(txtekstra.Text));
            komut.Parameters.AddWithValue("@p9", rchnotlar.Text);
            komut.Parameters.AddWithValue("@p10", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Gider Listesi Güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            giderlistesi();
            temizle();
        }
    }
}
