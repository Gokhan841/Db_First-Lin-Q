using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EntıtıyOrnek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbSınavEntities db = new DbSınavEntities();

        private void button1_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = db.TblOgrenci.ToList();
            dataGridView1.Columns[3].Visible = false;// ilişkili tablolardn geln değerleri sütunlarda görmemek için
            dataGridView1.Columns[4].Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = db.TblDersler.ToList();// datasource veri tabanı bağlantısını yapar.
            //dgv de autosizecloumn özeliğini fill yaparsan dgv komple hücrelerle dolar.
        }

        private void button7_Click(object sender, EventArgs e)
        //Linq Sorgusu ile veri listeleme        
        {
            var sorgu = from item in db.TblNotlar
                        select new { item.NotId, item.TblOgrenci.Ad, item.TblOgrenci.SoyAd, item.TblDersler.DersAdı, item.Sınav1, item.Sınav2, item.Sınav3, item.Ortalama, item.Durum };
            dataGridView1.DataSource = sorgu.ToList();
            //derdId yerine ders adı gelmesini,ve ogrencı adı ve soyadını getirmeyi yptık.
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            TblOgrenci ogr = new TblOgrenci();//öğrenci sınıfından bir nesne ürettik
            ogr.Ad = txtad.Text;
            ogr.SoyAd = txtsoyad.Text;
            ogr.Fotograf = txtFoto.Text;
            db.TblOgrenci.Add(ogr);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Ekleme İşlemi Başarıyla Gerçekleştirilmiştir.");
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtOgrId.Text);
            var x = db.TblOgrenci.Find(id);
            db.TblOgrenci.Remove(x);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Kaydı Silinmiştir.");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtOgrId.Text);
            var x = db.TblOgrenci.Find(id);
            x.Ad = txtad.Text;
            x.SoyAd = txtsoyad.Text;
            x.Fotograf = txtFoto.Text;
            db.SaveChanges();
            MessageBox.Show("Kayıt Başarı ile Güncellenmiştir");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.NotLisstesi();
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            //Lamda ifadelerle öğrenci adı veya soyadından veri listeleme.
            dataGridView1.DataSource = db.TblOgrenci.Where(x => x.Ad == txtad.Text || x.SoyAd == txtsoyad.Text).ToList();
        }

        private void txtad_TextChanged(object sender, EventArgs e)
        {
            //textboxa adı yazarken eş zamanlı olarak sonuçların dgv de listelenmesi.
            var aranan = txtad.Text;
            var adlar = from item in db.TblOgrenci
                        where item.Ad.Contains(aranan)
                        select item;
            dataGridView1.DataSource = adlar.ToList();

        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnSonuclar_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                List<TblOgrenci> Liste1 = db.TblOgrenci.OrderBy(p => p.Ad).ToList();
                dataGridView1.DataSource = Liste1;
            }
            if (radioButton2.Checked == true)
            {
                List<TblOgrenci> Liste2 = db.TblOgrenci.OrderByDescending(p => p.Ad).ToList();
                dataGridView1.DataSource = Liste2;
            }
            if (radioButton3.Checked == true)
            {
                List<TblOgrenci> Liste3 = db.TblOgrenci.OrderBy(p => p.Ad).Take(3).ToList();
                dataGridView1.DataSource = Liste3;
            }
            if (radioButton4.Checked == true)
            {
                List<TblOgrenci> Liste4 = db.TblOgrenci.Where(p => p.Id == 5).ToList();
                dataGridView1.DataSource = Liste4;
            }
            if (radioButton5.Checked == true)
            {
                List<TblOgrenci> Liste5 = db.TblOgrenci.Where(p => p.Ad.StartsWith("a")).ToList();
                dataGridView1.DataSource = Liste5;
            }
            if (radioButton6.Checked == true)
            {
                List<TblOgrenci> Liste6 = db.TblOgrenci.Where(p => p.Ad.EndsWith("a")).ToList();
                dataGridView1.DataSource = Liste6;
            }
            if (radioButton7.Checked == true)
            {
                bool sonuc = db.TblOgrenci.Any();
                if (sonuc == true)
                {
                    MessageBox.Show("Bu tabloda değer mevcut");
                }
                else
                {
                    MessageBox.Show("Bu tabloda değer yok");
                }
            }
            if (radioButton8.Checked == true)
            {
                int deger = db.TblOgrenci.Count();
                MessageBox.Show(deger + " tane veri mevcut");
            }
            if (radioButton9.Checked == true)
            {
                int toplam = Convert.ToInt32(db.TblNotlar.Sum(p => p.Sınav1));
                MessageBox.Show("1.Sınavların Toplamı:" + toplam);
            }
            if (radioButton10.Checked == true)
            {
                int ortalama = Convert.ToInt32(db.TblNotlar.Average(p => p.Sınav1));
                MessageBox.Show("Sınavın Ortalaması:" + ortalama);
                // notu ortlamanın üstende olanların getirilmesi;
                List<TblNotlar> liste10 = db.TblNotlar.Where(x => x.Sınav1 > ortalama).ToList();
                dataGridView1.DataSource = liste10;
            }
            if (radioButton11.Checked == true)
            {
                var deger = db.TblNotlar.Max(p => p.Sınav1);
                MessageBox.Show(deger.ToString());
            }
            //en yüksek notu alan kişiyi getiren kodu yaz.
            if (radioButton12.Checked == true)
            {
                var deger = db.TblNotlar.Min(p => p.Sınav1);
                MessageBox.Show(deger.ToString());
            }
        }

        private void btnJoinleBirlestir_Click(object sender, EventArgs e)
        {
            //eğer yukarıdaki gibi değilde join ile birleştirmek istersek;
            var birlestir = from x in db.TblNotlar
                            join y in db.TblOgrenci
                            on x.Ogr equals y.Id
                            join z in db.TblDersler
                            on x.Ders equals z.DersId
                            select new
                            {
                                ogrenci = y.Ad + y.SoyAd,
                                ders =z.DersAdı,
                                sınav1=x.Sınav1,
                            };
            dataGridView1.DataSource = birlestir.ToList();
        }
    }
}
//1-) Eğer Dataaseden yeni bir tablo ekleyeceksek Model1.edmx[Diagram]' st/update model from databeseden add kısmından tablolara gelerek yapıyoruz.

//2_)create procedure NotLisstei
//as
//select Ad+ ' '+ SoyAd as 'AD SOYAD',DersAdı,NotId,Sınav1, Sınav2, Sınav3, Ortalama,Durum from TblNotlar
//inner join TblOgrenci on TblOgrenci.Id=TblNotlar.Ogr
//inner join TblDersler on TblDersler.DersId= TblNotlar.Ders komutunu sql de yazıp update Model From db diyip Prosedürü ekler ve KAYDEDERSEK! ders id yerine ders adı ogrid yerine ogr adının yazılı oldugu taloyu goruntulerız.