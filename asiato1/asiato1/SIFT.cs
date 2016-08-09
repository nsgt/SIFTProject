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
       
        Filter f = new Filter();

        public List<byte[,]> DoG(byte[,] ORG, double k, double sigma, int kMax)
        {
            List<byte[,]> D = new List<byte[,]>(); 
            List<byte[,]> L = new List<byte[,]>();
            byte[,] IMG = new byte[ORG.GetLength(0), ORG.GetLength(1)];
            for (int i = 0; i < kMax; i++)
            {
                L.Add(f.gaussian(ORG, 7, Math.Pow(k, (i)) * sigma));
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
    }
}
