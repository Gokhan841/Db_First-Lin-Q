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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        DbSınavEntities db = new DbSınavEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)
            {
                var degerler = db.TblNotlar.Where(x => x.Sınav1 > 70);
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton2.Checked == true)
            {
                var degerler = db.TblOgrenci.Where(x => x.Ad== "ali");
                dataGridView1.DataSource = degerler.ToList();
            }
            if(radioButton3.Checked==true)
            {
                var degerler = db.TblOgrenci.Where(x => x.Ad == textBox1.Text || x.SoyAd == textBox1.Text);
                dataGridView1.DataSource = degerler.ToList();
            }
            if(radioButton4.Checked==true)
            {
                var degerler = db.TblOgrenci.Select(x =>
                  new
                  {
                      adı = x.Ad.ToLower(),
                      soyadı=x.SoyAd.ToUpper()
                  }) ;
                dataGridView1.DataSource = degerler.ToList();
            }
            if(radioButton5.Checked==true)
            {
                var degerler = db.TblNotlar.Select(x =>
                new{
                    NotId=x.NotId,
                    ortalama=x.Ortalama
                }).Where(x=>x.ortalama>60);
                dataGridView1.DataSource = degerler.ToList();
            }
            if(radioButton6.Checked==true)
            {
                var degerler = db.TblNotlar.Select(x =>
                new
                {
                    NotID = x.NotId,
                    Ortalama = x.Ortalama,
                    Durum = x.Durum == true ? "Geçti" : "Kaldı"
                }).Where(x => x.Durum == "Geçti");
                dataGridView1.DataSource = degerler.ToList();               
            }
            if(radioButton7.Checked==true)
            {
               var degerler=db.TblNotlar.SelectMany(x=>db.TblOgrenci.Where(y=>y.Id==x.Ogr),(x,y)=>
                    new{
                    Ortalama=x.Ortalama,
                    Adı=y.Ad
                    });
                dataGridView1.DataSource = degerler.ToList();
            }
            if(radioButton8.Checked==true)
            {
                var degerler = db.TblOgrenci.OrderBy(x => x.Id).Take(3);
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton9.Checked == true)
            {
                var degerler = db.TblOgrenci.OrderByDescending(x => x.Id).Take(3);
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton10.Checked == true)
            {
                var degerler = db.TblOgrenci.OrderBy(x => x.Ad);
                dataGridView1.DataSource = degerler.ToList();
            }
            if(radioButton11.Checked==true)
            {
                var degerler = db.TblOgrenci.OrderBy(x => x.Id).Skip(5);
                dataGridView1.DataSource = degerler.ToList();
            }
            if(radioButton12.Checked==true)
            {
                var degerler = db.TblOgrenci.OrderBy(x => x.Sehir).GroupBy(y => y.Sehir).Select(z =>
                  new
                  {
                   Sehir=z.Key,
                   OgrenciSayisi=z.Count(),
                  });               
                dataGridView1.DataSource = degerler.ToList();
            }
            if(radioButton13.Checked==true)
            {
                var deger = db.TblNotlar.Max(x => x.Ortalama);
                MessageBox.Show(deger.ToString());
            }
            if (radioButton14.Checked == true)
            {
                var a = db.TblNotlar.Select(x =>
                      new
                      {
                          Ortalama = x.Ortalama,
                          Durumu = x.Durum
                      }).Where(x => x.Durumu == false).Max(x => x.Ortalama).ToString();
                MessageBox.Show(a.ToString());
            }
            if(radioButton15.Checked==true)
            {
                var deger = db.TblUrunler.Count();
                MessageBox.Show(deger.ToString());
            }
            if (radioButton16.Checked == true)
            {
                var toplam = db.TblUrunler.Sum(x => x.Fiyat);
                MessageBox.Show(toplam.ToString());

            }
            if(radioButton17.Checked == true)
            {
                var toplam = db.TblUrunler.Count(x => x.Adı=="buzdolabı");
                MessageBox.Show(toplam.ToString());

            }
            if (radioButton18.Checked == true)
            {
                var ortalama = db.TblUrunler.Where(x => x.Adı == "buzdolabı").Average(y=>y.Fiyat);
                MessageBox.Show(ortalama.ToString());

            }
            if (radioButton20.Checked == true)
            {
                var deger = (from item in db.TblUrunler
                             orderby item.Stok_Sayısı ascending
                             select item.Adı).First();
                MessageBox.Show(deger.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
          //  dataGridView1.DataSource == db.Kulupbilgisi().ToList();
        }
    }
}
