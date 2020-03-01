using static System.Console;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string name;
            WriteLine("Welcome to C#!");
            Write("Enter your name: ");
            name= ReadLine();
            WriteLine("Thank you, {0,-15} {1,10}",name,"Smith");
            WriteLine("Press enter to exit...");
            ReadLine();

        }


    }
}
