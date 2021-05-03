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
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_admin", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();

            txtkullaniciad.Text = "";
            txtsifre.Text = "";
        }

        private void btnislem_Click(object sender, EventArgs e)
        {
            if (btnislem.Text == "Kaydet")
            {
                SqlCommand komut = new SqlCommand("insert into tbl_admin values (@p1,@p2)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtkullaniciad.Text);
                komut.Parameters.AddWithValue("@p2", txtsifre.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Yeni Admin Sisteme Kaydedildi!", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            if (btnislem.Text == "Güncelle")
            {
                SqlCommand komut2 = new SqlCommand("update tbl_admin set Sifre=@p1 where KullaniciAd=@p2", bgl.baglanti());
                komut2.Parameters.AddWithValue("@p1", txtsifre.Text);
                komut2.Parameters.AddWithValue("@p2", txtkullaniciad.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kayıt Güncellendi!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
            }

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtkullaniciad.Text = dr["KullaniciAd"].ToString();
                txtsifre.Text = dr["Sifre"].ToString();
            }
        }

        private void txtkullaniciad_TextChanged(object sender, EventArgs e)
        {
            if (txtkullaniciad.Text != "")
            {
                btnislem.Text = "Güncelle";
            }
            else
            {
                btnislem.Text = "Kaydet";
            }
        }
    }
}
