using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpPIDum.Helpers
{

    class RungeKutta
    {
        //TODO я так понял здесь описывается одно апериодическое звено, верно?
        //TODO полностью описать-прокомментировать класс
        //TODO вопрос со звеном транспортного запаздывания
        public static double Calculation(double t, double dt, double T, double K)
        {
            double res;
            double k1;
            double k2;
            double k3;
            double k4;
            double x = 10; //TODO я не понял что это за параметр
            double y = 0; //TODO а это что за параметр

            k1 = K * x / T - y / T;
            k2 = K * (x + dt / 2) / T - (y + dt * k1 / 2) / T;
            k3 = K * (x + dt / 2) / T - (y + dt * k2 / 2) / T;
            k4 = K * (x + dt) / T - (y + dt * k3) / T;
            res = y + dt * (k1 + 2 * k2 + 2 * k3 + k4) / 6;
            
            return res;
        }
    }
}
