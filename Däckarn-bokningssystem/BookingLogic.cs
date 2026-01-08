using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Däckarn_bokningssystem;
internal class BookingLogic
{
    static public void BookTime() //funktion för att boka en tjänst
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
        Program.Bookings.Add(newBooking); //lägger in den i listan med bokningar
        Console.Clear();
        Console.WriteLine("Bokning skapad:\n{0}\n", newBooking.ToString()); //boknings bekräftelse till användaren
        Console.WriteLine("För att avboka, skriv regnummer till våran mail: Däckarns@info.se\n");

        Console.Write("Den temporära kostnaden ligger på:");
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
    
    static public void CollectName(ref string firstName, ref string lastName) //metod som hämtar för- och efternamn inför bokning
    {
        bool validName = false;
        while (!validName)
        {

            Console.WriteLine("Ange Förnamn:");
            firstName = Console.ReadLine();

            Console.WriteLine("\nAnge Efternamn:");
            lastName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Vänligen ange ett giltigt för- och efternamn.\n");
                continue;
            }
            else if (firstName.Any(char.IsDigit) || lastName.Any(char.IsDigit))
            {
                Console.WriteLine("Namn kan inte innehålla siffror, försök igen.\n");
                continue;
            }

            validName = true;
        }
    }

    static public void CollectPlate(ref string regNr)//metod som hämtar registrerings nummer inför bokning
    {

        Console.WriteLine("\nAnge Registreringsnummer: \nExempel: " + regNr);
        while (true)
        {
            regNr = Console.ReadLine();

            if (regNr.Length > 7 || regNr.Length < 2)
            {
                Console.WriteLine("Registreringsnummret måste vara mellan 2 och 7 symboler");
                continue;
            }

            break;

        }
    }

    static public int ChooseServiceType() //metod som tar emot kundens val av tjänst.
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

    static public DateTime CollectDate(ref DateTime serviceDate) //metod som hämtar kundens val av datum.
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

    static public DateTime CollectTime(ref DateTime serviceTime, ref DateTime serviceDate)//metod som hämtar kundens val av tid, och samlar dem till ett datum.
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
            else if (ScheduleLogics.IsBussinessHours(serviceTime))
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



            if (ScheduleLogics.IsTimeBooked(serviceTime))
            {
                Console.WriteLine("Tiden är redan bokad, var god välj en annan");
                boolTime = false;
            }

        } while (!boolTime);
        return serviceTime;
    }

    static public void EditBooking() //funktion för att ändra en bokad tid
    {
        ServiceBooking selectedBooking = Program.Bookings.FirstOrDefault();
        bool isBooked = false;


        Console.Clear();
        while (!isBooked)
        {
            int i = 0;
            isBooked = false;

            Console.WriteLine("Skriv in regnummer för bilen på bokningen:\neller skriv \"AVBRYT\" för att avbryta");
            string userInput = Console.ReadLine().ToUpper();
            if (userInput == "AVBRYT")
            {
                Menu.AdminMenu();
            }


            foreach (var booking in Program.Bookings)
            {
                if (booking.RegNr == userInput)
                {
                    i++;
                }
            }

            if (i > 1)
            {
                Console.WriteLine("Vilken bokning vill du ändra på:");
                foreach (var booking in Program.Bookings)
                {

                    if (booking.RegNr == userInput)
                    {
                        foreach (var b in Program.Bookings) i = 0; i++;

                        Console.WriteLine("{0}. {1}", i, booking.ToString());
                    }
                }
                userInput = Console.ReadLine();
                //ServiceBooking selectedBooking = Bookings.FirstOrDefault(b => b.RegNr == userInput);

            }

            ServiceBooking selectedBooking = Program.Bookings.FirstOrDefault(b => b.RegNr == userInput);

            if (selectedBooking == null)
            {
                Console.Clear();
                Console.WriteLine("Vi hittar inte bokningen i detta registrerings nummer, försök igen\n---------------------");
                continue;
            }


            Console.WriteLine("Vad vill du ändra i bokningen:\n" +
                "1. Tid\n" +
                "2. Registrerings nummer\n" +
                "3. Boknings namn\n" +
                "4. Service typ");

            if (int.TryParse(Console.ReadLine(), out int userChoice))
            {
                switch (userChoice)
                {
                    case 1: //ändra tid
                        Console.Clear();

                        DateTime serviceDate = DateTime.Now;
                        CollectDate(ref serviceDate);

                        DateTime serviceTime = DateTime.Now;
                        CollectTime(ref serviceTime, ref serviceDate);


                        ServiceBooking newBooking = new ServiceBooking(selectedBooking.CustomerName, selectedBooking.RegNr, selectedBooking.ServiceType, serviceTime);
                        Program.Bookings.Add(newBooking); //lägger in den i listan med bokningar

                        Program.Bookings.RemoveAll(b => b.RegNr == selectedBooking.RegNr && b.ServiceTime == selectedBooking.ServiceTime); //tar bort den gamla bokningen

                        Console.Clear();
                        Console.WriteLine("Tiden på bokningen är nu ändrad!\n-------------------------");
                        Menu.AdminMenu();
                        break;

                    case 2: // ändra regnummer
                        CollectPlate(ref userInput);
                        string newRegNr = userInput;

                        newBooking = new ServiceBooking(selectedBooking.CustomerName, newRegNr, selectedBooking.ServiceType, selectedBooking.ServiceTime);
                        Program.Bookings.Add(newBooking); //lägger in den i listan med bokningar
                        Program.Bookings.RemoveAll(b => b.RegNr == selectedBooking.RegNr && b.ServiceTime == selectedBooking.ServiceTime); //tar bort den gamla bokningen

                        Console.Clear();
                        Console.WriteLine("Registrerings numret är nu ändrad!\n-------------------------");
                        Menu.AdminMenu();
                        break;


                    case 3: //ändra boknings namn

                        string firstName = "", lastName = "";
                        CollectName(ref firstName, ref lastName);

                        string newName = firstName + " " + lastName;
                        newBooking = new ServiceBooking(newName, selectedBooking.RegNr, selectedBooking.ServiceType, selectedBooking.ServiceTime);

                        Program.Bookings.Add(newBooking); //lägger in den i listan med bokningar
                        Program.Bookings.RemoveAll(b => b.RegNr == selectedBooking.RegNr && b.CustomerName == selectedBooking.CustomerName);

                        Console.Clear();
                        Console.WriteLine("Namnet på bokningen är nu ändrad!\n-------------------------");
                        Menu.AdminMenu();
                        break;

                    case 4: //ändra service typ
                        Console.WriteLine("Vilken servicetyp ska du ha:\n" +
                            "1. Däckbyte\n" +
                            "2. Hjulbalansernig\n" +
                            "3. Däckförvaring\n" +
                            "4. MotorService\n" +
                            "5. Plåtknackning");

                        if (int.TryParse(Console.ReadLine(), out userChoice))
                        {
                            switch (userChoice)
                            {
                                case 1: //däckbyte
                                        //gör en ny bokning med den nya servicetypen
                                    newBooking = new ServiceBooking(selectedBooking.CustomerName, selectedBooking.RegNr, ServiceType.Däckbyte, selectedBooking.ServiceTime);
                                    Program.Bookings.Add(newBooking); //lägger in den i listan med bokningar
                                    break;

                                case 2: //hjulbalansering
                                    newBooking = new ServiceBooking(selectedBooking.CustomerName, selectedBooking.RegNr, ServiceType.Hjulbalansering, selectedBooking.ServiceTime);
                                    Program.Bookings.Add(newBooking);
                                    break;
                                case 3: //däckförvaring
                                    newBooking = new ServiceBooking(selectedBooking.CustomerName, selectedBooking.RegNr, ServiceType.Däckförvaring, selectedBooking.ServiceTime);
                                    Program.Bookings.Add(newBooking);
                                    break;

                                case 4: //motorservice
                                    newBooking = new ServiceBooking(selectedBooking.CustomerName, selectedBooking.RegNr, ServiceType.MotorService, selectedBooking.ServiceTime);
                                    Program.Bookings.Add(newBooking);
                                    break;

                                case 5: //plåtknackning
                                    newBooking = new ServiceBooking(selectedBooking.CustomerName, selectedBooking.RegNr, ServiceType.PlåtKnackning, selectedBooking.ServiceTime);
                                    Program.Bookings.Add(newBooking);
                                    break;


                            }
                            try
                            {
                                Program.Bookings.RemoveAll(b => b.RegNr == selectedBooking.RegNr && b.ServiceType == selectedBooking.ServiceType); //tar bort bokningen med gamla servicetypen.
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Ett fel uppstod vid borttagning av gammal bokning: " + ex.Message);
                            }

                            Console.Clear();
                            Console.WriteLine("Servicetypen är nu ändrad!\n-------------------------");
                            Menu.AdminMenu();
                        }

                        break;
                }
            }
        }


    }
    
    static public void RemoveTime()
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
                Menu.StartMenu();
            }

            foreach (var booking in Program.Bookings)
            {
                if (userInput == booking.RegNr) //om reg nummer finns i databasen
                {
                    Console.WriteLine("är du säker på att du vill ta bort din bokning? (skriv \"JA\" eller \"NEJ\")\n{0}", booking.ToString());
                    string userConfirm = Console.ReadLine();
                    if (userConfirm.ToUpper() == "JA")
                    {
                        Program.Bookings.RemoveAll(u => u.RegNr == userInput); //tar bort bokningen
                        Console.WriteLine("bokning med registrerings nummer {0} är nu borttagen.\n" +
                            "---------------------------\n", userInput);
                        isBooked = true;
                        break;
                    }
                    else if (userConfirm.ToUpper() == "NEJ")
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


    } //funktion som tar bort en bokad tid
    
    static void CollectName(ref string firstName, ref string lastName) //metod som hämtar för- och efternamn inför bokning
    {
        bool validName = false;
        while (!validName)
        {

            Console.WriteLine("Ange Förnamn:");
            firstName = Console.ReadLine();

            Console.WriteLine("\nAnge Efternamn:");
            lastName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Vänligen ange ett giltigt för- och efternamn.\n");
                continue;
            }
            else if (firstName.Any(char.IsDigit) || lastName.Any(char.IsDigit))
            {
                Console.WriteLine("Namn kan inte innehålla siffror, försök igen.\n");
                continue;
            }

            validName = true;
        }
    }
    
    static void CollectPlate(ref string regNr)//metod som hämtar registrerings nummer inför bokning
    {

        Console.WriteLine("\nAnge Registreringsnummer: \nExempel: " + regNr);
        while (true)
        {
            regNr = Console.ReadLine();

            if (regNr.Length > 7 || regNr.Length < 2)
            {
                Console.WriteLine("Registreringsnummret måste vara mellan 2 och 7 symboler");
                continue;
            }

            break;

        }
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
            else if (ScheduleLogics.IsBussinessHours(serviceTime))
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



            if (ScheduleLogics.IsTimeBooked(serviceTime))
            {
                Console.WriteLine("Tiden är redan bokad, var god välj en annan");
                boolTime = false;
            }

        } while (!boolTime);
        return serviceTime;
    }
}
