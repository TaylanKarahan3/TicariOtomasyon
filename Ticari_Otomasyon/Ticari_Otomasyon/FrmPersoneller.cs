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
    public partial class FrmPersoneller : Form
    {
        public FrmPersoneller()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void personelliste()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_personeller", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void temizle()
        {
            txtid.Text = "";
            txtad.Text = "";
            txtsoyad.Text = "";
            txtmail.Text = "";
            txtgorev.Text = "";
            msktc.Text = "";
            msktel1.Text = "";
            rchadres.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
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
        Random rast= new Random();
        private void FrmPersoneller_Load(object sender, EventArgs e)
        {
            ımageSlider1.CurrentImageIndex = rast.Next(0, 11);
            ımageSlider2.CurrentImageIndex = rast.Next(0, 11);
            ımageSlider3.CurrentImageIndex = rast.Next(0, 11);
            personelliste();
            temizle();
            sehirlistesi();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into tbl_personeller (Ad,Soyad,Telefon,Tc,Mail,Il,Ilce,Adres,Gorev) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", msktel1.Text);
            komut.Parameters.AddWithValue("@p4", msktc.Text);
            komut.Parameters.AddWithValue("@p5", txtmail.Text);
            komut.Parameters.AddWithValue("@p6", cmbil.Text);
            komut.Parameters.AddWithValue("@p7", cmbilce.Text);
            komut.Parameters.AddWithValue("@p8", rchadres.Text);
            komut.Parameters.AddWithValue("@p9", txtgorev.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel Kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            personelliste();
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtid.Text = dr["ID"].ToString();
                txtad.Text = dr["Ad"].ToString();
                txtsoyad.Text = dr["Soyad"].ToString();
                msktel1.Text = dr["Telefon"].ToString();
                msktc.Text = dr["TC"].ToString();
                txtmail.Text = dr["Mail"].ToString();
                cmbil.Text = dr["Il"].ToString();
                cmbilce.Text = dr["Ilce"].ToString();
                rchadres.Text = dr["Adres"].ToString();
                txtgorev.Text = dr["Gorev"].ToString();
            }
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Personel silinsin mi?", "Personel Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("delete from tbl_personeller where Id=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Personel Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bgl.baglanti().Close();
                personelliste();
                temizle();
            }
            else if (secim == DialogResult.No)
            {
                personelliste();
            }
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update tbl_personeller set Ad=@p1,Soyad=@p2,Telefon=@p3,Tc=@p4,Mail=@p5,Il=@p6,Ilce=@p7,Adres=@p8,Gorev=@p9 where Id=@p10", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", msktel1.Text);
            komut.Parameters.AddWithValue("@p4", msktc.Text);
            komut.Parameters.AddWithValue("@p5", txtmail.Text);
            komut.Parameters.AddWithValue("@p6", cmbil.Text);
            komut.Parameters.AddWithValue("@p7", cmbilce.Text);
            komut.Parameters.AddWithValue("@p8", rchadres.Text);
            komut.Parameters.AddWithValue("@p9", txtgorev.Text);
            komut.Parameters.AddWithValue("@p10", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Personel Güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            personelliste();
        }

    }
}
