using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace asiato1
{
    class ColorChange
    {
    public byte[,] rgb2gray(Bitmap ORG)
        {
            
            // ORGのピクセルデータを格納するための配列
            byte[,] data = new byte[ORG.Width, ORG.Height];

            // ORGオブジェクトの画像ピクセル値を配列に挿入
            for (int i = 0; i < ORG.Height; i++)
            {
                for (int j = 0; j < ORG.Width; j++)
                {
                    // ここではグレイスケールに変換して格納
                    data[j, i] =
                        (byte)(
                        (ORG.GetPixel(j, i).R +
                        ORG.GetPixel(j, i).B +
                        ORG.GetPixel(j, i).G) / 3);
                }
            }
            return data;
        }
    }
}
