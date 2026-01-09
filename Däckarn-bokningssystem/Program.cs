using System;

namespace Däckarn_bokningssystem
{
    internal class Program
    {
        public static List<ServiceBooking> Bookings = new List<ServiceBooking>();
        static void Main(string[] args)
        {

            ServiceBooking newBooking = new ServiceBooking("Sebastian Kirjonen", "GLY342", ServiceType.Service, new DateTime(2026, 2, 15, 15, 00, 00));
            Bookings.Add(newBooking);
            newBooking = new ServiceBooking("Trulsa Viklund", "FWF18N", ServiceType.Däckbyte, new DateTime(2026, 2, 15, 13, 00, 00));
            Bookings.Add(newBooking);
            newBooking = new ServiceBooking("Trulsa Viklund", "FWF18N", ServiceType.Service, new DateTime(2026, 2, 15, 14, 00, 00));
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
        public static void PrintContactInfo() //metod som skriver ut kontaktuppgifter till kunden
        {
            Console.WriteLine("Däckarns AB\n" +
                "Telefon: 08-123 456 78\n" +
                "Email: Däckarns@info.se\n" +
                "Adress: Ryckepungsvägen 1, Falun");
        }

        public static string AboutUs()
        {
            string aboutUs = "Däckarns - Mer än bara däck \n\n" +

                "På Däckarns tror vi på det traditionella hantverket.\n" +
                "Vi är den lilla firman med den breda kompetensen. Vår historia sträcker sig tillbaka till 1953,\n" +
                "då vi bestämde oss för att skapa en verkstad där kunden slipper vända sig till tre olika ställen för att fixa bilen.\n\n" +

                "Vi är expeter på däck och fälj, men vi stannar inte där. Vår verkstad är utrustad för att hantera allt från\n" +
                "grundläggande motorservice till precisionarbetet som krävs vid mindre plåtknackningar och skadereparationer.\n\n" +

                "Vår filosofi är enkel: Vi lagar det som behövs, byter det som är slitet och ser till att du är nöjd innan du rullar ut.\n\n" +

                "Välkommen till till en verkstad där vi kan våra bilar - utanpå och inuti\n";
            return aboutUs;
        }

    }
}
