using System;

namespace Däckarn_bokningssystem;
internal class ScheduleLogics
{
    public static void PrintBookingsToday() //skriver ut alla tider som har en bokning på ett specifikt datum.
    {
        foreach (var booking in Program.Bookings)
        {
            if (booking.ServiceTime.Date == DateTime.Now.Date)
            {
                Console.WriteLine(booking);
            }
        }
    }

    public static void ListAvailableTimes() //skriver ut alla tider lediga på en specifik dag.
    {
        Console.Clear();
        bool listingTimes = true;

        while (listingTimes)
        {
            //tar in användarvärde i datum

            Console.WriteLine("skriv in datumet du vill se från:\n(\"åååå-mm-dd\")");
            DateTime firstDateInput = new DateTime();

            string firstDateUserInput = Console.ReadLine();
            Console.WriteLine("skriv in datumet du vill se till:\n(\"åååå-mm-dd\")");

            DateTime secondDateInput = new DateTime();
            string secondDateUserInput = Console.ReadLine();

            if (DateTime.TryParse(firstDateUserInput, out firstDateInput) && DateTime.TryParse(secondDateUserInput, out secondDateInput)) //försöker göra den till en datetime-värdetyp.
            {

                DateTime start = new DateTime(firstDateInput.Year, firstDateInput.Month, firstDateInput.Day, 7, 0, 0); //arbetsdagens start/slut tid, för att kunna ha varierande arbetstider på helgdagar osv.
                DateTime end = new DateTime(firstDateInput.Year, firstDateInput.Month, firstDateInput.Day, 16, 0, 0);
                TimeSpan hoursTimeSpan = end - start; //räknar ut tids skillnaden

                TimeSpan dateTimeSpan = secondDateInput.Date - firstDateInput.Date; //räknar ut datums skillnaden mellan de två datumen anvnändaren skrev in.


                Console.Clear();

                for (int dayCounter = 0; dayCounter < dateTimeSpan.Days + 1; dayCounter++) //loopar genom dagarna, just nu är det bara en dag som kollar.
                {

                    if (dayCounter > 0) start = start.AddDays(1); //lägger till en dag på datumet man kollar på om det är fler än 2 dagar.

                    Console.WriteLine("\nlediga tider för {0}:", start.ToString("d"));

                    //loopar igenom dagens timmar för att se vilka tider som är lediga.
                    for (int counter = 0; counter < hoursTimeSpan.Hours; counter++)
                    {
                        bool isOccupied = false;
                        DateTime timeToCheck = start.AddHours(counter); //gör en off-set på vilken tid den ska kolla

                        foreach (var booking in Program.Bookings) //loopar genom listan med bokningar för att se om någon har den tiden
                        {
                            if (booking.ServiceTime == timeToCheck)
                            {
                                isOccupied = true;
                                continue;
                            }
                        }


                        if (!isOccupied) //finns tiden med i listan så är den ledig, och skrivs då ut i konsollen.
                        {
                            Console.WriteLine(timeToCheck.ToString("HH:mm") + " - Ledig");
                        }
                        else if (isOccupied) //är tiden bokad så körs detta.
                        {
                            Console.WriteLine(timeToCheck.ToString("HH:mm") + " - Bokad");
                        }

                    }

                }
                Console.WriteLine(); //gör blank rad innan menyalternativen skrivs ut.
                listingTimes = false; //avslutar while loopen efter all utskrift är klar.
            }
        }

    }

    public static bool IsTimeBooked(DateTime serviceTime) //metod för att kolla om en tid redan är bokad.
    {
        bool isBooked = true; //default värde är att tiden är bokad.
        int i = 0;

        foreach (var booking in Program.Bookings)
        {
            if (booking.ServiceTime == serviceTime)
            {
                i++;
            }
        }

        if (i == 0)
        {
            isBooked = false;
        }

        return isBooked;
    }

    public static bool IsBussinessHours(DateTime serviceTime)
    {
        bool isBussinessHours = true;

        DateTime start = new DateTime(serviceTime.Year, serviceTime.Month, serviceTime.Day, 07, 00, 00);
        DateTime end = new DateTime(serviceTime.Year, serviceTime.Month, serviceTime.Day, 15, 00, 00);


        if (serviceTime >= start && serviceTime <= end)
        {
            isBussinessHours = false;
        }

        return isBussinessHours;
    } //metod som kollar om en tid är i arbetstimmar

    public static void PrintBookedTimes() //metod för att loopa genom alla bokningar i databasen
    {
        Console.Clear();
        Console.WriteLine("Bokningar:\n");
        foreach (var booking in Program.Bookings)
        {
            Console.WriteLine(booking.ToString());
        }
        Console.WriteLine("");
    }

    public static void PrintCustomerTimes()
    {
        bool isBooked = false;

        while (!isBooked)
        {
            Console.WriteLine("Skriv in ditt registreringsnummer:\nSkriv \"AVBRYT\" för att gå avbryta");
            string userInput = Console.ReadLine();

            if (userInput == "AVBRYT") //vill man avbryta så kommer man till början av programmet.
            {
                Console.Clear();
                Menu.StartMenu();
            }

            Console.Clear();
            foreach (var booking in Program.Bookings)
            {
                if (userInput.ToUpper() == booking.RegNr) //om namnet finns i databasen
                {
                    Console.WriteLine(booking + "\n");
                    isBooked = true;

                }
            }


            if (!isBooked) //om namnet inte blir hittat efter man kollat genom listan körs detta block.
            {
                Console.Clear();
                Console.WriteLine("En bokning för {0} finns inte.\n", userInput);
                continue;
            }

        }
    } //metod för att ta fram kundens bokade tider baserat på registrerings nummer.
}
