using System.Collections.Generic;

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpPIDum.Helpers.RungeKutta
{


    public class ObbjectRegulation
    {

        public List<AperiodicElement> AperiodicElements = new List<AperiodicElement>();

        /// <summary>
        /// Текущее значение времени
        /// </summary>
        public double t { get; set; }

        /// <summary>
        /// Время шага (время приращения)
        /// </summary>
        public double dt { get; set; }
        
        public Dictionary<double, double> chart = new Dictionary<double, double>();


        //====

        public double MaxTimePeriod;

        public double inValue = new double();
        public double OutValue = new double();


        public void CalculationЕransitionProcess()
        {
            AperiodicElements[0].PreviousValue = inValue;

            for (t = 0; t < MaxTimePeriod; t += dt) //TODO += or =+
            {                
                
                for (int i = 0; i < AperiodicElements.Count; i++) //TODO or foreach and ref
                {
                    var res = StepCalculationAperiodicElement(
                        AperiodicElements[i].PreviousValue,
                        AperiodicElements[i].CurrentValue,
                        t,
                        dt,
                        AperiodicElements[i].T,
                        AperiodicElements[i].K
                    );
                    AperiodicElements[i].CurrentValue = res;

                    if (i < AperiodicElements.Count - 1)
                        AperiodicElements[i + 1].PreviousValue = res;
                }

                OutValue = AperiodicElements[AperiodicElements.Count - 1].CurrentValue;
                AperiodicElements[0].PreviousValue = OutValue;

                chart.Add(t, OutValue);
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">Значение на входе звена</param>
        /// <param name="y">Значение на выходе звена (результат предыдущего расчета)</param>
        /// <param name="t">Начальное значение времени шага</param>
        /// <param name="dt">Время шага (приращение времени)</param>
        /// <param name="T">Постоянная времени апериодического звена</param>
        /// <param name="K">Коэффициент усиления апериодического звена</param>
        /// <returns></returns>
        private double StepCalculationAperiodicElement(double x, double y, double t, double dt, double T, double K)
        {
            var k1 = K * x / T - y / T;
            var k2 = K * (x + dt / 2) / T - (y + dt * k1 / 2) / T;
            var k3 = K * (x + dt / 2) / T - (y + dt * k2 / 2) / T;
            var k4 = K * (x + dt) / T - (y + dt * k3) / T;

            return y + dt * (k1 + 2 * k2 + 2 * k3 + k4) / 6; ;
        }

    }

    public class AperiodicElement
    {

        public double T { get; set; }
        public double K { get; set; }

        public double PreviousValue { get; set; }
        public double CurrentValue { get; set; }

    }
    
}
