using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Triangulation
{
    class Draw
    {
        private int Width, Height;

        // конструктор
        public Draw(int BitmapWidth, int BitmapHeight)
        {
            Width = BitmapWidth;
            Height = BitmapHeight;
        }

        // рисуем точки
        public Bitmap DrawPoints(List<PointD> data, int Number)
        {
            Bitmap dblBuffer = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(dblBuffer);

            for (int i = 0; i < Number; i++)
            {
                RectangleF rect = new RectangleF(new PointF((float)data[i].x, (float)data[i].y), new SizeF(3.0F, 3.0F));
                g.FillEllipse(new SolidBrush(Color.Black), rect);
            }

            return dblBuffer;
        }

        // рисуем треугольники
        public Bitmap DrawTriangul(List<PointD> data, List<List<PointD>> delone_triangle, List<PointD_> dop_value, int Number, bool h, bool q)
        {
            Bitmap dblBuffer = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(dblBuffer);

            Pen pen_triangl = new Pen(Color.Red, 1);
            Pen pen_circle = new Pen(Color.DarkGray, 1);
            PointF[] triangl = new PointF[3];

            for (int i = 0; i < delone_triangle.Count; i++)
            {
                for (int k = 0; k < delone_triangle[i].Count; k++)
                {
                    PointF point = new PointF((float)delone_triangle[i][k].x, (float)delone_triangle[i][k].y);
                    triangl[k] = point;
                }
                g.DrawPolygon(pen_triangl, triangl);
            }

            for (int i = 0; i < delone_triangle.Count; i++)
            {
                if (h == true)
                {
                    RectangleF rect = new RectangleF(new PointF((float)dop_value[i].xc, (float)dop_value[i].yc), new SizeF(5.0F, 5.0F));
                    g.FillEllipse(new SolidBrush(Color.DarkBlue), rect);
                    g.DrawEllipse(pen_circle, (float)(dop_value[i].xc - dop_value[i].rad), (float)(dop_value[i].yc - dop_value[i].rad), (float)(2.0 * dop_value[i].rad), (float)(2.0 * dop_value[i].rad));
                }
            }

            if (q == true)
            {
                for (int w = 0; w < Number; w++)
                {
                    RectangleF rect = new RectangleF(new PointF((float)data[w].x, (float)data[w].y), new SizeF(3.0F, 3.0F));
                    g.FillEllipse(new SolidBrush(Color.Black), rect);
                }
            } 

            return dblBuffer;
        }
    }
}

