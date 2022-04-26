using System;
using System.IO.Ports;
using System.Text.Json;
using System.IO;

namespace TestZadanie_1
{
    public class Weather
    {
        public DateTimeOffset Date { get; set; }
        public string NameD { get; set; }
        public string ws { get; set; }
        public string wd { get; set; }

        public Weather(string _data)
        {
            int indexOfChar = _data.IndexOf(",");
            ws = _data.Substring(1,indexOfChar - 1);
            wd = _data.Substring(indexOfChar, _data.Length - indexOfChar - 1);
            Date = DateTimeOffset.UtcNow;
            NameD = "WMT700";
        }
        
    }
    class Program
    {
        static bool _continue = true;
        static private SerialPort port = new SerialPort("COM1",
     9600, Parity.None, 8, StopBits.One);

         

    static void Main(string[] args)
        {
            string message;

            port.Open();
            while (_continue)
            {
                message = port.ReadLine();

                Console.WriteLine(message);

                Weather _data = new Weather(message);

                string json = JsonSerializer.Serialize(_data);

                File.AppendAllText(@"D:\temp.json", json + '\n');

                Console.WriteLine(json);

                
            }
        }
        
    }
        
}
