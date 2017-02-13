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
        /// <param name="n"></param>
        /// <param name="speed"></param>
        /// <param name="part"></param>
        /// <param name="bits"></param>
        /// <param name="stop"></param>
        /// <param name="vid"></param>
        /// <returns></returns>
        [DllImportAttribute("owen_io.dll", EntryPoint = "OpenPort", CallingConvention = CallingConvention.StdCall)]
        public static extern int OpenPort(int n, int speed, int part, int bits, int stop, int vid);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImportAttribute("owen_io.dll", EntryPoint = "ClosePort", CallingConvention = CallingConvention.StdCall)]
        public static extern int ClosePort();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adr"></param>
        /// <param name="adr_type"></param>
        /// <param name="command"></param>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        [DllImportAttribute("owen_io.dll", EntryPoint = "WriteFloat24", CallingConvention = CallingConvention.StdCall)]
        public static extern int WriteFloat24(int adr, int adr_type, System.IntPtr command, float value, int index);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adr"></param>
        /// <param name="adr_type"></param>
        /// <param name="command"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        [DllImport("owen_io.dll", EntryPoint = "ReadIEEE32", CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadIEEE32(int adr, int adr_type, System.IntPtr command, ref float value, ref int time, int index);

    }
}
