using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Däckarn_bokningssystem
{
    internal class Program
    {
        static public List<ServiceBooking> Bookings = new List<ServiceBooking>();
        static void Main(string[] args)
        {

            ServiceBooking newBooking = new ServiceBooking("Sebastian Kirjonen", "GLY342", ServiceType.MotorService, new DateTime(2026, 2, 15, 15, 00, 00));
            Bookings.Add(newBooking);
            newBooking = new ServiceBooking("Trulsa Viklund", "FWF18N", ServiceType.Däckbyte, new DateTime(2026, 2, 15, 13, 00, 00));
            Bookings.Add(newBooking);
            newBooking = new ServiceBooking("Göran Göransson", "OLW08N", ServiceType.Hjulbalansering, new DateTime(2026, 2, 15, 11, 00, 00));
            Bookings.Add(newBooking);

            /*
             * 
             * hård kodade tider^^
             * 
             * 
             */

            Menu.StartMenu();


        } //end of main
        static public void PrintContactInfo() //metod som skriver ut kontaktuppgifter till kunden
        {
            Console.WriteLine("Däckarns AB\n" +
                "Telefon: 08-123 456 78\n" +
                "Email: Däckarns@info.se\n" +
                "Adress: Ryckepungsvägen 1, Falun");
        }

    }
}
