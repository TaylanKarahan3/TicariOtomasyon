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
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_urunler", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            txtad.Text = "";
            txtalisfiyat.Text = "";
            txtid.Text = "";
            txtmarka.Text = "";
            txtmodel.Text = "";
            txtsatisfiyat.Text = "";
            mskyil.Text = "";
            rchdetay.Text = "";
            nudadet.Value = 0;
        }
        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            //verileri kaydetme 
            SqlCommand komut = new SqlCommand("insert into tbl_urunler(UrunAd,Marka,Model,Yil,Adet,AlisFiyat,SatisFiyat,Detay) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtmarka.Text);
            komut.Parameters.AddWithValue("@p3", txtmodel.Text);
            komut.Parameters.AddWithValue("@p4", mskyil.Text);
            komut.Parameters.AddWithValue("@p5", int.Parse(nudadet.Value.ToString()));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtalisfiyat.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtsatisfiyat.Text));
            komut.Parameters.AddWithValue("@p8", rchdetay.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Ürün Kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bgl.baglanti().Close();
            listele();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //verileri silme
            SqlCommand komut = new SqlCommand("delete from tbl_urunler where ID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //verileri taşıma
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtid.Text = dr["ID"].ToString();
            txtad.Text = dr["UrunAd"].ToString();
            txtmarka.Text = dr["Marka"].ToString();
            txtmodel.Text = dr["Model"].ToString();
            mskyil.Text = dr["Yil"].ToString();
            nudadet.Value = int.Parse(dr["Adet"].ToString());
            txtalisfiyat.Text = dr["AlisFiyat"].ToString();
            txtsatisfiyat.Text= dr["SatisFiyat"].ToString();
            rchdetay.Text = dr["Detay"].ToString();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            //verileri güncelleme
            SqlCommand komut = new SqlCommand("update tbl_urunler set UrunAd=@p1,Marka=@p2,Model=@p3,Yil=@p4,Adet=@p5,AlisFiyat=@p6,SatisFiyat=@p7,Detay=@p8 where ID=@p9", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtmarka.Text);
            komut.Parameters.AddWithValue("@p3", txtmodel.Text);
            komut.Parameters.AddWithValue("@p4", mskyil.Text);
            komut.Parameters.AddWithValue("@p5", int.Parse(nudadet.Value.ToString()));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(txtalisfiyat.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(txtsatisfiyat.Text));
            komut.Parameters.AddWithValue("@p8", rchdetay.Text);
            komut.Parameters.AddWithValue("@p9", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Ürün Güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
