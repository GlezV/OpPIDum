using System.Runtime.InteropServices;

namespace OpPIDum.Helpers
{

    /// <summary>
    /// Класс-обертка для взаимодействия с модулями ввода-вывода компании ОВЕН используя интерфейс RS-485
    /// </summary>
    class OwenIO //TODO добавить описание всех методов класса и их параметров
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>       //Номер порта для COMX n=X-1
        /// <param name="speed"></param>   //Скорость порта -3 - 300кбит/с -2 - 600кбит/с -1 - 1200кбит/с 0 - 2400кбит/с
        ///                                //1 - 4800кбит/с 2 - 9600кбит/с 3 - 14400кбит/с 4 - 19200кбит/с 5 - 28800кбит/с
        ///                                //6 - 38800кбит/с 7 - 57600кбит/с 8 - 115200кбит/с 
        /// <param name="part"></param>    //Бит четности 0 - без бита четности 1 - чет 2 - нечет
        /// <param name="bits"></param>    //Бит данных 7 - 7 бит 8 - 8 бит
        /// <param name="stop"></param>    //Стоповые биты
        /// <param name="vid"></param>     //Управление передатчиком RS-485 0 - Сигнал RTS(АС-3 или другой полуавтоматический преобразователь)
        ///                                //                               1 - Автоматический преобразователь
        ///                                //                               2 - Сигнал DTR
        /// <returns></returns>
        [DllImportAttribute("owen_io.dll", EntryPoint = "OpenPort", CallingConvention = CallingConvention.StdCall)]
        public static extern int OpenPort(int n, int speed, int part, int bits, int stop, int vid);  //Открытие порта При успехе возвращает ERR_OR, при неудаче <0

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImportAttribute("owen_io.dll", EntryPoint = "ClosePort", CallingConvention = CallingConvention.StdCall)]
        public static extern int ClosePort();   //Закрытие порта При успехе возвращает ERR_OR, при неудаче <0

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adr"></param>          //Адрес устройства
        /// <param name="adr_type"></param>     //Длина адреса  0 - 8 бит 1 - 11 бит
        /// <param name="command"></param>      //Команда
        /// <param name="value"></param>        //Записываемое значение
        /// <param name="index"></param>        //Индекс
        /// <returns></returns>
        [DllImportAttribute("owen_io.dll", EntryPoint = "WriteFloat24", CallingConvention = CallingConvention.StdCall)]
        public static extern int WriteFloat24(int adr, int adr_type, System.IntPtr command, float value, int index);  //Запись значения с плавающей точкой в формате PIC

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adr"></param>          //Адрес устройства
        /// <param name="adr_type"></param>     //Длина адреса  0 - 8 бит 1 - 11 бит
        /// <param name="command"></param>      //Команда
        /// <param name="value"></param>        //Значение считанного параметра
        /// <param name="time"></param>         //Время измерения
        /// <param name="index"></param>        //Индекс
        /// <returns></returns>
        [DllImport("owen_io.dll", EntryPoint = "ReadIEEE32", CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadIEEE32(int adr, int adr_type, System.IntPtr command, ref float value, ref int time, int index); //Чтение значения с плавающей точкой в формате IEEE32

    }
}
