using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace laba4
{
    public partial class Form1 : Form
    {

        static Pen pen1;
        static Graphics g;
        static Pen pen2;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pic1.Visible = false;
            pic2.Visible = false;
            pen1 = new Pen(Color.Pink, 1);
            pen2 = new Pen(Color.Purple, 1);
            g = CreateGraphics();
            g.Clear(Color.White);
            var point1 = new PointF(200, 200);
            var point2 = new PointF(500, 200);
            var point3 = new PointF(350, 400);
            g.DrawLine(pen1, point1, point2);
            g.DrawLine(pen1, point2, point3);
            g.DrawLine(pen1, point3, point1);
            Fractal(point1, point2, point3, 5);
            Fractal(point2, point3, point1, 5);
            Fractal(point3, point1, point2, 5);
        }

        static int Fractal(PointF p1, PointF p2, PointF p3, int iter)
        {
            if (iter > 0)
            {
                var p4 = new PointF((p2.X + 2 * p1.X) / 3, (p2.Y + 2 * p1.Y) / 3);
                var p5 = new PointF((2 * p2.X + p1.X) / 3, (p1.Y + 2 * p2.Y) / 3);
                var ps = new PointF((p2.X + p1.X) / 2, (p2.Y + p1.Y) / 2);
                var pn = new PointF((4 * ps.X - p3.X) / 3, (4 * ps.Y - p3.Y) / 3);
                g.DrawLine(pen1, p4, pn);
                g.DrawLine(pen1, p5, pn);
                g.DrawLine(pen2, p4, p5);
                Fractal(p4, pn, p5, iter - 1);
                Fractal(pn, p5, p4, iter - 1);
                Fractal(p1, p4, new PointF((2 * p1.X + p3.X) / 3, (2 * p1.Y + p3.Y) / 3), iter - 1);
                Fractal(p5, p2, new PointF((2 * p2.X + p3.X) / 3, (2 * p2.Y + p3.Y) / 3), iter - 1);
            }
            return iter;
        }

        public class Complex
        {
            public double a;
            public double b;

            public Complex(double a, double b)
            {
                this.a = a;
                this.b = b;
            }

            public void Square()
            {
                double temp = (a * a) - (b * b);
                b = 2.0 * a * b;
                a = temp;
            }

            public double Magnitude()
            {
                return Math.Sqrt((a * a) + (b * b));
            }

            public void Add(Complex c)
            {
                a += c.a;
                b += c.b;
            }
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            g = CreateGraphics();
            g.Clear(Color.White);
            pic1.Visible = true;
            pic2.Visible = false;
            Bitmap bm = new Bitmap(pic1.Width, pic1.Height);
            for (int x=0; x<pic1.Width; x++)
            {
                for(int y=0; y<pic1.Height; y++)
                {
                    double a = (double)(x - (pic1.Width / 2)) / (double)(pic1.Width / 4);
                    double b = (double)(y - (pic1.Height / 2)) / (double)(pic1.Height / 4);
                    Complex c = new Complex(a, b);
                    Complex z = new Complex(0, 0);
                    int it = 0;
                    do
                    {
                        it++;
                        z.Square();
                        z.Add(c);
                        if (z.Magnitude() > 2.0) break;
                    }
                    while (it<100);
                    bm.SetPixel(x, y, it < 100 ? Color.Purple : Color.Pink);
                }
            }
            pic1.Image = bm;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            g = CreateGraphics();
            g.Clear(Color.White);
            pic1.Visible = false;
            pic2.Visible = true;
            const int w = 600;
            const int h = 600;
            var bm = new Bitmap(w, h);
            var r = new Random();
            double x = 0;
            double y = 0;
            for (int count = 0; count < 100000; count++)
            {
                bm.SetPixel((int)(300 + 58 * x), (int)(58 * y), Color.Pink);
                int roll = r.Next(100);
                double xp = x;
                if (roll < 1)
                {
                    x = 0;
                    y = 0.16 * y;
                }
                else if (roll < 86)
                {
                    x = 0.85 * x + 0.04 * y;
                    y = -0.04 * xp + 0.85 * y + 1.6;
                }
                else if (roll < 93)
                {
                    x = 0.2 * x - 0.26 * y;
                    y = 0.23 * xp + 0.22 * y + 1.6;
                }
                else
                {
                    x = -0.15 * x + 0.28 * y;
                    y = 0.26 * xp + 0.24 * y + 0.44;
                }
            }
            pic2.Image = bm;
        }
    }
}
