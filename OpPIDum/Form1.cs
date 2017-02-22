using OpPIDum.Helpers.RungeKutta;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpPIDum
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime; // повышение приоритета процесса
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
            //TODO sdsd
            objectControl.dt = Convert.ToDouble(textBox7.Text);
            objectControl.MaxTimePeriod = Convert.ToDouble(textBox8.Text);
            objectControl.inValue = Convert.ToDouble(textBox10.Text);

            //расчета переходного процесса
            objectControl.CalculationЕransitionProcess();

            //вывод результата
            chart1.Series[0].Points.Clear();
            foreach (var p in objectControl.chart) //TODO сделать через привязку данных а не через цикл
                chart1.Series[0].Points.AddXY(p.Key, p.Value);
            

        }

        private bool _exitThread;
        private Thread _workThread;

        private void button2_Click(object sender, EventArgs e)
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
            objectControl.inValue = Convert.ToDouble(textBox10.Text);

            

            backgroundWorker1.RunWorkerAsync(objectControl);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            
            Thread.CurrentThread.Priority = ThreadPriority.Highest; // повышение приоритета потока

            var myControl = e.Argument as ObbjectRegulation;

            double TimeCurrent = 0;
            double ValCurrent = 0;

            //настройка для расчета dt
            Stopwatch stopwatch = new Stopwatch();
            
            while (!backgroundWorker1.CancellationPending)
            {
                //var ms = (double)stopwatch.ElapsedMilliseconds / 1000;
                var ms = 1000; //ms 
                stopwatch.Start();
                myControl.CalculationStep(
                    TimeCurrent,
                    ms/1000,
                    out TimeCurrent,
                    out ValCurrent
                );
                stopwatch.Stop();
                var sleep = ms - (int)stopwatch.ElapsedMilliseconds;
                Thread.Sleep(sleep);


                backgroundWorker1.ReportProgress(0, new chartPoint() { T = TimeCurrent, V = ValCurrent });


            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            var newpoint = e.UserState as chartPoint;

            chart2.Series[0].Points.AddXY(newpoint.T, newpoint.V);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //MessageBox.Show(this, "Работа фонового процесса остановлена");
        }
    }
}
