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
    public partial class FrmNotlar : Form
    {
        public FrmNotlar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void notlistele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_notlar", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            txtbaslik.Text = "";
            txthitap.Text = "";
            txtid.Text = "";
            txtolusturan.Text = "";
            msksaat.Text = "";
            msktarih.Text = "";
            rchdetay.Text = "";
        }
        private void FrmNotlar_Load(object sender, EventArgs e)
        {
            notlistele();
            temizle();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into tbl_notlar (Tarih,Saat,Baslik,Detay,Olusturan,Hitap) values (@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", msktarih.Text);
            komut.Parameters.AddWithValue("@p2", msksaat.Text);
            komut.Parameters.AddWithValue("@p3", txtbaslik.Text);
            komut.Parameters.AddWithValue("@p4", rchdetay.Text);
            komut.Parameters.AddWithValue("@p5", txtolusturan.Text);
            komut.Parameters.AddWithValue("@p6", txthitap.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Not Eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            notlistele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtbaslik.Text = dr["Baslik"].ToString();
                txthitap.Text = dr["Hitap"].ToString();
                txtid.Text = dr["ID"].ToString();
                txtolusturan.Text = dr["Olusturan"].ToString();
                msksaat.Text = dr["Saat"].ToString();
                msktarih.Text = dr["Tarih"].ToString();
                rchdetay.Text = dr["Detay"].ToString();
            }
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Not silinsin mi?", "Not Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("delete from tbl_notlar where Id=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtid.Text);
                komut.ExecuteNonQuery();
                notlistele();
                MessageBox.Show("Not Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                temizle();
            }
            else if (secim == DialogResult.No)
            {
                notlistele();
            }

        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update tbl_notlar set Tarih=@p1,Saat=@p2,Baslik=@p3,Detay=@p4,Olusturan=@p5,Hitap=@p6 where ID=@p7", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", msktarih.Text);
            komut.Parameters.AddWithValue("@p2", msksaat.Text);
            komut.Parameters.AddWithValue("@p3", txtbaslik.Text);
            komut.Parameters.AddWithValue("@p4", rchdetay.Text);
            komut.Parameters.AddWithValue("@p5", txtolusturan.Text);
            komut.Parameters.AddWithValue("@p6", txthitap.Text);
            komut.Parameters.AddWithValue("@p7", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            notlistele();
            MessageBox.Show("Not Güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            FrmNotDetay fr = new FrmNotDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                fr.not = dr["Detay"].ToString();
            }
                fr.Show();
        }
    }
}
