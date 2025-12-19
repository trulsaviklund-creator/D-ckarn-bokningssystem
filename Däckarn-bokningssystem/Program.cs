using System.ComponentModel;

namespace Däckarn_bokningssystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
                        case 1: //admin menu
                            Console.WriteLine("Vänligen ange ditt lösenord");
                            string Password = "";

                            while (Password != "abc123")
                            {
                                Password = Console.ReadLine();
                                break; //lämnar loopen
                             }
                            AdminMenu();


                            AdminMenu();
                            break;

                        case 2: //user menu
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
            "Vad vill du göra? Välj ditt alternativ & skriv in siffran för det valda aleternativet");

            Console.WriteLine("-------------------------------");
            Console.WriteLine("1. Visa alla bokningar\n" +
                "2. Lägg till en bokning\n" +
                "3. Visa dagens bokningar\n" +
                "4. Visa alla bokningar\n" +
                "5. Ta bort en bokning\n" +
                "6. Logga ut\n\n" +
                "7. Exit program\n");

            Console.ReadLine();
        }
        static void UserMenu()
        {
            Console.Clear();
            Console.WriteLine("~~ Välkommen till Däckarns ~~\n" +
            "Vad vill du göra? Välj ditt alternativ & skriv in siffran för det valda aleternativet");

            Console.WriteLine("-------------------------------");
            Console.WriteLine("1. Boka tid\n" +
                "2. Mina bokade tider\n" +
                "3. Avboka tid\n" +
                "4. Kontakt uppgifter\n" +
                "5. Logga ut\n\n" +
                "6. Exit program\n");

                    if (Password == "abc123") //här ska lösenordet verifieras
                    {
                        Console.WriteLine("Välkommen administratör!");
                    }
                    else
                    {
                   
                        Console.WriteLine("Fel lösenord, försök igen"); 



            if (!int.TryParse(Console.ReadLine().ToString(), out int userInput))
            {
                Console.WriteLine("Ogiltigt val. Vänligen ange en siffra mellan 1 & 6");
            }


            Console.ReadLine();
        }

        static void ContactInfo()
        {
            Console.WriteLine("Däckarns AB\n" +
                "Telefon: 08-123 456 78\n" +
                "Email: Däckarns@info.se");
        }
    }
}
