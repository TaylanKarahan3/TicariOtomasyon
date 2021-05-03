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
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void faturalistesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_faturabilgi", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void temizle()
        {
            txtalici.Text = "";
            txtid.Text = "";
            txtseri.Text = "";
            txtsirano.Text = "";
            txtteslimalan.Text = "";
            txtteslimeden.Text = "";
            txtvergid.Text = "";
            msksaat.Text = "";
            msktarih.Text = "";
        }
        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            faturalistesi();
            temizle();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            if (txtfaturaid.Text == "")
            {
                SqlCommand komut = new SqlCommand("insert into tbl_faturabilgi (Seri,SiraNo,Tarih,Saat,VergiDaire,Alici,TeslimEden,TeslimAlan) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtseri.Text);
                komut.Parameters.AddWithValue("@p2", txtsirano.Text);
                komut.Parameters.AddWithValue("@p3", msktarih.Text);
                komut.Parameters.AddWithValue("@p4", msksaat.Text);
                komut.Parameters.AddWithValue("@p5", txtvergid.Text);
                komut.Parameters.AddWithValue("@p6", txtalici.Text);
                komut.Parameters.AddWithValue("@p7", txtteslimeden.Text);
                komut.Parameters.AddWithValue("@p8", txtteslimalan.Text);
                komut.ExecuteNonQuery();
                faturalistesi();
                MessageBox.Show("Fatura Bilgisi Kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
            }
            //firma carisi
            if (txtfaturaid.Text != "" && comboBox1.Text == "Firma")
            {
                double miktar, tutar, fiyat;
                fiyat = Convert.ToDouble(txtfiyat.Text);
                miktar = Convert.ToDouble(txtmiktar.Text);
                tutar = miktar * fiyat;
                txttutar.Text = tutar.ToString();
                SqlCommand komut2 = new SqlCommand("insert into tbl_faturadetay (Urun,Miktar,Fiyat,Tutar,FaturaID) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@p1", txturun.Text);
                komut2.Parameters.AddWithValue("@p2", txtmiktar.Text);
                komut2.Parameters.AddWithValue("@p3", decimal.Parse(txtfiyat.Text));
                komut2.Parameters.AddWithValue("@p4", decimal.Parse(txttutar.Text));
                komut2.Parameters.AddWithValue("@p5", txtfaturaid.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
                //hareket tablosuna veri girişi
                SqlCommand komut3 = new SqlCommand("insert into tbl_firmahareketler (UrunID,Adet,Personel,Firma,Fiyat,Toplam,FaturaID,Tarih) values (@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)", bgl.baglanti());
                komut3.Parameters.AddWithValue("@h1", txturunid.Text);
                komut3.Parameters.AddWithValue("@h2", txtmiktar.Text);
                komut3.Parameters.AddWithValue("@h3", txtpersonel.Text);
                komut3.Parameters.AddWithValue("@h4", txtfirma.Text);
                komut3.Parameters.AddWithValue("@h5", decimal.Parse(txtfiyat.Text));
                komut3.Parameters.AddWithValue("@h6", decimal.Parse(txttutar.Text));
                komut3.Parameters.AddWithValue("@h7", txtfaturaid.Text);
                komut3.Parameters.AddWithValue("@h8", msktarih.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();

                //stok sayısını azaltma
                SqlCommand komut4 = new SqlCommand("update tbl_urunler set Adet=adet-@s1 where ID=@s2", bgl.baglanti());
                komut4.Parameters.AddWithValue("@s1", txtmiktar.Text);
                komut4.Parameters.AddWithValue("@s2", txturunid.Text);
                komut4.ExecuteNonQuery();
                bgl.baglanti().Close();
                faturalistesi();
                MessageBox.Show("Fatura Detayı Kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
            }

            //müşteri carisi
            if (txtfaturaid.Text != "" && comboBox1.Text == "Müşteri")
            {
                double miktar, tutar, fiyat;
                fiyat = Convert.ToDouble(txtfiyat.Text);
                miktar = Convert.ToDouble(txtmiktar.Text);
                tutar = miktar * fiyat;
                txttutar.Text = tutar.ToString();
                SqlCommand komut2 = new SqlCommand("insert into tbl_faturadetay (Urun,Miktar,Fiyat,Tutar,FaturaID) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@p1", txturun.Text);
                komut2.Parameters.AddWithValue("@p2", txtmiktar.Text);
                komut2.Parameters.AddWithValue("@p3", decimal.Parse(txtfiyat.Text));
                komut2.Parameters.AddWithValue("@p4", decimal.Parse(txttutar.Text));
                komut2.Parameters.AddWithValue("@p5", txtfaturaid.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
                //hareket tablosuna veri girişi
                SqlCommand komut3 = new SqlCommand("insert into tbl_musterihareketler (UrunID,Adet,Personel,Musteri,Fiyat,Toplam,FaturaID,Tarih) values (@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)", bgl.baglanti());
                komut3.Parameters.AddWithValue("@h1", txturunid.Text);
                komut3.Parameters.AddWithValue("@h2", txtmiktar.Text);
                komut3.Parameters.AddWithValue("@h3", txtpersonel.Text);
                komut3.Parameters.AddWithValue("@h4", txtfirma.Text);
                komut3.Parameters.AddWithValue("@h5", decimal.Parse(txtfiyat.Text));
                komut3.Parameters.AddWithValue("@h6", decimal.Parse(txttutar.Text));
                komut3.Parameters.AddWithValue("@h7", txtfaturaid.Text);
                komut3.Parameters.AddWithValue("@h8", msktarih.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();

                //stok sayısını azaltma
                SqlCommand komut4 = new SqlCommand("update tbl_urunler set Adet=adet-@s1 where ID=@s2", bgl.baglanti());
                komut4.Parameters.AddWithValue("@s1", txtmiktar.Text);
                komut4.Parameters.AddWithValue("@s2", txturunid.Text);
                komut4.ExecuteNonQuery();
                bgl.baglanti().Close();
                faturalistesi();
                MessageBox.Show("Fatura Detayı Kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["FaturaBilgiID"].ToString();
                txtseri.Text = dr["Seri"].ToString();
                txtsirano.Text = dr["SiraNo"].ToString();
                msktarih.Text = dr["Tarih"].ToString();
                msksaat.Text = dr["Saat"].ToString();
                txtvergid.Text = dr["VergiDaire"].ToString();
                txtalici.Text = dr["Alici"].ToString();
                txtteslimeden.Text = dr["TeslimEden"].ToString();
                txtteslimalan.Text = dr["TeslimAlan"].ToString();
            }
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Fatura bilgileri silinsin mi?", "Fatura Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("delete from tbl_faturabilgi where FaturaBilgiID=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura Bilgileri Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                faturalistesi();
                temizle();
            }
            else if (secim == DialogResult.No)
            {
                faturalistesi();
            }
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update tbl_faturabilgi set Seri=@p1,SiraNo=@p2,Tarih=@p3,Saat=@p4,VergiDaire=@p5,Alici=@p6,TeslimEden=@p7,TeslimAlan=@p8 where FaturaBilgiID=@p9", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtseri.Text);
            komut.Parameters.AddWithValue("@p2", txtsirano.Text);
            komut.Parameters.AddWithValue("@p3", msktarih.Text);
            komut.Parameters.AddWithValue("@p4", msksaat.Text);
            komut.Parameters.AddWithValue("@p5", txtvergid.Text);
            komut.Parameters.AddWithValue("@p6", txtalici.Text);
            komut.Parameters.AddWithValue("@p7", txtteslimeden.Text);
            komut.Parameters.AddWithValue("@p8", txtteslimalan.Text);
            komut.Parameters.AddWithValue("@p9", txtid.Text);
            komut.ExecuteNonQuery();
            faturalistesi();
            MessageBox.Show("Fatura Bilgisi Güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunler fr = new FrmFaturaUrunler();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                fr.id = dr["FaturaBilgiID"].ToString();
            }
            fr.Show();
        }

        private void btnbul_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select UrunAd,SatisFiyat from tbl_urunler where ID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txturunid.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txturun.Text = dr[0].ToString();
                txtfiyat.Text = dr[1].ToString();
            }
            bgl.baglanti().Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
