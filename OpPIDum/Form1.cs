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
            //объявляем объект регулирования
            var objectControl = new Helpers.RungeKutta.ObbjectRegulation();

            //задаем параметры ОР - лучше сделать через IEnumerable Foreach
            objectControl.AperiodicElements.Add(    //1
                new Helpers.RungeKutta.AperiodicElement()
                {
                    T = Convert.ToDouble(textBox1.Text),
                    K = Convert.ToDouble(textBox2.Text),
                    PreviousValue = 0,
                }
                );
            objectControl.AperiodicElements.Add(    //2
                new Helpers.RungeKutta.AperiodicElement()
                {
                    T = Convert.ToDouble(textBox4.Text),
                    K = Convert.ToDouble(textBox3.Text),
                    PreviousValue = 0,
                }
                );
            objectControl.AperiodicElements.Add(    //3
                new Helpers.RungeKutta.AperiodicElement()
                {
                    T = Convert.ToDouble(textBox6.Text),
                    K = Convert.ToDouble(textBox5.Text),
                    PreviousValue = 0,
                }
                );

            objectControl.dt = Convert.ToDouble(textBox7.Text);
            objectControl.MaxTimePeriod = Convert.ToDouble(textBox8.Text);
            objectControl.inValue = 10; //возмущение

            //расчета переходного процесса
            objectControl.CalculationЕransitionProcess();

            //вывод результата
            chart1.Series[0].Points.Clear();
            foreach (var p in objectControl.chart) //TODO сделать через привязку данных а не через цикл
                chart1.Series[0].Points.AddXY(p.Key, p.Value);
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
