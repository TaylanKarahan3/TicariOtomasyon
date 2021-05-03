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
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void firmalistesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_firmalar", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            txtid.Text = "";
            txtad.Text = "";
            txtyetkili.Text = "";
            txtygorev.Text = "";
            txtkod1.Text = "";
            txtkod2.Text = "";
            txtkod3.Text = "";
            txtmail.Text = "";
            txtsektor.Text = "";
            txtvergid.Text = "";
            mskfaks.Text = "";
            msktc.Text = "";
            msktel1.Text = "";
            msktel2.Text = "";
            msktel3.Text = "";
            rchadres.Text = "";
            txtad.Focus();
        }

        void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("select Sehir from tbl_Iller", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbil.Properties.Items.Add(dr[0]);
                bgl.baglanti().Close();
            }
        }
        void carikodaciklamalar()
        {
            SqlCommand komut = new SqlCommand("select FirmaKod1 from tbl_kodlar", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                rchkod1.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }
        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            firmalistesi();
            temizle();
            sehirlistesi();
            carikodaciklamalar();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["ID"].ToString();
                txtad.Text = dr["Ad"].ToString();
                txtygorev.Text = dr["YetkiliStatu"].ToString();
                txtyetkili.Text = dr["YetkiliAdSoyad"].ToString();
                msktc.Text = dr["YetkiliTc"].ToString();
                txtsektor.Text = dr["Sektor"].ToString();
                msktel1.Text = dr["Telefon1"].ToString();
                msktel2.Text = dr["Telefon2"].ToString();
                msktel3.Text = dr["Telefon3"].ToString();
                txtmail.Text = dr["Mail"].ToString();
                mskfaks.Text = dr["Faks"].ToString();
                cmbil.Text = dr["Il"].ToString();
                cmbilce.Text = dr["Ilce"].ToString();
                txtvergid.Text = dr["VergiDaire"].ToString();
                rchadres.Text = dr["Adres"].ToString();
                txtkod1.Text = dr["OzelKod1"].ToString();
                txtkod2.Text = dr["OzelKod2"].ToString();
                txtkod3.Text = dr["OzelKod3"].ToString();
            }
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into tbl_firmalar (Ad,YetkiliStatu,YetkiliAdSoyad,YetkiliTc,Sektor,Telefon1,Telefon2,Telefon3,Mail,Faks,Il,Ilce,VergiDaire,Adres,OzelKod1,OzelKod2,OzelKod3) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtygorev.Text);
            komut.Parameters.AddWithValue("@p3", txtyetkili.Text);
            komut.Parameters.AddWithValue("@p4", msktc.Text);
            komut.Parameters.AddWithValue("@p5", txtsektor.Text);
            komut.Parameters.AddWithValue("@p6", msktel1.Text);
            komut.Parameters.AddWithValue("@p7", msktel2.Text);
            komut.Parameters.AddWithValue("@p8", msktel3.Text);
            komut.Parameters.AddWithValue("@p9", txtmail.Text);
            komut.Parameters.AddWithValue("@p10", mskfaks.Text);
            komut.Parameters.AddWithValue("@p11", cmbil.Text);
            komut.Parameters.AddWithValue("@p12", cmbilce.Text);
            komut.Parameters.AddWithValue("@p13", txtvergid.Text);
            komut.Parameters.AddWithValue("@p14", rchadres.Text);
            komut.Parameters.AddWithValue("@p15", txtkod1.Text);
            komut.Parameters.AddWithValue("@p16", txtkod2.Text);
            komut.Parameters.AddWithValue("@p17", txtkod3.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmalistesi();
            temizle();
        }

        private void cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbilce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("select Ilce from tbl_Ilceler where Sehir=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbil.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbilce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Firma silinsin mi?", "Firma Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("delete from tbl_firmalar where Id=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Firma Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                firmalistesi();
                temizle();
            }
            else if (secim==DialogResult.No)
            {
                firmalistesi();
            }

        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update tbl_firmalar set Ad=@p1,YetkiliStatu=@p2,YetkiliAdSoyad=@p3,YetkiliTc=@p4,Sektor=@p5,Telefon1=@p6,Telefon2=@p7,Telefon3=@p8,Mail=@p9,Faks=@p10,Il=@p11,Ilce=@p12,VergiDaire=@p13,Adres=@p14,OzelKod1=@p15,OzelKod2=@p16,OzelKod3=@p17 where Id=@p18", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtygorev.Text);
            komut.Parameters.AddWithValue("@p3", txtyetkili.Text);
            komut.Parameters.AddWithValue("@p4", msktc.Text);
            komut.Parameters.AddWithValue("@p5", txtsektor.Text);
            komut.Parameters.AddWithValue("@p6", msktel1.Text);
            komut.Parameters.AddWithValue("@p7", msktel2.Text);
            komut.Parameters.AddWithValue("@p8", msktel3.Text);
            komut.Parameters.AddWithValue("@p9", txtmail.Text);
            komut.Parameters.AddWithValue("@p10", mskfaks.Text);
            komut.Parameters.AddWithValue("@p11", cmbil.Text);
            komut.Parameters.AddWithValue("@p12", cmbilce.Text);
            komut.Parameters.AddWithValue("@p13", txtvergid.Text);
            komut.Parameters.AddWithValue("@p14", rchadres.Text);
            komut.Parameters.AddWithValue("@p15", txtkod1.Text);
            komut.Parameters.AddWithValue("@p16", txtkod2.Text);
            komut.Parameters.AddWithValue("@p17", txtkod3.Text);
            komut.Parameters.AddWithValue("@p18", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmalistesi();
            temizle();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
