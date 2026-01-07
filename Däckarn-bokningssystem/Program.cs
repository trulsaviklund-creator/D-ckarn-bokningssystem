using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Däckarn_bokningssystem
{
    internal class Program
    {
        static List<ServiceBooking> Bookings = new List<ServiceBooking>();
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

            StartMenu();


        } //end of main

        static void StartMenu()
        {
            Console.WriteLine("Välkommen till Däckarns!\n" +
                "Vad vill du göra? Välj ditt alternativ & skriv in siffran för det valda aleternativet");

            Console.WriteLine("-------------------------------");
            Console.WriteLine("1. Logga in som administratör\n" +
                "2. Logga in som kund\n" +
                "3. Avsluta programmet");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int userInput))
                {
                    switch (userInput)
                    {
                        case 1: //admin
                            Console.WriteLine("\nVänligen ange ditt lösenord (Admin)\n" + //skriver ut lösenordet i programmet för enkelhetens skull, vid riktigt program bör detta inte göras.
                            "skriv \"AVBRYT\" för att gå tillbaka");

                            while (true)
                            {
                                string passwordInput = Console.ReadLine();
                                if (passwordInput == "Admin")
                                {
                                    break;
                                }
                                else if (passwordInput == "AVBRYT")
                                {
                                    Console.Clear();
                                    StartMenu();
                                }
                                else
                                {
                                    Console.WriteLine("Felaktigt lösenord, försök igen");
                                }
                            }
                            Console.Clear();
                            AdminMenu();

                            break;

                        case 2: //user
                            Console.Clear();
                            UserMenu();
                            break;

                        case 3:
                            Environment.Exit(0); //avslutar programmet
                            break;
                    }
                }
                else//om ogiltigt användar input
                {
                    Console.WriteLine("Ogiltigt val. Vänligen ange en siffra mellan 1 & 2");
                    continue;
                }
            }


        }

        static void AdminMenu() //meny alternativ för admin
        {
            while (true)
            {
                Console.WriteLine("~~ Administratörsmenyn - Däckarns ~~\n" +
                "Vad vill du göra? Välj ditt alternativ & skriv in siffran för det valda alternativet");

                Console.WriteLine("-------------------------------");
                Console.WriteLine("1. Visa alla bokningar\n" +
                    "2. Lägg till en bokning\n" +
                    "3. Visa dagens bokningar\n" +
                    "4. visa lediga tider\n" +
                    "5. Ta bort en bokning\n" +
                    "6. Logga ut\n\n" +
                    "7. Exit program\n");

                int userInput;
                while (true)
                {
                    if (!int.TryParse(Console.ReadLine(), out userInput))
                    {
                        Console.WriteLine("Ogiltigt val. Vänligen ange en siffra mellan 1 & 7");
                        continue;
                    }
                    break;
                }


                switch (userInput)
                {
                    case 1: //skriv ut alla bokningar
                        PrintBookedTimes();
                        break;

                    case 2: //lägg till bokning (boka tid)
                        Console.Clear();
                        Console.WriteLine("~~ Boka tid - Däckarns ~~\n");

                        BookTime();
                        break;
                    case 3: //visa dagens bokningar
                        PrintBookingsToday();
                        break;
                    case 4: //visa lediga tider
                        ListAvailableTimes();
                        break;
                    case 5: //ta bort en bokning
                        RemoveTime();
                        break;
                    case 6: //logga ut
                        Console.Clear();
                        StartMenu();
                        break;
                    case 7: //stäng ner programmet
                        Environment.Exit(0);
                        break;
                }
            }
        }

        static void UserMenu() //menyalternativ för kund
        {
            while (true)
            {
                Console.WriteLine("~~ Välkommen till Däckarns ~~\n" +
                "Vad vill du göra? Välj ditt alternativ & skriv in siffran för det valda alternativet");

                Console.WriteLine("-------------------------------\n");
                Console.WriteLine("1. Boka tid\n" +
                    "2. Mina bokade tider\n" +
                    "3. Kontakt uppgifter\n" +
                    "4. Logga ut\n\n" +
                    "5. Exit program\n");
                int userInput;
                while (true)
                {
                    if (!int.TryParse(Console.ReadLine(), out userInput))
                    {
                        Console.WriteLine("Ogiltigt val. Vänligen ange en siffra mellan 1 & 6");
                        continue;
                    }
                    break;
                }

                switch (userInput)
                {
                    case 1: //boka tid
                        Console.Clear();
                        Console.WriteLine("~~ Boka tid - Däckarns ~~\n");

                        BookTime();
                        break;

                    case 2: //visa användarens bokade tider
                        Console.Clear();
                        PrintCustomerTimes();
                        break;

                    case 3: //skriva ut kontaktuppgifter
                        Console.Clear();
                        PrintContactInfo();
                        Console.WriteLine();
                        break;

                    case 4: //logga ut
                        Console.Clear();
                        StartMenu();
                        break;

                    case 5: //stäng ner programmet
                        Environment.Exit(0);
                        break;
                }
            }
        }

        static void BookTime() //funktion för att boka en tjänst
        {

            //skriva in boknings namn
            string firstName = "", lastName = "";
            CollectName(ref firstName, ref lastName);


            //skriva in regnummer på fordnonet
            string regNr = "ABC123";
            CollectPlate(ref regNr);


            //väljer service typ
            ServiceType serviceType = new ServiceType();
            serviceType = (ServiceType)ChooseServiceType();

            //välja datum
            DateTime serviceDate = DateTime.Now;
            CollectDate(ref serviceDate);

            //lägga in tiden
            DateTime serviceTime = DateTime.Now;
            CollectTime(ref serviceTime, ref serviceDate);



            //skapar ett nytt boknings objekt
            ServiceBooking newBooking = new ServiceBooking(firstName + " " + lastName, regNr, serviceType, serviceTime);
            Bookings.Add(newBooking); //lägger in den i listan med bokningar
            Console.Clear();
            Console.WriteLine("För att avboka, skriv regnummer till våran mail: Däckarns@info.se\n");
            Console.WriteLine("Bokning skapad:\n{0}\n", newBooking.ToString()); //boknings bekräftelse till användaren

            Console.Write("den temporära kostnaden ligger på:");
            switch ((int)serviceType)
            {
                case 0:
                    Console.Write("1 500kr\n");
                    break;
                case 1:
                    Console.Write("500kr\n");
                    break;
                case 2:
                    Console.Write("2 000kr\n");
                    break;
                case 3:
                    Console.Write("10 000kr\n");
                    break;
                case 4:
                    Console.Write("2 000kr\n");
                    break;
            }
            Console.WriteLine();
        }

        static void RemoveTime()
        {
            bool isBooked = false;

            Console.Clear();
            while (!isBooked)
            {
                isBooked = false; //säkerställer att boolen är false, om användaren inte vill avboka den tid som skrivits ut.

                Console.WriteLine("Skriv in registrerings Numret på bilen som ska på service:");
                string userInput = Console.ReadLine().ToUpper();

                if (userInput == "AVBRYT") //vill man avbryta så kommer man till början av programmet.
                {
                    StartMenu();
                }

                foreach (var booking in Bookings)
                {
                    if (userInput == booking.RegNr) //om reg nummer finns i databasen
                    {
                        Console.WriteLine("är du säker på att du vill ta bort din bokning? (skriv \"JA\" eller \"NEJ\")\n{0}", booking.ToString());
                        string userConfirm = Console.ReadLine();
                        if(userConfirm.ToUpper() == "JA")
                        {
                            Bookings.RemoveAll(u => u.RegNr == userInput); //tar bort bokningen
                            Console.WriteLine("bokning med registrerings nummer {0} är nu borttagen.\n" +
                                "---------------------------\n", userInput);
                            isBooked = true;
                            break;
                        } else if (userConfirm.ToUpper() == "NEJ")
                        {
                            isBooked = true; //för att inte få ut texten att bokningen inte finns.
                            continue;
                        }
                    }
                }

                if (!isBooked) //om registreringsnummret inte blir hittat efter man kollat genom listan körs detta block.
                {
                    Console.WriteLine("En bokning med {0} finns inte med i databasen.\n", userInput);
                }

            }


        }

        static bool IsBooked(DateTime serviceTime) //metod för att kolla om en tid redan är bokad.
        {
            bool isBooked = true; //default värde är att tiden är bokad.
            int i = 0;

            foreach (var booking in Bookings)
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

        static bool IsBussinessHours(DateTime serviceTime)
        {
            bool isBussinessHours = true;

            DateTime start = new DateTime(serviceTime.Year, serviceTime.Month, serviceTime.Day, 07, 00, 00);
            DateTime end = new DateTime(serviceTime.Year, serviceTime.Month, serviceTime.Day, 15, 00, 00);


            if (serviceTime >= start && serviceTime <= end)
            {
                isBussinessHours = false;
            }

            return isBussinessHours;
        }

        static void CollectName(ref string firstName, ref string lastName) //metod som hämtar för- och efternamn inför bokning
        {
            Console.WriteLine("Ange Förnamn:");
            firstName = Console.ReadLine();

            Console.WriteLine("\nAnge Efternamn:");
            lastName = Console.ReadLine();
        }

        static void CollectPlate(ref string regNr)//metod som hämtar registrerings nummer inför bokning
        {

            Console.WriteLine("\nAnge Registreringsnummer: \nExempel: " + regNr);
            regNr = Console.ReadLine();
        }

        static int ChooseServiceType() //metod som tar emot kundens val av tjänst.
        {
            ServiceType serviceType = new ServiceType();

            Console.WriteLine("\nVälj tjänst:\n" +
            "1. Däckbyte\n" +
            "2. Däckförvaring\n" +
            "3. Hjulinställning\n" +
            "4. Motorservice\n" +
            "5. Plåtknackning");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int serviceTypeInput))
                {
                    if (serviceTypeInput >= 1 && serviceTypeInput <= 5)
                    {
                        switch (serviceTypeInput)
                        {
                            case 1:
                                serviceType = ServiceType.Däckbyte;
                                break;
                            case 2:
                                serviceType = ServiceType.Däckförvaring;
                                break;
                            case 3:
                                serviceType = ServiceType.Hjulbalansering;
                                break;
                            case 4:
                                serviceType = ServiceType.MotorService;
                                break;
                            case 5:
                                serviceType = ServiceType.PlåtKnackning;
                                break;

                        }
                        break;
                    }
                }
                Console.WriteLine("Felaktig input, försök igen");
            }
            return (int)serviceType;
        }

        static DateTime CollectDate(ref DateTime serviceDate) //metod som hämtar kundens val av datum.
        {
            Console.WriteLine("\nVilken dag vill du boka?" +
                "\n(\"åååå-mm-dd\")");

            bool boolDate = false;
            while (!boolDate || serviceDate < DateTime.Now)
            {
;
                boolDate = DateTime.TryParse(Console.ReadLine(), out serviceDate);

                if (!boolDate)
                {
                    Console.WriteLine("Vänligen skriv datumet i format visat ovanför");
                }
                else if (serviceDate < DateTime.Now)
                {
                    Console.WriteLine("Vänligen skriv ett datum i framtiden.");
                }

            }

            return serviceDate;
        }
        
        static DateTime CollectTime(ref DateTime serviceTime, ref DateTime serviceDate)//metod som hämtar kundens val av tid, och samlar dem till ett datum.
        {
            bool boolTime;
            do
            {
                Console.WriteLine("\nAnge start-tiden för bokning:\n(Öppettider: 07:00-15:00)");
                boolTime = DateTime.TryParse(Console.ReadLine(), out serviceTime);

                DateTime rounded = new DateTime(serviceDate.Year, serviceDate.Month, serviceDate.Day, serviceTime.Hour, 00, 00);


                if (!boolTime)
                {
                    Console.WriteLine("Ange tid i rätt format!");
                }
                else if (IsBussinessHours(serviceTime))
                {
                    Console.WriteLine("Du kan endast boka tid 07:00-15:00");
                    boolTime = false;
                }
                else if (serviceTime.Minute != 0)
                {

                    Console.WriteLine("Vi kan bara boka tider hela timmar, ska vi boka in dig {0}?  (ja/nej):", rounded.ToString("yyyy MMMM dd - HH:mm"));
                    while (true)
                    {
                        string userInput = Console.ReadLine();
                        if (userInput.Trim().ToLower() == "ja")
                        {
                            serviceTime = rounded;
                            break;
                        }
                        else if (userInput.Trim().ToLower() == "nej")
                        {
                            Console.WriteLine("Välj en annan tid:");
                            boolTime = false;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Vänligen svara endast Ja eller Nej");
                        }

                    }
                }
                else if (serviceTime < serviceTime.Date) //använder .Date i detta sammahang ifall programmet rättas senare på dagen.. annars bör DateTime.Now användas.
                {
                    Console.WriteLine("Du kan inte boka bakåt i tiden");
                    boolTime = false;
                }
                else
                {
                    serviceTime = rounded;

                }



                if (IsBooked(serviceTime))
                {
                    Console.WriteLine("Tiden är redan bokad, var god välj en annan");
                    boolTime = false;
                }

            } while (!boolTime);
            return serviceTime;
        }

        static void PrintBookedTimes() //metod för att loopa genom alla bokningar i databasen
        {
            Console.Clear();
            Console.WriteLine("Bokningar:\n");
            foreach (var booking in Bookings)
            {
                Console.WriteLine(booking.ToString());
            }
            Console.WriteLine("");
        }

        static void PrintCustomerTimes()
        {
            bool isBooked = false;

            while (!isBooked)
            {
                Console.WriteLine("Skriv in kund namn:\nSkriv \"ABRYT\" för att gå abryta");
                string userInput = Console.ReadLine();

                if (userInput == "AVBRYT") //vill man avbryta så kommer man till början av programmet.
                {
                    StartMenu();
                }

                foreach (var booking in Bookings)
                {
                    if (userInput == booking.RegNr) //om reg nummer finns i databasen
                    {
                        Bookings.RemoveAll(u => u.CustomerName.ToLower() == userInput.ToLower()); //ta bort
                        Console.WriteLine("bokning med registrerings nummer {0} är nu borttagen.\n" +
                            "---------------------------\n", userInput);

                        isBooked = true;
                        break;
                    }
                }

                if (!isBooked) //om registreringsnummret inte blir hittat efter man kollat genom listan körs detta block.
                {
                    Console.WriteLine("En bokning i {0} finns inte.\n", userInput);
                }

            }
        }

        static void PrintContactInfo() //metod som skriver ut kontaktuppgifter till kunden
        {
            Console.WriteLine("Däckarns AB\n" +
                "Telefon: 08-123 456 78\n" +
                "Email: Däckarns@info.se\n" +
                "Adress: Ryckepungsvägen 1, Falun");
        }

        static void PrintBookingsToday() //skriver ut alla tider som har en bokning på ett specifikt datum.
        {
            foreach(var booking in Bookings)
            {
                if (booking.ServiceTime.Date == DateTime.Now.Date)
                {
                    Console.WriteLine(booking);
                }
            }
        }

        static void ListAvailableTimes() //skriver ut alla tider lediga på en specifik dag.
        {
            Console.Clear();
            bool listingTimes = true;

            while (listingTimes)
            {

                Console.WriteLine("skriv in datumet du vill se från:\n(\"åååå-mm-dd\")");
                //tar in användarvärde i datum
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

                            foreach (var booking in Bookings) //loopar genom listan med bokningar för att se om någon har den tiden
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
                    listingTimes = false; //avslutar while loopen
                }
            }
            
        }

    }
}
