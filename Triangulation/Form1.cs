using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

namespace Triangulation
{
    public partial class Form1 : Form
    {
        private Draw draw;
        private Calculate calculate;
        private double time_work;
        bool check;
        Stopwatch st = new Stopwatch();

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1;   // выставляем интервал 1 миллисекунду
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            calculate.Triangle();
            pictureBox.Image = draw.DrawTriangul(calculate.data, calculate.delone_triangle, calculate.dop_value, calculate.Number, checkBox1.Checked, checkBox2.Checked);
            st.Stop();
            check = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            check = false;
            st.Start();
            timer1.Enabled = true;             
            calculate = new Calculate(Convert.ToInt32(textBoxNumber.Text), pictureBox.Width, pictureBox.Height);
            draw = new Draw(pictureBox.Width, pictureBox.Height);

            calculate.Random();
            pictureBox.Image = draw.DrawPoints(calculate.data, calculate.Number);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time_work = st.Elapsed.TotalSeconds;
            label3.Text = time_work.ToString();

            if (check)
            {
                timer1.Stop();
                st.Reset();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
