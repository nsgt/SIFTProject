using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;



namespace asiato1
{
    public partial class Form1 : Form
    {
        public string fileName;
        int w = 256;
        int h = 256;
        byte [,] IMG;
        int sw = 0 ;
        List<SIFT.KEYPOINT> list=new List<SIFT.KEYPOINT>();
        List<byte[,]> DoGlist = new List<byte[,]>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            Bitmap ORG = IncludeFile();
            ColorChange color = new ColorChange();
            SIFT sift = new SIFT();
            Filter f = new Filter();
            IMG = color.rgb2gray(ORG);
            list = sift.serachKeypoint(IMG, 3,1.6);
            /*DoGlist = sift.DoG(IMG, Math.Pow(2, 1 / (double)3), 1.6, 6);
            for (int x = 0; x < DoGlist[0].GetLength(0); x++)
            {
                for (int y = 0; y <DoGlist[0].GetLength(1); y++)
                {
                    IMG[x, y] = (byte)(Math.Abs((byte)DoGlist[0].GetValue(x, y) - (byte)DoGlist[1].GetValue(x, y)));
                }
            }*/
       
            imshow(IMG);
        }
       



        private Bitmap IncludeFile()
        { 
            //openFileDialogの新しいインスタンスを生成する
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult dr;
            // OpenFileDialogの結果をdrに格納
            dr = openFileDialog1.ShowDialog(this);              //ShowDialogで実行時にダイアログボックスを表示する
                                                                //thisで現在実行中のクラスの意味
            fileName = openFileDialog1.FileName;     //FileNameは選択されたファイル名(ファイルへのパスを含む)

            Bitmap img = (Bitmap)Image.FromFile(fileName);

            return img;

        }
        private void imshow(byte[,] ORG)
        {
            Bitmap IMG = new Bitmap(ORG.GetLength(0),ORG.GetLength(1));

            //描画先とするImageオブジェクトを作成する
            for (int i = 0; i < ORG.GetLength(1); i++)
            {
                for (int j = 0; j < ORG.GetLength(0); j++)
                {
                    IMG.SetPixel(
                        j,
                        i,
                        Color.FromArgb(
                            (int)ORG[j, i],
                            (int)ORG[j, i],
                            (int)ORG[j, i])
                        );
                }
            }
            Bitmap canvas = new Bitmap(ORG.GetLength(0), ORG.GetLength(1));
            //ImageオブジェクトのGraphicsオブジェクトを作成する
            Graphics g = Graphics.FromImage(canvas);
            //画像のサイズをcanvasに描画する
            g.DrawImage(IMG, 0, 0, ORG.GetLength(0), ORG.GetLength(1));
            //Imageオブジェクトのリソースを解放する
            IMG.Dispose();
            //PictureBox1に表示する
            pictureBox1.Image = canvas;
        }
      

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public void button2_Click(object sender, EventArgs e)
        {
            if(e == null)
            {
                sw = 0;
            }
            else
            {
                sw = 1;
            }
        }
    }
}
