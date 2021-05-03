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
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void bankaliste()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("execute bankabilgileri", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            txtbankaad.Text = "";
            lookUpEdit1.Text = "";
            txthesapno.Text = "";
            txthesaptur.Text = "";
            txtid.Text = "";
            txtsube.Text = "";
            txtyetkili.Text = "";
            cmbil.Text = "";
            cmbilce.Text = "";
            mskiban.Text = "";
            msktarih.Text = "";
            msktel.Text = "";

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

        void firmalistesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ID,Ad from tbl_firmalar ", bgl.baglanti());
            da.Fill(dt);
            lookUpEdit1.Properties.NullText = "Bir Firma Seçin";
            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "Ad";
            lookUpEdit1.Properties.DataSource = dt;
        }
        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            bankaliste();
            temizle();
            sehirlistesi();
            firmalistesi();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into tbl_bankalar (BankaAd,Il,Ilce,Sube,Iban,HesapNo,Yetkili,Telefon,Tarih,HesapTuru,FirmaID) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtbankaad.Text);
            komut.Parameters.AddWithValue("@p2", cmbil.Text);
            komut.Parameters.AddWithValue("@p3", cmbilce.Text);
            komut.Parameters.AddWithValue("@p4", txtsube.Text);
            komut.Parameters.AddWithValue("@p5", mskiban.Text);
            komut.Parameters.AddWithValue("@p6", txthesapno.Text);
            komut.Parameters.AddWithValue("@p7", txtyetkili.Text);
            komut.Parameters.AddWithValue("@p8", msktel.Text);
            komut.Parameters.AddWithValue("@p9", msktarih.Text);
            komut.Parameters.AddWithValue("@p10", txthesaptur.Text);
            komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);
            komut.ExecuteNonQuery();
            bankaliste();
            bgl.baglanti().Close();
            MessageBox.Show("Banka Listeye Eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtid.Text = dr["ID"].ToString();
            txtbankaad.Text = dr["BankaAd"].ToString();
            cmbil.Text = dr["Il"].ToString();
            cmbilce.Text = dr["Ilce"].ToString();
            txtsube.Text = dr["Sube"].ToString();
            mskiban.Text = dr["Iban"].ToString();
            txthesapno.Text = dr["HesapNo"].ToString();
            txtyetkili.Text = dr["Yetkili"].ToString();
            msktel.Text = dr["Telefon"].ToString();
            msktarih.Text = dr["Tarih"].ToString();
            txthesaptur.Text = dr["HesapTuru"].ToString();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            DialogResult secim = new DialogResult();
            secim = MessageBox.Show("Banka bilgisi sistemden silinsin mi?", "Banka Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("delete from tbl_bankalar where Id=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                temizle();
                MessageBox.Show("Banka Bilgisi Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                bankaliste();
            }
            else if (secim == DialogResult.No)
            {
                bankaliste();
            }

        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update tbl_bankalar set BankaAd=@p1,Il=@p2,Ilce=@p3,Sube=@p4,Iban=@p5,HesapNo=@p6,Yetkili=@p7,Telefon=@p8,Tarih=@p9,HesapTuru=@p10,FirmaID=@p11 where Id=@p12", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtbankaad.Text);
            komut.Parameters.AddWithValue("@p2", cmbil.Text);
            komut.Parameters.AddWithValue("@p3", cmbilce.Text);
            komut.Parameters.AddWithValue("@p4", txtsube.Text);
            komut.Parameters.AddWithValue("@p5", mskiban.Text);
            komut.Parameters.AddWithValue("@p6", txthesapno.Text);
            komut.Parameters.AddWithValue("@p7", txtyetkili.Text);
            komut.Parameters.AddWithValue("@p8", msktel.Text);
            komut.Parameters.AddWithValue("@p9", msktarih.Text);
            komut.Parameters.AddWithValue("@p10", txthesaptur.Text);
            komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);
            komut.Parameters.AddWithValue("@p12", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            bankaliste();
            MessageBox.Show("Banka Bilgisi Güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
        }
    }
}
