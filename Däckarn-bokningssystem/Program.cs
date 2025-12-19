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

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int userInput))
                {
                    switch (userInput)
                    {
                        case 1:

                            Console.WriteLine("Vänligen ange ditt lösenord");
                            Console.ReadLine(); //här ska lösenordet verifieras

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




                break; //lämnar loopen
            }

            


            

           
            
            
            
        }
    }
}
