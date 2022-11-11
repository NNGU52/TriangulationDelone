using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Triangulation
{
    // структура для хранения координат узлов
    struct PointD
    {
        public double x;
        public double y;

        public PointD(double x_, double y_)
        {
            this.x = x_;
            this.y = y_;
        }
    }

    struct PointD_
    {
        public double xc;
        public double yc;
        public double rad;

        public PointD_(double xc_, double yc_, double rad_)
        {
            this.xc = xc_;
            this.yc = yc_;
            this.rad = rad_;
        }
    }

    class Calculate
    {
        public int Number;                                // кол-во узлов на сетке
        private int BWidth, BHeight;                      // размер области рисования                
        public List<PointD> data = new List<PointD>();    // лист для хранения точек
        public List<List<PointD>> delone_triangle = new List<List<PointD>>();
        public List<PointD_> dop_value = new List<PointD_>();


        // конструктор
        public Calculate(int PendulumLength, int BitmatWidth, int BitmatHeight)
        {
            Number = PendulumLength;

            BWidth = BitmatWidth;
            BHeight = BitmatHeight;
        }

        // функция для рандомизирования значений
        public void Random()
        {
            Random rnd = new Random();

            for (int i = 0; i < Number; i++)
            {
                double cor = 0 + rnd.NextDouble() * ((BWidth) - 0);
                double cor_ = 0 + rnd.NextDouble() * ((BHeight) - 0);
                data.Add(new PointD(cor, cor_));
            }
        }

        // посчитать центр окружности
        private double[] CalculateCenterCircle(double x1, double x2, double x3, double y1, double y2, double y3)
        {
            double xc = (0.5 * (((x2 * x2) - (x1 * x1) + (y2 * y2) - (y1 * y1)) * (y3 - y1) - ((x3 * x3) - (x1 * x1) + (y3 * y3) - (y1 * y1)) * (y2 - y1))) / (((x2 - x1) * (y3 - y1)) - ((x3 - x1) * (y2 - y1)));
            double yc = (0.5 * (((x3 * x3) - (x1 * x1) + (y3 * y3) - (y1 * y1)) * (x2 - x1) - ((x2 * x2) - (x1 * x1) + (y2 * y2) - (y1 * y1)) * (x3 - x1))) / (((x2 - x1) * (y3 - y1)) - ((x3 - x1) * (y2 - y1)));
            double[] mas = new double[2];
            mas[0] = xc;
            mas[1] = yc;

            return mas;
        }

        // посчитать радиус
        private double CalculateRadius(double[] mas, double x1, double y1)
        {
            double radius = Math.Sqrt(((mas[0] - x1) * (mas[0] - x1)) + ((mas[1] - y1) * (mas[1] - y1)));
            return radius;
        }

        // посчитать расстояние от центра до каждой из точек
        private bool CalculateDistance(int Number, double radius, double x1, double x2, double x3, double y1, double y2, double y3, List<PointD> data, double[] mas)
        {
            double distance = 0;
            for (int i = 0; i < Number; i++)
            {
                if (data[i].x == x1 && data[i].y == y1) continue;
                if (data[i].x == x2 && data[i].y == y2) continue;
                if (data[i].x == x3 && data[i].y == y3) continue;

                distance = Math.Sqrt((mas[0] - data[i].x) * (mas[0] - data[i].x) + (mas[1] - data[i].y) * (mas[1] - data[i].y));

                if (distance <= radius) return false;
            }

            return true;
        }

        public void Triangle()
        {
            delone_triangle.Clear();
            dop_value.Clear();

            int iterator = 0;
            for (int i = 0; i < Number; i++)
            {
                for (int n = 0; n < Number; n++)
                {
                    if (i == n) continue;

                    for (int k = 0; k < Number; k++)
                    {
                        if (k == i || k == n) continue;

                        double[] mas = CalculateCenterCircle(data[i].x, data[n].x, data[k].x, data[i].y, data[n].y, data[k].y);
                        double radius = CalculateRadius(mas, data[i].x, data[i].y);
                        bool check = CalculateDistance(Number, radius, data[i].x, data[n].x, data[k].x, data[i].y, data[n].y, data[k].y, data, mas);

                        if (check == true)
                        {
                            delone_triangle.Add(new List<PointD>());
                            delone_triangle[iterator].Add(new PointD(data[i].x, data[i].y));
                            delone_triangle[iterator].Add(new PointD(data[n].x, data[n].y));
                            delone_triangle[iterator].Add(new PointD(data[k].x, data[k].y));

                            dop_value.Add(new PointD_(mas[0], mas[1], radius));
                            

                            iterator++;
                        }
                    }
                }
            }
        }
    }
}
