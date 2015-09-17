using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESPConsole
{
    public class Program
    {

        public static SerialPort COMPort;

        public static void Main(String[] Arguments)
        {

            foreach (var _COMPort in SerialPort.GetPortNames())
                Console.WriteLine(_COMPort);

            COMPort = new SerialPort("COM14", 9600, Parity.None, 8, StopBits.One);
            COMPort.DataReceived += (s, e) => Console.Write(COMPort.ReadExisting());
            COMPort.ReadTimeout  = 4000;
            COMPort.WriteTimeout = 6000;

            try
            {
                COMPort.Open();
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }

            do
            {
                var CursorLeftPosition = Console.CursorLeft;
                var line = Console.ReadLine();
                Console.SetCursorPosition(CursorLeftPosition, Console.CursorTop -1);
                COMPort.WriteLine(line);
                Thread.Sleep(250);
            } while (true);

        }

    }

}
