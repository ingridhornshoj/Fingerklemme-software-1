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
            //1) Definerer og initialiserer objekter
            RPi rpi = new RPi();
            SevenSeg sevenseg = new SevenSeg(rpi);
            sevenseg.Init_SevenSeg();
            Key startbutton = new Key(rpi, Key.ID.P1);
            Key stopbutton = new Key(rpi, Key.ID.P2);

            //2) Kører i loop for evigt
            while (true)
            {

                //3) Sæt display til 000 og venter på start
                Console.WriteLine("Tryk på start for at måle din puls");
                while (!startbutton.isPressed())
                {
                    sevenseg.Close_SevenSeg();
                }

                Console.WriteLine("Startknap er trykket - starter måling");

                //4) Starter måling af puls
                Random rnd = new Random();
                int puls = rnd.Next(12, 220);

                //5) Vent på måling afsluttes
                while (!stopbutton.isPressed())
                {
                    rpi.wait(30);
                }

                Console.WriteLine("Stopknap er trykket - viser puls");
                Console.WriteLine("Tryk på stop for at nulstille");
                rpi.wait(250);


                //6) Vis puls på display indtil stopknap trykkes
                while (!stopbutton.isPressed())
                {
                    sevenseg.Disp_SevenSeg(ConvertTobcd(puls));
                }

                // Start forfra
                Console.WriteLine("Starter forfra\n");
            }
        }

        //7) Konverterer et tal til BCD
        internal static short ConvertTobcd(int input)
        {
            return ((short)(input / 100 * 16 * 16 + (input % 100) / 10 * 16 + input % 10));
        }
    }
}