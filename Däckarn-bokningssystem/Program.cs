using System.ComponentModel;

namespace Däckarn_bokningssystem
{
    internal class Program
    {
        static List<string> Bookings = new List<string>();
        static void Main(string[] args)
        {
            //välkomst meny
            Console.WriteLine("Välkommen till Däckarns!\n" +
                "Vad vill du göra? Välj ditt alternativ & skriv in siffran för det valda aleternativet");

            Console.WriteLine("-------------------------------");
            Console.WriteLine("1. Logga in som administratör\n" +
                "2. Logga in som kund\n" +
                "3. Avsluta programmet");


            if (int.TryParse(Console.ReadLine(), out int userInput))
            {
                switch (userInput)
                {
                    case 1: //admin
                        Console.WriteLine("Vänligen ange ditt lösenord (Admin)\n" + //skriver ut lösenordet i programmet för enkelhetens skull, vid riktigt program bör detta inte göras.
                        "skriv \"AVBRYT\" för att gå tillbaka");

                        while (true)
                        {
                            string passwordInput = Console.ReadLine();
                            if(passwordInput == "Admin")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("felaktigt lösenord, försök igen");
                            }
                        }
                            AdminMenu();

                        break;

                        case 2: //user
                            UserMenu();
                            break;

                        case 3:
                            Environment.Exit(0); //avslutar programmet
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltigt val. Vänligen ange en siffra mellan 1 & 2");
                    return;
                }
           

        } //end of main

        static void AdminMenu()
        {
         
            Console.Clear();
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
                    foreach (var booking in Bookings)
                    {
                        Console.WriteLine("booking");
                    }
                    break;

                case 2: //lägg till bokning (boka tid)
                    Console.Clear();
                    Console.WriteLine("~~ Boka tid - Däckarns ~~\n");

                    BookTime();
                    break;
                case 3: //visa dagens bokningar
                    break;
                case 4: //visa lediga tider
                    PrintContactInfo();
                    break;
                case 5: //logga ut
                    break;
                case 6: //stäng ner programmet
                    Environment.Exit(0);
                    break;
            }
        }

        static void UserMenu()
        {
            Console.Clear();
            Console.WriteLine("~~ Välkommen till Däckarns ~~\n" +
            "Vad vill du göra? Välj ditt alternativ & skriv in siffran för det valda alternativet");

            Console.WriteLine("-------------------------------");
            Console.WriteLine("1. Boka tid\n" +
                "2. Mina bokade tider\n" +
                "3. Avboka tid\n" +
                "4. Kontakt uppgifter\n" +
                "5. Logga ut\n\n" +
                "6. Exit program\n");
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
                    break;
                case 3: //avboka tid
                    break;
                case 4: //skriva ut kontaktuppgifter
                    PrintContactInfo();
                    break;
                case 5: //logga ut
                    break;
                case 6: //stäng ner programmet
                    break;
            }
        }

        static void PrintContactInfo()
        {
            Console.WriteLine("Däckarns AB\n" +
                "Telefon: 08-123 456 78\n" +
                "Email: Däckarns@info.se");
        }

        static void BookTime() //metod för att boka en tjänst
        {

            //skriva in boknings namn
            string firstName = "", lastName = "";
            CollectName(ref firstName, ref lastName);


            //skriva in regnummer på fordnonet
            string regNr = "abc123";
            CollectPlate(ref regNr);


            //väljer service typ
            int serviceTypeNum = 0;
            ChooseServiceType(ref serviceTypeNum);

            //välja datum
            DateTime serviceDate = DateTime.Now;
            CollectDate(ref serviceDate);

            //lägga in tiden
            DateTime serviceTime = DateTime.Now;
            CollectTime(ref serviceTime, ref serviceDate);



            //skapar ett nytt boknings objekt
            ServiceBooking newBooking = new ServiceBooking(firstName + " " + lastName, regNr, serviceTypeNum, serviceTime);
            Bookings.Add(newBooking.ToString()); //lägger in den i listan med bokningar
            Console.WriteLine("Bokning skapad:\n" + newBooking.ToString()); //boknings bekräftelse till användaren
        }


        static void CollectName(ref string firstName, ref string lastName)
        {
            Console.WriteLine("Ange Förnamn:");
            firstName = Console.ReadLine();

            Console.WriteLine("\nAnge Efternamn:");
            lastName = Console.ReadLine();
        }

        static void CollectPlate(ref string regNr)
        {

            Console.WriteLine("\nAnge Registreringsnummer: \nExempel: " + regNr);
            regNr = Console.ReadLine();
        }

        static int ChooseServiceType(ref int serviceTypeNum)
        {
            Console.WriteLine("\nVälj tjänst:\n" +
            "1. Däckbyte\n" +
            "2. Däckförvaring\n" +
            "3. Hjulinställning");

            while (true)
            {
                bool serviceType = int.TryParse(Console.ReadLine(), out serviceTypeNum);

                if (serviceType && serviceTypeNum < 4 && serviceTypeNum > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("skriv 1, 2 eller 3.");
                }
            }

            return serviceTypeNum;
        }


        static DateTime CollectDate(ref DateTime serviceDate)
        {
            bool boolDate;
            do
            {
                Console.WriteLine("Vilken dag vill du boka?" +
                    "\n(\"åååå-mm-dd\")");
                boolDate = DateTime.TryParse(Console.ReadLine(), out serviceDate);

                if (!boolDate)
                {
                    Console.WriteLine("Vänligen skriv datumet i format visat ovanför");
                }
                else if (serviceDate < DateTime.Now.Date)
                {
                    Console.WriteLine("Vänligen skriv ett datum i framtiden.");
                }

            } while (!boolDate && serviceDate > DateTime.Now);

            return serviceDate;
        }

        
        static DateTime CollectTime(ref DateTime serviceTime, ref DateTime serviceDate)
        {
            bool boolTime;
            do
            {
                Console.WriteLine("ange tiden för bokning:\n(\"15:00\")");
                boolTime = DateTime.TryParse(Console.ReadLine(), out serviceTime);

                DateTime rounded = new DateTime(serviceDate.Year, serviceDate.Month, serviceDate.Day, serviceTime.Hour, 00, 00);


                if (!boolTime)
                {
                    Console.WriteLine("Ange tid i rätt format!");
                }
                else if (serviceTime.Minute != 0)
                {

                    Console.WriteLine("vi kan bara boka tider hela timmar, ska vi boka in dig {0}?  (Y/N):", rounded.ToString("yyyy MMMM dd - HH:mm"));
                    while (true)
                    {
                        string userInput = Console.ReadLine();
                        if (userInput.Trim().ToLower() == "y")
                        {
                            serviceTime = rounded;
                            break;
                        }
                        else if (userInput.Trim().ToLower() == "n")
                        {
                            Console.WriteLine("okej, välj en ny tid din krånglige fan");
                            boolTime = false;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("please write Y or N");
                        }
                    }
                }
                else if (serviceTime < DateTime.Now)
                {
                    Console.WriteLine("du kan inte boka bakåt i tiden");
                    boolTime = false;
                }
            } while (!boolTime);
            return serviceTime;
        }
    }

}
