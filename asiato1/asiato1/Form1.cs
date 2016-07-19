using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace asiato1
{
    public partial class Form1 : Form
    {
        public string fileName;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           IncludeFile();

            //ガウシアンフィルタをかける
            Bitmap gyate = new Bitmap(pictureBox1.Image);//Gaussianフィルタ呼び出し
            Filter fn = new Filter();
            gyate = fn.Apply(gyate,5);

            //グレースケール化
            Bitmap img = new Bitmap(gyate);
            Grayscale glayImg = new Grayscale();
            img = glayImg.CreateGrayscaleImage(img);

            //表示
            pictureBox1.Image = img;
            
            
        }

        private void IncludeFile()
        {
            //描画先とするImageオブジェクトを作成する
            Bitmap canvas = new Bitmap(256, 256);
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics g = Graphics.FromImage(canvas);


            //openFileDialogの新しいインスタンスを生成する
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult dr;
            // OpenFileDialogの結果をdrに格納
            dr = openFileDialog1.ShowDialog(this);              //ShowDialogで実行時にダイアログボックスを表示する
                                                                //thisで現在実行中のクラスの意味
            fileName = openFileDialog1.FileName;                 //FileNameは選択されたファイル名(ファイルへのパスを含む)


            //オープンダイアログから読み込んだfileNameを、Imageオブジェクトとして取得する
            Image img = Image.FromFile(fileName);
            //画像のサイズをcanvasに描画する
            g.DrawImage(img, 0, 0, 256, 256);
            //Imageオブジェクトのリソースを解放する
            img.Dispose();

            //Graphicsオブジェクトのリソースを解放する
            g.Dispose();
            //PictureBox1に表示する
            pictureBox1.Image = canvas;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }


    }
}
