using System;
using static System.Console;
class RentFinder
{
   static void Main()
   {
      string[] names = { "Jack", "Jill", "Bob", "Bill" };
      int[,] rents = { {400, 450, 510},
                         {500, 560, 630},
                         {625, 676, 740},
                         {1000, 1250, 1600} };
      int floor;
      int bedrooms;

        GetData(out floor, out bedrooms);

        DisplayRent(rents, ref floor, ref bedrooms, names);
        DisplayRent(rents, ref bedrooms, ref floor, true);
      
        ReadLine();

   }
    
    /// <summary>
    /// gets stuff
    /// </summary>
    /// <param name="fl"></param>
    /// <param name="br"></param>
    static void GetData(out int fl, out int br)
    {
        Write("Enter the floor on which you want to live ");
        fl = Convert.ToInt32(ReadLine());

        Write("Enter the number of bedrooms you need ");
        br = Convert.ToInt32(ReadLine());
    }
    /// <summary>
    /// displays stuff
    /// </summary>
    /// <param name="aptRents">contains array</param>
    /// <param name="fl">desired floor</param>
    /// <param name="br">desired num of rooms</param>
    /// <param name="n"></param>
    static void DisplayRent(int[,] aptRents, ref int fl, ref int br, string[] n)
    {
        WriteLine("The rent is {0}, {1}", aptRents[fl, br], n[2]);
        fl = 99;
        br = 99;
        aptRents[0, 0] = 999;

    }
    /// <summary>
    /// other display method
    /// </summary>
    /// <param name="aptRents"></param>
    /// <param name="fl"></param>
    /// <param name="br"></param>
    /// <param name="ok"></param>
    static void DisplayRent(int[,] aptRents, ref int fl, ref int br, bool ok)
    {
        if (ok)
        {
            WriteLine("Butt");
        }
        WriteLine("The rent is {0}, {1}", aptRents[fl, br]);
        fl = 99;
        br = 99;
        aptRents[0, 0] = 999;

    }
}
