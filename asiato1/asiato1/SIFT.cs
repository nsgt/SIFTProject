using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;

namespace asiato1
{
    class SIFT
    {
        public struct KEYPOINT
        {
            double x;           //位置x
            double y;           //位置y

            int o;              //オクターブ数
            int s;              //スケール
            public KEYPOINT(double _x, double _y, int _o, int _s)
            {
                x = _x; y = _y; o = _o; s = _s;
            }
        }
        int minW = 20;
        int oct =  0;

        Filter f = new Filter();

        public List<byte[,]> DoG(byte[,] ORG, double k, double sigma, int kMax)
        {
            List<byte[,]> D = new List<byte[,]>(); 
            List<byte[,]> L = new List<byte[,]>();
            byte[,] IMG = new byte[ORG.GetLength(0), ORG.GetLength(1)];
            for (int i = 0; i < kMax; i++)
            {
                L.Add(f.gaussian(ORG,11, Math.Pow(k,i) * sigma));
            }
            for (int j = 0; j < kMax - 1; j++)
            {
                for (int x = 0; x < ORG.GetLength(0); x++)
                {
                    for (int y = 0; y < ORG.GetLength(1); y++)
                    {
                        if ((byte)L[j].GetValue(x, y) >= (byte)L[j + 1].GetValue(x, y))
                        {
                            IMG[x, y] = (byte)((byte)L[j].GetValue(x, y) - (byte)L[j + 1].GetValue(x, y));
                        }
                        else
                        {
                            IMG[x, y] = 0;
                        }
                    }

                }
               D.Add(IMG);
            }
            return D;
        }
        public List<KEYPOINT> serachKeypoint(byte[,] ORG,int S,double sigma0)
        {
            List<KEYPOINT> pointsList = new List<KEYPOINT>();
            List<byte[,]> D = new List<byte[,]>();
            List<List<byte[,]>> DoGList = new List<List<byte[,]>>();
            bool[,] flag =new bool[ORG.GetLength(0),ORG.GetLength(1)];
            bool isMax = false;
            bool isSmall = false;
            double k = Math.Pow(2, 1 /(double) S);
            for(int W = ORG.GetLength(0);W>minW;W/=2) {
                D = DoG(ORG, k, sigma0, S + 3);
                DoGList.Add(D);
                oct += 1;
                ORG= down_sampling(ORG);
            }
            for (int oct = 0; oct < DoGList.Count; oct++)
            {
                for (int scale = 1; scale < S + 1; scale++)
                {
                    for (int x = 1; x < DoGList[oct][scale].GetLength(0) - 1; x++)
                    {
                        for (int y = 1; y < DoGList[oct][scale].GetLength(1) - 1; y++)
                        {
                            if (flag[x, y] == true)
                            {
                                continue;
                            }
                            isMax = true;
                            isSmall = true;

                            for (int s = scale-1; s <= scale+1; s++)
                            {
                                for (int w = x - 1; w <= x + 1; w++)
                                {
                                    for (int h = y - 1; h <= y + 1; h++)
                                    {
                                       

                                        if ( w == x && h == y) continue;
                                        if (isMax && (byte)DoGList[oct][scale].GetValue(x, y) <= (byte)DoGList[oct][s].GetValue(w, h))
                                        {
                                            isMax = false;
                                        }

                                        if (isSmall && (byte)DoGList[oct][scale].GetValue(x, y) >= (byte)DoGList[oct][s].GetValue(w, h))
                                        {
                                            isSmall = false;
                                        }
                                        if (isSmall == false && isMax == false)
                                        {
                                            break;
                                        }
                                    }
                                    if (isSmall == false && isMax == false)
                                    {
                                        break;
                                    }
                                }
                                if (isSmall == false && isMax == false)
                                {
                                    break;
                                }
                            }
                            if (isMax || isSmall)
                            {
                                KEYPOINT Keypoint = new KEYPOINT(x, y, oct, scale);
                                pointsList.Add(Keypoint);
                                flag[x, y] = true;
                            }
                        }
                    }
                }
            }
            return pointsList;
        }
        public byte[,] down_sampling(byte[,] ORG)
        {
            int W = ORG.GetLength(0) / 2;
            int H = ORG.GetLength(1) / 2;
            byte[,] IMG = new byte[W,H];

            for (int x = 0; x < W; x++)
            {
                for (int y = 0; y < H; y++)
                {
                    IMG[x,y] = (byte)ORG[2*x, 2*y];
                }
            }
            return IMG;
        }
    }
}
