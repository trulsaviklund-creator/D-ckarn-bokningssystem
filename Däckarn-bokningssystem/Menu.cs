using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Däckarn_bokningssystem;
internal class Menu
{
    static public void StartMenu()
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
                    default:
                        Console.WriteLine("Ogiltigt val. Vänligen ange en siffra mellan 1 & 3");
                        continue;
                }
            }
            else//om ogiltigt användar input
            {
                Console.WriteLine("Ogiltigt val. Vänligen ange en siffra mellan 1 & 3");
                continue;
            }
        }


    }

    static public void AdminMenu() //meny alternativ för admin
    {
        while (true)
        {
            Console.WriteLine("~~ Administratörsmenyn - Däckarns ~~\n" +
            "Vad vill du göra? Välj ditt alternativ & skriv in siffran för det valda alternativet");

            Console.WriteLine("-------------------------------");
            Console.WriteLine("1. Visa alla bokningar\n" +
                "2. Lägg till en bokning\n" +
                "3. Visa dagens bokningar\n" +
                "4. Visa lediga tider\n" +
                "5. Ändra en bokning\n" +
                "6. Ta bort en bokning\n" +
                "7. Logga ut\n\n" +
                "8. Exit program\n");

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

                    ScheduleLogics.PrintBookedTimes();
                    break;

                    break;
                case 2: //lägg till bokning (boka tid)

                    Console.Clear();
                    Console.WriteLine("~~ Boka tid - Däckarns ~~\n");

                    BookingLogic.BookTime();

                    break;
                case 3: //visa dagens bokningar

                    ScheduleLogics.PrintBookingsToday();

                    break;
                case 4: //visa lediga tider

                    ScheduleLogics.ListAvailableTimes();

                    break;
                case 5: //ändra bokning

                    BookingLogic.EditBooking();

                    break;
                case 6: //ta bort en bokning

                    BookingLogic.RemoveTime();

                    break;
                case 7: //logga ut

                    Console.Clear();
                    StartMenu();

                    break;
                case 8: //stäng ner programmet

                    Environment.Exit(0);

                    break;


            }
        }
    }

    static public void UserMenu() //menyalternativ för kund
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
                    BookingLogic.BookTime();

                    BookingLogic.BookTime();
                    break;

                case 2: //visa användarens bokade tider
                    Console.Clear();

                    ScheduleLogics.PrintCustomerTimes();

                    break;

                case 3: //skriva ut kontaktuppgifter
                    Console.Clear();

                    Program.PrintContactInfo();
                    Console.WriteLine();

                    break;
                case 4: //logga ut

                case 4: //logga ut
                    Console.Clear();
                    StartMenu();

                    break;
                case 5: //stäng ner programmet

                case 5: //stäng ner programmet
                    Environment.Exit(0);

                    break;
            }
        }
    }
}
