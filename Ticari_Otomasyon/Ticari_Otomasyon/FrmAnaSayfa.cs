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
using System.Xml;

namespace Ticari_Otomasyon
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        //stoklar
        void stoklar()
        {
            SqlDataAdapter da = new SqlDataAdapter("select UrunAd,Sum(Adet) as 'Adet' from Tbl_Urunler group by UrunAd having sum(adet) <= 20 order by sum(adet)", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridcontrolstoklar.DataSource = dt;
        }

        //ajanda
        void ajanda()
        {
            SqlDataAdapter da = new SqlDataAdapter("select top 10 Tarih,Saat,Baslik from Tbl_Notlar order by ID desc ", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControlajanda.DataSource = dt;
        }

        //firma hareketleri
        void firmahareketleri()
        {
            SqlDataAdapter da = new SqlDataAdapter("execute FirmaHareketler2", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControlfirmahareket.DataSource = dt;
        }

        //fihrist
        void fihrist()
        {
            SqlDataAdapter da = new SqlDataAdapter("select Ad,Telefon1 from tbl_Firmalar",bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControlfihrist.DataSource = dt;
        }

        //haberler
        void haberler()
        {
            XmlReader xmloku = new XmlTextReader("http://www.hurriyet.com.tr/rss/anasayfa");
            while (xmloku.Read())
            {
                if (xmloku.Name == "title")
                {
                    listBox1.Items.Add(xmloku.ReadString());
                }
            }
        }
        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            stoklar();
            ajanda();
            firmahareketleri();
            fihrist();
            haberler();
            webBrowser1.Navigate("https://www.tcmb.gov.tr/kurlar/kurlar_tr.html");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            stoklar();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ajanda();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            firmahareketleri();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            fihrist();
        }
    }
}
