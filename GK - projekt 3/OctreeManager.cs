using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK___projekt_3
{
    public static class OctreeManager
    {
        public static int Bit(int x, int i)
        {
            return (x >> i) & 1;
        }

        public static Node Search(Node head, Color rgb, int destinationHeight)
        {
            Node n = head;

            int i = 7;

            while (i >= 0 && n.Height <= destinationHeight && n.Type != 1)
            {
                int bitR = Bit(rgb.R, i);
                int bitG = Bit(rgb.G, i);
                int bitB = Bit(rgb.B, i);

                if (bitR == 0 && bitG == 0 && bitB == 0)
                {
                    n = n.Next[0];
                }
                else if (bitR == 0 && bitG == 0 && bitB == 1)
                {
                    n = n.Next[1];
                }
                else if (bitR == 0 && bitG == 1 && bitB == 0)
                {
                    n = n.Next[2];
                }
                else if (bitR == 0 && bitG == 1 && bitB == 1)
                {
                    n = n.Next[3];
                }
                else if (bitR == 1 && bitG == 0 && bitB == 0)
                {
                    n = n.Next[4];
                }
                else if (bitR == 1 && bitG == 0 && bitB == 1)
                {
                    n = n.Next[5];
                }
                else if (bitR == 1 && bitG == 1 && bitB == 0)
                {
                    n = n.Next[6];
                }
                else if (bitR == 1 && bitG == 1 && bitB == 1)
                {
                    n = n.Next[7];
                }

                i--;
            }

            return n;
        }

        public static void InsertNewColor(ref Node head, Color rgb, int i)
        {
            Node node = head;
            int height = 1;

            while (i >= 0)
            {
                int bitR = Bit(rgb.R, i);
                int bitG = Bit(rgb.G, i);
                int bitB = Bit(rgb.B, i);

                if (bitR == 0 && bitG == 0 && bitB == 0)
                {
                    if (node.Next[0] == null)
                        node.Next[0] = i != 0 ? new Node(0, height, rgb) : new Node(1, height, rgb);

                    node = node.Next[0];
                }
                else if (bitR == 0 && bitG == 0 && bitB == 1)
                {
                    if (node.Next[1] == null)
                        node.Next[1] = i != 0 ? new Node(0, height, rgb) : new Node(1, height, rgb);

                    node = node.Next[1];
                }
                else if (bitR == 0 && bitG == 1 && bitB == 0)
                {
                    if (node.Next[2] == null)
                        node.Next[2] = i != 0 ? new Node(0, height, rgb) : new Node(1, height, rgb);

                    node = node.Next[2];
                }
                else if (bitR == 0 && bitG == 1 && bitB == 1)
                {
                    if (node.Next[3] == null)
                        node.Next[3] = i != 0 ? new Node(0, height, rgb) : new Node(1, height, rgb);

                    node = node.Next[3];
                }
                else if (bitR == 1 && bitG == 0 && bitB == 0)
                {
                    if (node.Next[4] == null)
                        node.Next[4] = i != 0 ? new Node(0, height, rgb) : new Node(1, height, rgb);

                    node = node.Next[4];
                }
                else if (bitR == 1 && bitG == 0 && bitB == 1)
                {
                    if (node.Next[5] == null)
                        node.Next[5] = i != 0 ? new Node(0, height, rgb) : new Node(1, height, rgb);

                    node = node.Next[5];
                }
                else if (bitR == 1 && bitG == 1 && bitB == 0)
                {
                    if (node.Next[6] == null)
                        node.Next[6] = i != 0 ? new Node(0, height, rgb) : new Node(1, height, rgb);

                    node = node.Next[6];
                }
                else if (bitR == 1 && bitG == 1 && bitB == 1)
                {
                    if (node.Next[7] == null)
                        node.Next[7] = i != 0 ? new Node(0, height, rgb) : new Node(1, height, rgb);

                    node = node.Next[7];
                }

                i--;
                height++;
            }
        }

        public static void InsertNewColorK(ref Node head, Color rgb, int i, int _K)
        {
            Node node = head;
            int height = 1;
            int K = _K;

            while (i >= 0)
            {
                int bitR = Bit(rgb.R, i);
                int bitG = Bit(rgb.G, i);
                int bitB = Bit(rgb.B, i);

                if (bitR == 0 && bitG == 0 && bitB == 0)
                {
                    if (node.Next[0] == null)
                        node.Next[0] = i != 0 ? new Node(0, height, rgb) : new Node(1, height, rgb);

                    int f = 0, childrenAmount = 0;

                    foreach (Node child in node.Next)
                    {
                        childrenAmount++;
                        if (child != null) f++;
                    }

                    if (f > K && node != head)
                    {
                        var sumR = 0;
                        var sumG = 0;
                        var sumB = 0;

                        foreach (Node child in node.Next)
                        {
                            sumR += node.RGB.R;
                            sumG += node.RGB.G;
                            sumB += node.RGB.B;
                        }

                        node.RGB = Color.FromArgb(255, sumR / childrenAmount, sumG / childrenAmount, sumB / childrenAmount);
                        node.Type = 1;
                        return;
                    }

                    node = node.Next[0];
                }
                else if (bitR == 0 && bitG == 0 && bitB == 1)
                {
                    if (node.Next[1] == null)
                        node.Next[1] = i != 0 ? new Node(0, height, rgb) : new Node(1, height, rgb);
                    int f = 0, childrenAmount = 0;

                    foreach (Node child in node.Next)
                    {
                        childrenAmount++;
                        if (child != null) f++;
                    }

                    if (f > K && node != head)
                    {
                        var sumR = 0;
                        var sumG = 0;
                        var sumB = 0;

                        foreach (Node child in node.Next)
                        {
                            sumR += node.RGB.R;
                            sumG += node.RGB.G;
                            sumB += node.RGB.B;
                        }

                        node.RGB = Color.FromArgb(255, sumR / childrenAmount, sumG / childrenAmount, sumB / childrenAmount);
                        node.Type = 1;
                        return;
                    }
                    node = node.Next[1];
                }
                else if (bitR == 0 && bitG == 1 && bitB == 0)
                {
                    if (node.Next[2] == null)
                        node.Next[2] = i != 0 ? new Node(0, height, rgb) : new Node(1, height, rgb);
                    int f = 0, childrenAmount = 0;

                    foreach (Node child in node.Next)
                    {
                        childrenAmount++;
                        if (child != null) f++;
                    }

                    if (f > K && node != head)
                    {
                        var sumR = 0;
                        var sumG = 0;
                        var sumB = 0;

                        foreach (Node child in node.Next)
                        {
                            sumR += node.RGB.R;
                            sumG += node.RGB.G;
                            sumB += node.RGB.B;
                        }

                        node.RGB = Color.FromArgb(255, sumR / childrenAmount, sumG / childrenAmount, sumB / childrenAmount);
                        node.Type = 1;
                        return;
                    }
                    node = node.Next[2];
                }
                else if (bitR == 0 && bitG == 1 && bitB == 1)
                {
                    if (node.Next[3] == null)
                        node.Next[3] = i != 0 ? new Node(0, height, rgb) : new Node(1, height, rgb);
                    int f = 0, childrenAmount = 0;

                    foreach (Node child in node.Next)
                    {
                        childrenAmount++;
                        if (child != null) f++;
                    }

                    if (f > K && node != head)
                    {
                        var sumR = 0;
                        var sumG = 0;
                        var sumB = 0;

                        foreach (Node child in node.Next)
                        {
                            sumR += node.RGB.R;
                            sumG += node.RGB.G;
                            sumB += node.RGB.B;
                        }

                        node.RGB = Color.FromArgb(255, sumR / childrenAmount, sumG / childrenAmount, sumB / childrenAmount);
                        node.Type = 1;
                        return;
                    }
                    node = node.Next[3];
                }
                else if (bitR == 1 && bitG == 0 && bitB == 0)
                {
                    if (node.Next[4] == null)
                        node.Next[4] = i != 0 ? new Node(0, height, rgb) : new Node(1, height, rgb);
                    int f = 0, childrenAmount = 0;

                    foreach (Node child in node.Next)
                    {
                        childrenAmount++;
                        if (child != null) f++;
                    }

                    if (f > K && node != head)
                    {
                        var sumR = 0;
                        var sumG = 0;
                        var sumB = 0;

                        foreach (Node child in node.Next)
                        {
                            sumR += node.RGB.R;
                            sumG += node.RGB.G;
                            sumB += node.RGB.B;
                        }

                        node.RGB = Color.FromArgb(255, sumR / childrenAmount, sumG / childrenAmount, sumB / childrenAmount);
                        node.Type = 1;
                        return;
                    }
                    node = node.Next[4];
                }
                else if (bitR == 1 && bitG == 0 && bitB == 1)
                {
                    if (node.Next[5] == null)
                        node.Next[5] = i != 0 ? new Node(0, height, rgb) : new Node(1, height, rgb);
                    int f = 0, childrenAmount = 0;

                    foreach (Node child in node.Next)
                    {
                        childrenAmount++;
                        if (child != null) f++;
                    }

                    if (f > K && node != head)
                    {
                        var sumR = 0;
                        var sumG = 0;
                        var sumB = 0;

                        foreach (Node child in node.Next)
                        {
                            sumR += node.RGB.R;
                            sumG += node.RGB.G;
                            sumB += node.RGB.B;
                        }

                        node.RGB = Color.FromArgb(255, sumR / childrenAmount, sumG / childrenAmount, sumB / childrenAmount);
                        node.Type = 1;
                        return;
                    }
                    node = node.Next[5];
                }
                else if (bitR == 1 && bitG == 1 && bitB == 0)
                {
                    if (node.Next[6] == null)
                        node.Next[6] = i != 0 ? new Node(0, height, rgb) : new Node(1, height, rgb);

                    int f = 0, childrenAmount = 0;

                    foreach (Node child in node.Next)
                    {
                        childrenAmount++;
                        if (child != null) f++;
                    }

                    if (f > K && node != head)
                    {
                        var sumR = 0;
                        var sumG = 0;
                        var sumB = 0;

                        foreach (Node child in node.Next)
                        {
                            sumR += node.RGB.R;
                            sumG += node.RGB.G;
                            sumB += node.RGB.B;
                        }

                        node.RGB = Color.FromArgb(255, sumR / childrenAmount, sumG / childrenAmount, sumB / childrenAmount);
                        node.Type = 1;
                        return;
                    }
                    node = node.Next[6];
                }
                else if (bitR == 1 && bitG == 1 && bitB == 1)
                {
                    if (node.Next[7] == null)
                        node.Next[7] = i != 0 ? new Node(0, height, rgb) : new Node(1, height, rgb);
                    int f = 0, childrenAmount = 0;

                    foreach (Node child in node.Next)
                    {
                        childrenAmount++;
                        if (child != null) f++;
                    }

                    if (f > K && node != head)
                    {
                        var sumR = 0;
                        var sumG = 0;
                        var sumB = 0;

                        foreach (Node child in node.Next)
                        {
                            sumR += node.RGB.R;
                            sumG += node.RGB.G;
                            sumB += node.RGB.B;
                        }

                        node.RGB = Color.FromArgb(255, sumR / childrenAmount, sumG / childrenAmount, sumB / childrenAmount);
                        node.Type = 1;
                        return;
                    }
                    node = node.Next[7];
                }

                i--;
                height++;
            }
        }
    }
}
