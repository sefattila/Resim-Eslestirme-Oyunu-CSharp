using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {      //resimleri resources ettim
        Image[] resimler = { Properties.Resources.arsenal, Properties.Resources.barcelona, Properties.Resources.beşiktaş, Properties.Resources.juventus, Properties.Resources.madrid, Properties.Resources.manchester, Properties.Resources.napoli, Properties.Resources.porto, };
        int[] resimleriKaristir = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };//resimleri karıştırmada kullandığım dizi
        PictureBox ilkResim;
        int ilkIndex, kontrol =0;
        DateTime time = new DateTime();
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            karistir();
            timer1.Start();
            timer1.Interval = 1000;
        }
        private void karistir()
        {
            Random random = new Random();
            for(int i = 0; i <16; i++)//resimleri birbiri içerisinden bir dizide rastgele yerleştirmke için bu sayede program her açıldığında resimler farklı konumlarda olacak
            {
                int a = random.Next(16);
                int gecici = resimleriKaristir[i];
                resimleriKaristir[i] = resimleriKaristir[a];
                resimleriKaristir[a] = gecici;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time = time.AddSeconds(1);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        { 
            PictureBox gonder = (PictureBox)sender;//bütün boxları 1 box altında topladım hepsinin Clik'ini boz1 e verdim
            int resimNo = int.Parse(gonder.Name.Substring(10));//10'uncu karakterden sonra gelen sayıyı tutabilmek için
            int x = resimleriKaristir[resimNo-1];//15 indis olduğu için 1 çıkardık
            gonder.Image = resimler[x];
            gonder.Refresh();
            if (ilkResim == null)
            {
                ilkResim = gonder;
                ilkIndex = x;
            }
            else
            {
                System.Threading.Thread.Sleep(500);//resimler açıldığında yarım saniye bekler sonra kapanır
                ilkResim.Image = null;
                gonder.Image = null;
                if (ilkIndex == x)
                {
                    ilkResim.Image = resimler[x];//resimler aynı ise ekranda açık olarak kalmasını sağladım
                    gonder.Image = resimler[x];
                    kontrol++;//her seferinde kontrolü artırdım. 8 olunca bittiğini anlayalım diye
            
                    if (kontrol == 8)
                    {
                        timer1.Stop();
                        MessageBox.Show("Tebrikler "+time.Minute.ToString()+" Dakika "+time.Second.ToString()+" Saniyede Yaptınız");// saniye ve dakika cinsinden yazdırdım
                        Application.Restart();
                        /*kontrol = 0;     // kodu yeniden başlatmak için yaptım ama application restart yaptım daha sonradan
                        foreach(Control yenile in Controls)
                        {
                            yenile.Visible = true;
                        }
                        karistir();*/
                    }       
                }
                ilkResim = null;
            }
        }

    }
}
