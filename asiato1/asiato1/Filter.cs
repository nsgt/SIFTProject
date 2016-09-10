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
    class Filter {
        public byte[,] gaussian(byte[,] ORG, int size, double sigma)
        {
          
                byte[,] IMG = new byte[ORG.GetLength(0), ORG.GetLength(1)];
                for (int i = 0; i < ORG.GetLength(1); i++)
                {
                    for (int j = 0; j < ORG.GetLength(0); j++)
                    {
                        double sumweight = 0;
                        double mask = 0;
                        double sum = 0;
                        for (int k = -size / 2; k <= size / 2; k++)
                        {
                            for (int n = -size / 2; n <= size / 2; n++)
                            {
                                if (j + n >= 0 && j + n < ORG.GetLength(0) && i + k >= 0 && i + k < ORG.GetLength(1))
                                {
                                    mask = (1/(2*Math.PI*Math.Pow(sigma,2)))*Math.Exp(-(Math.Pow(k,2) + Math.Pow(n,2)) / (2 * Math.Pow(sigma,2)));
                                    sumweight += mask;
                                    sum += ((double)ORG[j+n, i+k]) * mask;
                                }
                            }
                        }

                        sum = sum / sumweight;
                        if (sum > 255.0)
                        {
                            IMG[j, i] = 255;
                        }
                        else if (sum < 0)
                        {
                            IMG[j, i] = 0;
                        }
                        else
                        {
                            IMG[j, i] = (byte)sum;
                        }
                    }
                }
                return IMG;
            }
        }
    }


    