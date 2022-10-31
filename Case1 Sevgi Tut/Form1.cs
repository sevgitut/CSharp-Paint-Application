using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Case1_Sevgi_Tut
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
            
            
            this.Width = 950;
            this.Height = 700;
            bm=new Bitmap(pic.Width, pic.Height);
            g=Graphics.FromImage(bm);
            g.Clear(Color.White);
            pic.Image = bm;
            
        }

        Bitmap bm;
        Graphics g;
        bool paint = false;
        Point px, py;
        Pen p = new Pen(Color.Black, 1);
        Pen erase =new Pen(Color.White,10);
        int index;
        int x, y, sX, sY,cX,cY;

        ColorDialog cd = new ColorDialog();
        Color new_color;

       

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
            py = e.Location;


            cX = e.X;
            cY = e.Y;

        }

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (paint)
            {
                if (index == 1)
                {
                    px = e.Location;
                    g.DrawLine(p, px, py);

                    //the last point where the mouse stopped is set as the new starting point
                    py = px;

                }
                if (index == 2)
                {
                    px = e.Location;
                    g.DrawLine(erase, px, py);
                    py = px;

                }
            }
            pic.Refresh();

            x = e.X;
            y = e.Y;
            sX = e.X - cX;
            sY = e.Y - cY;


        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            paint=false;

            sX = x - cX;
            sY= y - cY;   

            if (index == 3)
            {
                g.DrawEllipse(p, cX, cY, sX, sY);
            }

            if (index == 4)
            {
                g.DrawRectangle(p, cX, cY, sX, sY);
            }

            if (index == 5)
            {
                g.DrawLine(p, cX, cY, x, y);
            }
        }

        private void pic_paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (paint)
            {
                if (index == 3)
                {
                    g.DrawEllipse(p, cX, cY, sX, sY);
                }

                if (index == 4)
                {
                    g.DrawRectangle(p, cX, cY, sX, sY);
                }

                if (index == 5)
                {
                    g.DrawLine(p, cX, cY, x, y);
                }
            }

        }

        private void pic_Paint_1(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (paint)
            {
                if (index == 3)
                {
                    g.DrawEllipse(p, cX, cY, sX, sY);
                }

                if (index == 4)
                {
                    g.DrawRectangle(p, cX, cY, sX, sY);
                }

                if (index == 5)
                {
                    g.DrawLine(p, cX, cY, x, y);
                }
            }

        }

        private void btn_color_Click_1(object sender, EventArgs e)
        {
            cd.ShowDialog();
            new_color = cd.Color;
            btnActiveColor.BackColor = cd.Color;
            p.Color = cd.Color;


        }


        private void btn_clear_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            this.BackgroundImage = null;
            this.BackgroundImage = bm;
            index = 0;

        }

       



        private void btn_pencil_Click(object sender, EventArgs e)
        {
            index = 1;
        }


        private void eraser_Click(object sender, EventArgs e)
        {
            index = 2;
        }


        private void btn_circle_Click(object sender, EventArgs e)
        {
            index = 3;
        }

       

        private void btn_rectangle_Click(object sender, EventArgs e)
        {
            index = 4;
        }

       

        private void btn_line_Click(object sender, EventArgs e)
        {
            index = 5;
        }

        private void btnActiveColor_Click(object sender, EventArgs e)
        {

        }

        private void btnred_Click(object sender, EventArgs e)
        {
            btnActiveColor.BackColor = Color.Red;
            new_color = btnActiveColor.BackColor;
            p.Color=Color.Red;
        }

        private void btnblue_Click(object sender, EventArgs e)
        {
            btnActiveColor.BackColor = Color.Blue;
            new_color = btnActiveColor.BackColor;
            p.Color = Color.Blue;

        }

        private void btngreen_Click(object sender, EventArgs e)
        {
            btnActiveColor.BackColor = Color.Green;
            new_color = btnActiveColor.BackColor;
            p.Color = Color.Green;
        }

        private void btnyellow_Click(object sender, EventArgs e)
        {
            btnActiveColor.BackColor = Color.Yellow;
            new_color = btnActiveColor.BackColor;
            p.Color = Color.Yellow;

        }

        private void btnorange_Click(object sender, EventArgs e)
        {
            btnActiveColor.BackColor = Color.Orange;
            new_color = btnActiveColor.BackColor;
            p.Color = Color.Orange;

        }

        private void btnpurple_Click(object sender, EventArgs e)
        {
            btnActiveColor.BackColor = Color.Purple;
            new_color = btnActiveColor.BackColor;
            p.Color = Color.Purple;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            var sfd=new SaveFileDialog();
            sfd.Filter = "Image(*.jpg)|*.jpg|(*.*|*.*";
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                Bitmap btm = bm.Clone(new Rectangle(0, 0, this.Width, this.Height), bm.PixelFormat);
                btm.Save(sfd.FileName,ImageFormat.Jpeg);
            }

        }

    

        static Point set_Point(PictureBox pb, Point pt)
        {
            float pX = 1f * pb.Image.Width / pb.Width;
            float pY = 1f * pb.Image.Height / pb.Height;
            return new Point((int)(pt.X * pX), (int)(pt.Y * pY));
        }


        //Creating method to validate pixel old color before filling the shape to the new color
        private void validate(Bitmap bm, Stack<Point> sp, int x, int y, Color old_color, Color new_color)
        {
            Color cx = bm.GetPixel(x, y);
            if (cx == old_color)
            {
                sp.Push(new Point(x, y));
                bm.SetPixel(x, y, new_color);
            }
        }

        public void Fill(Bitmap bm, int x, int y, Color new_clr)
        {
            Color old_color = bm.GetPixel(x, y);
            Stack<Point> pixel = new Stack<Point>();
            pixel.Push(new Point(x, y));
            bm.SetPixel(x, y, new_clr);

            if (old_color == new_clr) return;

            //this method will take old pixel color and fill new color from clicked point to stack count > 0.

            while (pixel.Count > 0)
            {
                Point pt = (Point)pixel.Pop();
                if (pt.X > 0 && pt.Y > 0 && pt.X < bm.Width - 1 && pt.Y < bm.Height - 1)
                {
                    validate(bm, pixel, pt.X - 1, pt.Y, old_color, new_clr);
                    validate(bm, pixel, pt.X, pt.Y - 1, old_color, new_clr);
                    validate(bm, pixel, pt.X + 1, pt.Y, old_color, new_clr);
                    validate(bm, pixel, pt.X, pt.Y + 1, old_color, new_clr);
                }
            }
        }

        private void pic_MouseClick(object sender, MouseEventArgs e)
        {
            if (index == 7)
            {

                Point point = set_Point(pic, pt: e.Location);
                Fill(bm, point.X, point.Y, new_color);

            }
        }


        private void btn_fill_Click(object sender, EventArgs e)
        {
            index = 7;
        }



    }
}
