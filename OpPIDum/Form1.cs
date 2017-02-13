using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpPIDum
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double t = 0;
            double dt = 0.01;
            double T = 10;
            double K = 1;
            double k1;
            double k2;
            double k3;
            double k4;
            double x = 10;
            double y = 0;
            while (t <= 15)
            {
                k1 = K * x / T - y / T;
                k2 = K * (x + dt / 2) / T - (y + dt * k1 / 2) / T;
                k3 = K * (x + dt / 2) / T - (y + dt * k2 / 2) / T;
                k4 = K * (x + dt) / T - (y + dt * k3) / T;
                y = y + dt * (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                t = t + dt;
                chart1.Series[0].Points.AddXY(t, y);
                //Thread.Sleep(10);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
