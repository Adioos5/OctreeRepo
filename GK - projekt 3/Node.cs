using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK___projekt_3
{
    public class Node
    {
        public int Type { get; set; }
        public int Height { get; set; }
        public Color RGB { get; set; }
        public Node[] Next { get; set; }
        
        public Node(int type, int height, Color rgb)
        {
            Type = type;
            Height = height;
            RGB = rgb;

            Next = new Node[8];
        }
    }
}
