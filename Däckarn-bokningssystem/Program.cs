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
                        case 1:

                            Console.WriteLine("Vänligen ange ditt lösenord");
                            AdminMenu();


                            break;

                        case 2:

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
           

            static void AdminMenu()
                    
            {
                string Password = "";

                while (Password != "abc123")
                {
                     Password = Console.ReadLine();

                    if (Password == "abc123") //här ska lösenordet verifieras
                    {
                        Console.WriteLine("Välkommen administratör!");
                    }
                    else
                    {
                   
                        Console.WriteLine("Fel lösenord, försök igen"); 

                    }

                }

            }
              
        }
    }
}
