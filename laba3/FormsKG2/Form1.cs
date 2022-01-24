using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace FormsKG2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void FloodFillzatravka(Bitmap bmp, Point pt, Color replacementColor) 
        {
            Stack<Point> pixels = new Stack<Point>();
            Color targetColor = bmp.GetPixel(pt.X, pt.Y);
            pixels.Push(pt);
            while (pixels.Count > 0)
            {
                Point a = pixels.Pop();
                if (a.X < bmp.Width && a.X > 0 &&
                        a.Y < bmp.Height && a.Y > 0)
                {
                    if (bmp.GetPixel(a.X, a.Y) == targetColor)
                    {
                        bmp.SetPixel(a.X, a.Y, replacementColor);
                        pixels.Push(new Point(a.X - 1, a.Y));
                        pixels.Push(new Point(a.X + 1, a.Y));
                        pixels.Push(new Point(a.X, a.Y - 1));
                        pixels.Push(new Point(a.X, a.Y + 1));
                    }
                }
            }
            return;
        }

        private void FloodFillSL(Bitmap bmp, Point pt, Color replacementColor) 
        {
            Color targetColor = bmp.GetPixel(pt.X, pt.Y);
            if (targetColor.ToArgb().Equals(replacementColor.ToArgb()))
            {
                return;
            }
            Stack<Point> pixels = new Stack<Point>();
            pixels.Push(pt);
            while (pixels.Count != 0)
            {
                Point temp = pixels.Pop();
                int y1 = temp.Y;
                while (y1 >= 0 && bmp.GetPixel(temp.X, y1) == targetColor)
                {
                    y1--;
                }
                y1++;
                bool spanLeft = false;
                bool spanRight = false;
                while (y1 < bmp.Height && bmp.GetPixel(temp.X, y1) == targetColor)
                {
                    bmp.SetPixel(temp.X, y1, replacementColor);

                    if (!spanLeft && temp.X > 0 && bmp.GetPixel(temp.X - 1, y1) == targetColor)
                    {
                        pixels.Push(new Point(temp.X - 1, y1));
                        spanLeft = true;
                    }
                    else if (spanLeft && temp.X - 1 == 0 && bmp.GetPixel(temp.X - 1, y1) != targetColor)
                    {
                        spanLeft = false;
                    }
                    if (!spanRight && temp.X < bmp.Width - 1 && bmp.GetPixel(temp.X + 1, y1) == targetColor)
                    {
                        pixels.Push(new Point(temp.X + 1, y1));
                        spanRight = true;
                    }
                    else if (spanRight && temp.X < bmp.Width - 1 && bmp.GetPixel(temp.X + 1, y1) != targetColor)
                    {
                        spanRight = false;
                    }
                    y1++;
                }
            }
        }

        private void FloodFill4xStack(Bitmap bmp, Point pt, Color replacementColor) 
        {
            Color targetColor = bmp.GetPixel(pt.X, pt.Y);
            Stack<Point> pixels = new Stack<Point>();
            pixels.Push(pt);
            if (targetColor.ToArgb().Equals(replacementColor.ToArgb()))
            {
                return;
            }
            int[] dx = new int[] { 0, 1, 0, -1 };
            int[] dy = new int[] { -1, 0, 1, 0 };
            while (pixels.Count != 0)
            {
                Point temp = pixels.Pop();
                bmp.SetPixel(temp.X, temp.Y, replacementColor);
                for (int i = 0; i < 4; i++)
                {
                    int nx = temp.X + dx[i];
                    int ny = temp.Y + dy[i];
                    if (nx >= 0 && nx < bmp.Width && ny >= 0 && ny < bmp.Height && bmp.GetPixel(nx, ny) == targetColor)
                    {
                        pt.X = nx;
                        pt.Y = ny;
                        pixels.Push(pt);
                    }
                }
            }
        }

        private void FloodFill8xStack(Bitmap bmp, Point pt, Color replacementColor) 
        {
            Color targetColor = bmp.GetPixel(pt.X, pt.Y);
            Stack<Point> pixels = new Stack<Point>();
            pixels.Push(pt);
            if (targetColor.ToArgb().Equals(replacementColor.ToArgb()))
            {
                return;
            }
            int[] dx = new int[] { 0, 1, 1, 1, 0, -1, -1, -1 }; 
            int[] dy = new int[] { -1, -1, 0, 1, 1, 1, 0, -1 }; 
            while (pixels.Count != 0)
            {
                Point temp = pixels.Pop();
                bmp.SetPixel(temp.X, temp.Y, replacementColor);
                for (int i = 0; i < 8; i++)
                {
                    int nx = temp.X + dx[i];
                    int ny = temp.Y + dy[i];
                    if (nx >= 0 && nx < bmp.Width && ny >= 0 && ny < bmp.Height && bmp.GetPixel(nx, ny) == targetColor)
                    {
                        pt.X = nx;
                        pt.Y = ny;
                        pixels.Push(pt);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            Bitmap bmp1 = new Bitmap(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\MyKV.png");
            Bitmap bmp2 = new Bitmap(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\MySecond.png");
            pictureBox1.Image = new Bitmap(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\MyKV.png");
            pictureBox3.Image = new Bitmap(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\MySecond.png");
            stopWatch.Start();
            FloodFillzatravka(bmp1, new Point(128, 128), Color.Yellow);
            stopWatch.Stop();
            TimeSpan ts1 = stopWatch.Elapsed;
            stopWatch.Start();
            FloodFillzatravka(bmp2, new Point(128, 128), Color.Yellow);
            stopWatch.Stop();
            TimeSpan ts2 = stopWatch.Elapsed;
            label4.Text = ts1.TotalMilliseconds + " ms";
            label5.Text = ts2.TotalMilliseconds + " ms";
            label4.Visible = true;
            label5.Visible = true;
            bmp1.Save(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\MyNew.png");
            bmp2.Save(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\MyNewSecond.png");
            pictureBox2.Image = bmp1;
            pictureBox4.Image = bmp2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            Bitmap bmp1 = new Bitmap(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\My.png");
            Bitmap bmp2 = new Bitmap(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\MySecond.png");
            pictureBox1.Image = new Bitmap(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\My.png");
            pictureBox3.Image = new Bitmap(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\MySecond.png");
            stopWatch.Start();
            FloodFillSL(bmp1, new Point(128, 128), Color.Red);
            stopWatch.Stop();
            TimeSpan ts1 = stopWatch.Elapsed;
            stopWatch.Start();
            FloodFillSL(bmp2, new Point(128, 128), Color.Red);
            stopWatch.Stop();
            TimeSpan ts2 = stopWatch.Elapsed;
            label4.Text = ts1.TotalMilliseconds + " ms";
            label5.Text = ts2.TotalMilliseconds + " ms";
            label4.Visible = true;
            label5.Visible = true;
            bmp1.Save(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\MyNew.png");
            bmp2.Save(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\MyNewSecond.png");
            pictureBox2.Image = bmp1;
            pictureBox4.Image = bmp2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            Bitmap bmp1 = new Bitmap(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\MyKV.png");
            Bitmap bmp2 = new Bitmap(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\MySecond.png");
            pictureBox1.Image = new Bitmap(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\MyKV.png");
            pictureBox3.Image = new Bitmap(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\MySecond.png");
            stopWatch.Start();
            FloodFill4xStack(bmp1, new Point(128, 128), Color.Pink);
            stopWatch.Stop();
            TimeSpan ts1 = stopWatch.Elapsed;
            stopWatch.Start();
            FloodFill4xStack(bmp2, new Point(128, 128), Color.Pink);
            stopWatch.Stop();
            TimeSpan ts2 = stopWatch.Elapsed;
            label4.Text = ts1.TotalMilliseconds + " ms";
            label5.Text = ts2.TotalMilliseconds + " ms";
            label4.Visible = true;
            label5.Visible = true;
            bmp1.Save(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\MyNew.png");
            bmp2.Save(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\MyNewSecond.png");
            pictureBox2.Image = bmp1;
            pictureBox4.Image = bmp2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            Bitmap bmp1 = new Bitmap(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\MyKV.png");
            Bitmap bmp2 = new Bitmap(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\MySecond.png");
            pictureBox1.Image = new Bitmap(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\MyKV.png");
            pictureBox3.Image = new Bitmap(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\MySecond.png");
            stopWatch.Start();
            FloodFill8xStack(bmp1, new Point(128, 128), Color.Gray);
            stopWatch.Stop();
            TimeSpan ts1 = stopWatch.Elapsed;
            stopWatch.Start();
            FloodFill8xStack(bmp2, new Point(128, 128), Color.Gray);
            stopWatch.Stop();
            TimeSpan ts2 = stopWatch.Elapsed;
            label4.Text = ts1.TotalMilliseconds + " ms";
            label5.Text = ts2.TotalMilliseconds + " ms";
            label4.Visible = true;
            label5.Visible = true;
            bmp1.Save(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\MyNew.png");
            bmp2.Save(@"C:\Users\roman\Desktop\unik\кг\laba3\FormsKG2\MyNewSecond.png");
            pictureBox2.Image = bmp1;
            pictureBox4.Image = bmp2;
        }
    }
}

