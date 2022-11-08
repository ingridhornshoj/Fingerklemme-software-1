using RPI;
using RPI.Controller;
using RPI.Display;
using RPI.Heart_Rate_Monitor;
using RPI.IO;

namespace Fingerklemme_software_1
{
    internal class Program
    {
       
        

        static void Main(string[] args)
        {
           
            Console.WriteLine("Hvad er dit ynglings tal? ");
            
            int tal = Convert.ToInt16(Console.ReadLine());
            RPi rpi = new RPi();
            SevenSeg sevenseg = new SevenSeg(rpi);
            Key key1 = new Key(rpi, Key.ID B1);
            sevenseg.Init_SevenSeg();
            sevenseg.Disp_SevenSeg(ConvertTobcd(tal));
            rpi.wait(10000);
            sevenseg.Close_SevenSeg();

        }

        internal static short ConvertTobcd(int input)
        {

            int bcd = 0;
            int shift = 0;

            while (input > 0)
            {
                bcd |= input % 10 << 4 * shift++;
                input /= 10; 
            }
            return (short)bcd;
        }
    }
}