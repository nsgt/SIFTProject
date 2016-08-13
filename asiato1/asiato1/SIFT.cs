using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;

namespace asiato1
{
    public class SIFT
    {
        struct KEYPOINT
        {
            double x;           //位置x
            double y;           //位置y

            int o;              //オクターブ数
            int s;              //スケール
            KEYPOINT(double _x, double _y, int _o, int _s)
            {
                x = _x; y = _y; o = _o; s = _s;
            }
        };

        Filter f = new Filter();

        public List<byte[,]> DoG(byte[,] ORG, double k, double sigma, int kMax)
        {
            List<byte[,]> D = new List<byte[,]>(); 
            List<byte[,]> L = new List<byte[,]>();
            byte[,] IMG = new byte[ORG.GetLength(0), ORG.GetLength(1)];
            for (int i = 0; i < kMax; i++)
            {
                L.Add(f.gaussian(ORG, 9, Math.Pow(k, (i)) * sigma));
            }
            for (int j = 0; j < kMax - 1; j++)
            {
                for (int x = 0; x < ORG.GetLength(0); x++)
                {
                    for (int y = 0; y < ORG.GetLength(1); y++)
                    {
                        IMG[x, y] = (byte)(Math.Abs((byte)L[j].GetValue(x, y) - (byte)L[j + 1].GetValue(x, y)));
                    }
                }
               D.Add(IMG);
            }
            return D;
        }
        public List<KEYPOINT> serachExtearmValue(List<byte[,]> DoGList)
        {
            List<KEYPOINT> pointsList = new List<KEYPOINT>();
            bool flag = false;
            for (int i = 1; i < DoGList.Count - 2; i++)
            {
                for (int x = 0; x <DoGList[i].GetLength(0); x++)
                {
                    for (int y = 0; y <DoGList[i].GetLength(1) ; y++)
                    {
                        for(int n = -1; n <=1; n++)
                        {
                            for(int k = -1; k <= 1; k++)
                            {
                                if (n + x > -1 && n + x < DoGList[i].GetLength(0) && k + y > -1 && k + y < DoGList[i].GetLength(1))
                                {
                                    if((byte)DoGList[i].GetValue(x,y)>(byte)DoGList[i-1].GetValue(x+n, y+k))
                                    {
                                        
                                    }
                                }
                            }
                        }
                        
                    }
                }
            }
            return pointsList;
        }
        public byte[,] down_sampling(byte[,] ORG,int scale)
        {
            int W = ORG.GetLength(0) / scale;
            int H = ORG.GetLength(1) / scale;
            byte[,] IMG = new byte[W,H];

            for (int x = 0; x < W; x++)
            {
                for (int y = 0; y < H; y++)
                {
                    IMG[x,y] = (byte)ORG[scale * x,scale * y];
                }
            }
            return IMG;
        }
    }
}
