using Microsoft.VisualBasic.Logging;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace GK___projekt_3
{
    public partial class Form1 : Form
    {
        private Bitmap imageBitmap, afterBitmap, alongBitmap;
        private Node Head1, Head2;
        private string imagePath;
        private int K_Value;
        private double V_Value;

        public Form1()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            afterBitmap = new Bitmap(Canvas.Size.Width, Canvas.Size.Height);
            //Canvas.Image = afterBitmap;
            imagePath = "..\\..\\..\\Images\\meadow.jpg";
            K_Value = 2;
            V_Value = 1.0;

            Canvas.Image = ResizeImage(new Bitmap(imagePath),
                new Size(Canvas.Size.Width, Canvas.Size.Height));
            
            PictureBox PictureBox1 = new PictureBox();
            PictureBox1.Image = ResizeImage(((Bitmap)Canvas.Image),
                new Size(AfterCanvas.Size.Width, AfterCanvas.Size.Height));
            imageBitmap = (Bitmap)PictureBox1.Image;

            PictureBox PictureBox2 = new PictureBox();
            PictureBox2.Image = ResizeImage(((Bitmap)Canvas.Image),
                new Size(AfterCanvas.Size.Width, AfterCanvas.Size.Height));
            afterBitmap = (Bitmap)PictureBox2.Image;

            AfterCanvas.Image = afterBitmap;

            InitializeOctree(ref Head1, false);
            InitializeOctree(ref Head2, true);
            RenderImage(DestinationHeightTrackBar.Value, false);
            RenderImage(DestinationHeightTrackBar.Value, true);
        }

        public void InitializeOctree(ref Node Head, bool useK)
        {
            Head = new Node(0, 0, Color.White);

            PictureBox PictureBox3 = new PictureBox();
            PictureBox3.Image = ResizeImage((Bitmap)Canvas.Image,
                new Size(AlongCanvas.Size.Width, AlongCanvas.Size.Height));
            alongBitmap = (Bitmap)PictureBox3.Image;
            AlongCanvas.Image = alongBitmap;

            for (int i = 0; i < AfterCanvas.Size.Width; i++)
            {
                for (int j = 0; j < AfterCanvas.Size.Height; j++)
                {
                    if(useK) OctreeManager.InsertNewColorK(ref Head, imageBitmap.GetPixel(i, j), 7, K_Value);
                    else OctreeManager.InsertNewColor(ref Head, imageBitmap.GetPixel(i, j), 7);
                }
            }
        }

        public void RenderImage(int destinationHeight, bool useK)
        {
            Node head = !useK ? Head1 : Head2;

            for (int i = 0; i < AfterCanvas.Size.Width; i++)
            {
                for (int j = 0; j < AfterCanvas.Size.Height; j++)
                {
                    Node n = OctreeManager.Search(head, imageBitmap.GetPixel(i, j), destinationHeight);
                    var sumR = 0;
                    var sumG = 0;
                    var sumB = 0;

                    var k = 0;

                    foreach (var node in n.Next)
                    {
                        if (node == null) continue;

                        sumR += node.RGB.R;
                        sumG += node.RGB.G;
                        sumB += node.RGB.B;
                        k++;
                    }

                    Color c = Color.FromArgb(255, sumR / k, sumG / k, sumB / k);

                    if(!useK)afterBitmap.SetPixel(i, j, c);
                    else alongBitmap.SetPixel(i, j, c);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dlg.FileName;

                    PictureBox PictureBox1 = new PictureBox();
                    PictureBox1.Image = ResizeImage(new Bitmap(imagePath),
                        new Size(AfterCanvas.Size.Width, AfterCanvas.Size.Height));
                    imageBitmap = (Bitmap)PictureBox1.Image;

                    PictureBox PictureBox2 = new PictureBox();
                    PictureBox2.Image = ResizeImage(new Bitmap(imagePath),
                        new Size(AfterCanvas.Size.Width, AfterCanvas.Size.Height));
                    afterBitmap = (Bitmap)PictureBox2.Image;

                    Canvas.Image = ResizeImage(new Bitmap(imagePath),
                        new Size(Canvas.Size.Width, Canvas.Size.Height));
                    AfterCanvas.Image = afterBitmap;

                }
            }

            InitializeOctree(ref Head1, false);
            InitializeOctree(ref Head2, true);
            RenderImage(DestinationHeightTrackBar.Value, false);
            RenderImage(DestinationHeightTrackBar.Value, true);
            
            Canvas.Refresh();
        }

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Canvas.Size.Width; i++)
            {
                for (int j = 0; j < Canvas.Size.Height; j++)
                {
                    ((Bitmap)Canvas.Image).SetPixel(i, j, Color.White);
                }
            }
            for (int i = 0; i < Canvas.Size.Width; i++)
            {
                if (i % 20 == 0)
                {
                    i += 20;
                    if (i >= Canvas.Size.Width) break;
                }

                for (int j = 20;j<Canvas.Size.Height-20; j++)
                {
                    Color c = ColorFromHSV((double)j / (double)Canvas.Size.Height * 360, (((double)i / (double)Canvas.Size.Width) * 16.0) / 16.0, V_Value);
                    ((Bitmap)Canvas.Image).SetPixel(i, j, c);
                }
            }

            PictureBox PictureBox1 = new PictureBox();
            PictureBox1.Image = ResizeImage(((Bitmap)Canvas.Image),
                new Size(AfterCanvas.Size.Width, AfterCanvas.Size.Height));
            imageBitmap = (Bitmap)PictureBox1.Image;

            PictureBox PictureBox2 = new PictureBox();
            PictureBox2.Image = ResizeImage(((Bitmap)Canvas.Image),
                new Size(AfterCanvas.Size.Width, AfterCanvas.Size.Height));
            afterBitmap = (Bitmap)PictureBox2.Image;
            AfterCanvas.Image = afterBitmap;

            PictureBox PictureBox3 = new PictureBox();
            PictureBox3.Image = ResizeImage(((Bitmap)Canvas.Image),
                new Size(AlongCanvas.Size.Width, AlongCanvas.Size.Height));
            alongBitmap = (Bitmap)PictureBox3.Image;
            AlongCanvas.Image = alongBitmap;

            InitializeOctree(ref Head1, false);
            InitializeOctree(ref Head2, true);
            RenderImage(DestinationHeightTrackBar.Value, false);
            RenderImage(DestinationHeightTrackBar.Value, true);

            Canvas.Refresh();
            AfterCanvas.Refresh();
            AlongCanvas.Refresh();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            V_Value = (double)trackBar2.Value / 10.0;

            for (int i = 0; i < Canvas.Size.Width; i++)
            {
                for (int j = 0; j < Canvas.Size.Height; j++)
                {
                    ((Bitmap)Canvas.Image).SetPixel(i, j, Color.White);
                }
            }
            for (int i = 0; i < Canvas.Size.Width; i++)
            {
                if (i % 20 == 0)
                {
                    i += 20;
                    if (i >= Canvas.Size.Width) break;
                }

                for (int j = 20; j < Canvas.Size.Height - 20; j++)
                {
                    Color c = ColorFromHSV((double)j / (double)Canvas.Size.Height * 360, (((double)i / (double)Canvas.Size.Width) * 16.0) / 16.0, V_Value);
                    ((Bitmap)Canvas.Image).SetPixel(i, j, c);
                }
            }

            PictureBox PictureBox1 = new PictureBox();
            PictureBox1.Image = ResizeImage(((Bitmap)Canvas.Image),
                new Size(AfterCanvas.Size.Width, AfterCanvas.Size.Height));
            imageBitmap = (Bitmap)PictureBox1.Image;

            PictureBox PictureBox2 = new PictureBox();
            PictureBox2.Image = ResizeImage(((Bitmap)Canvas.Image),
                new Size(AfterCanvas.Size.Width, AfterCanvas.Size.Height));
            afterBitmap = (Bitmap)PictureBox2.Image;
            AfterCanvas.Image = afterBitmap;

            PictureBox PictureBox3 = new PictureBox();
            PictureBox3.Image = ResizeImage(((Bitmap)Canvas.Image),
                new Size(AlongCanvas.Size.Width, AlongCanvas.Size.Height));
            alongBitmap = (Bitmap)PictureBox3.Image;
            AlongCanvas.Image = alongBitmap;

            InitializeOctree(ref Head1, false);
            InitializeOctree(ref Head2, true);
            RenderImage(DestinationHeightTrackBar.Value, false);
            RenderImage(DestinationHeightTrackBar.Value, true);

            Canvas.Refresh();
            AfterCanvas.Refresh();
            AlongCanvas.Refresh();
        }

        public static Image ResizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private void DestinationHeightTrackBar_Scroll_1(object sender, EventArgs e)
        {
            RenderImage(DestinationHeightTrackBar.Value, true);
            RenderImage(DestinationHeightTrackBar.Value, false);

            AfterCanvas.Refresh();
            AlongCanvas.Refresh();
        }

        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            K_Value = trackBar1.Value;
            InitializeOctree(ref Head2, true);
            RenderImage(DestinationHeightTrackBar.Value, true);
            Canvas.Refresh();
        }

        
    }
}