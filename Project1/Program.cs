using System;
using static System.Console;
using System.IO;

namespace Project1
{
/* This program reads in baseball player names from a file.
 * Stores user input for player stats
 * Creates a report including calculated batting average
 */
    class Program
    {
        static void Main(string[] args)
        {
            string[] playerNames = new string[12];
            int[,] playerStats = new int[12, 2];
            double[] playerAverage = new double[12];
            string option;
            Init(playerNames, playerStats, playerAverage);
            Boolean exit = false;
            do
            {
                do
                {
                    option = MenuOptions().Trim();
                } 
                while (!ValidOption(option));
                switch (option)
                {
                    case "1":
                        OptionOne(playerNames, playerStats);
                        break;
                    case "2":
                        OptionTwo(playerNames, playerStats, playerAverage);
                        break;
                }
            }
            while (!exit);
            Write("Press enter to close program.");
            Read();
        }

        public static void Init(string[] pnArr, int[,] stats, double[] avg)
        {
            LoadArrays(pnArr, stats, avg);
        }

        public static void LoadArrays(string[] pnArr, int[,] stats, double[] avg)
        {
            string recordIn;
            const string FILENAME = "PlayerNames.dat";
            FileStream inFile = new FileStream(FILENAME,
            FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);
            recordIn = reader.ReadLine();
            for (int i = 0; i < pnArr.Length; i++)//load player names, initialize stats and average array
            {
                pnArr[i] = recordIn;
                recordIn = reader.ReadLine();
                stats[i,0] = 0;
                stats[i,1] = 0;
                avg[i] = 0;
            }
            reader.Close();
            inFile.Close();
        }

        /// <summary>
        /// Displays program menu
        /// </summary>
        /// <returns>User input</returns>
        public static string MenuOptions()
        {
            WriteLine("Baseball Stats Program\n" +
                "\nOption 1: Enter player stats." +
                "\nOption 2: Display printout.");
            Write("Enter option: ");
            string input = ReadLine();
            WriteLine();
            return input;
        }
        /// <summary>
        /// validates user input
        /// </summary>
        /// <param name="opt">user input to be validated</param>
        /// <returns>true if valid</returns>
        public static Boolean ValidOption(string opt)
        {
            Boolean valid = opt.Equals("1") || opt.Equals("2");
            if (!valid)
            {
                Write("Invalid input. Press enter to try again.");
                ReadLine();
                WriteLine();
            }
            return valid;
        }
        /// <summary>
        /// Promts for use input in consol, validates and stores player stats in array.
        /// </summary>
        /// <param name="pnArr"></param>
        /// <param name="stats"></param>
        private static void OptionOne(string[] pnArr, int[,] stats)
        {
            string input;
            Boolean enterbats = true;
            Boolean exit;
            Boolean reEnter;
            int atBats=0;
            int hits;
            int pNum;
            do
            {
                exit = false;
                do
                {
                    Write("Enter player number 1-12: ");
                    input = ReadLine();
                }
                while (!ValidPlayer(input));

                do
                {
                    reEnter = true;
                    pNum = Convert.ToInt32(input);
                    if (enterbats)
                    {

                        atBats = EnterStat("at bats", pnArr, pNum);
                    }
                    hits = EnterStat("hits", pnArr, pNum);
                    if (hits > atBats)
                    {
                        WriteLine("\nPlayer hits can not be more than at bats.");
                        Write("Enter B to re-enter both stats. Or press enter to re-enter hits: ");
                        string rEnter = ReadLine();
                        rEnter = rEnter.ToUpper();
                        enterbats = rEnter.Equals("B");
                    }
                    else
                    {
                        enterbats = true;
                        reEnter = false;
                    }
                }
                while (reEnter);
                stats[pNum - 1, 0] += atBats;//assign at bats to stats array
                stats[pNum - 1, 1] += hits;//assign hits to stats array
                WriteLine("Do you want to want to emter more player stats? Y/N: ");
                string cont = ReadLine();
                cont = cont.ToUpper();
                if (cont.Equals("N"))
                {
                    exit = true;
                    Write("Press enter to return to Menu:");
                    ReadLine();
                    WriteLine();
                }
                else
                {
                    if (cont.Equals("Y")){}
                    else
                    {
                        exit = true;
                        Write("Invalid entry. Press enter to return to Menu:");
                        ReadLine();
                        WriteLine();
                    }
                }
                
            }
            while (!exit);
        }
        /// <summary>
        /// Displays player stats report to console
        /// </summary>
        /// <param name="pnArr"></param>
        /// <param name="stats"></param>
        /// <param name="avg"></param>
        public static void OptionTwo(string[] pnArr, int[,] stats, double[] avg)
        {
            int i;
            Headings();
            for (i = 0; i < pnArr.Length; i++)//calculate average and store in array
            {
                CalcAvg(i,stats, avg);
                DetailLine(i, stats, avg, pnArr);
                
            }
            WriteLine();
            Write("Press enter to return to Menu:");
            ReadLine();
            WriteLine();
        }

        public static void Headings()
        {
            WriteLine("{0,-20}{1,7}{2,8}{3,10}\n", "Name", "At Bats", "Hits", "Average");
        }

        /// <summary>
        /// Calculates avarage of a single player and stores value in average array
        /// </summary>
        /// <param name="index">index of player in array</param>
        /// <param name="stats">array of player stats</param>
        /// <param name="avg">array of player averages</param>
        public static void CalcAvg(int index, int[,] stats, double[] avg)
        {
            if (stats[index,1] != 0)
            {
                avg[index] = Convert.ToDouble(stats[index, 1]) / Convert.ToDouble(stats[index, 0]);
            }
        }
        /// <summary>
        /// Writes A detail line to report per player
        /// </summary>
        /// <param name="index">player index in arrays</param>
        /// <param name="stats"></param>
        /// <param name="avg"></param>
        /// <param name="pnArr"></param>
        public static void DetailLine(int index, int[,] stats, double[] avg, string[] pnArr)
        {
            Write("{0,-20}", pnArr[index]);
            if (stats[index, 0] == 0 && stats[index, 1] == 0)
            {
                WriteLine("{0,25}", "No stats recorded      ");
            }
            else
            {
                string average = string.Format("{0:#.000}", avg[index]);
                WriteLine("{0,7}{1,8}{2,10}", stats[index, 0], stats[index, 1], average);
            }
        }

        /// <summary>
        /// Validates user input for player number
        /// </summary>
        /// <param name="playerNum">user input</param>
        /// <returns>True if valid input</returns>
        public static Boolean ValidPlayer(string playerNum)
        {
            int num;
            Boolean valid=false;
            try
            {
                num = Convert.ToInt32(playerNum);
                if (num > 0 && num <= 12)
                {
                    valid = true;
                }
                else
                {
                    WriteLine("Player number must be 1-12. Press enter to try again.");
                    ReadLine();
                }
            }
            catch (FormatException)
            {
                WriteLine("Player number must be numbers only. Press enter to try again.");
                ReadLine();
            }
            catch (OverflowException)
            {
                WriteLine("You and I both know thats not valid. Press enter to try again.");
                ReadLine();
            }
            return valid;
        }

        /// <summary>
        /// Promts for stat input.
        /// </summary>
        /// <param name="prompt">Included in prompt</param>
        /// <param name="pnArr"></param>
        /// <param name="i">player index on arrays</param>
        /// <returns>Validated player stat</returns>
        public static int EnterStat(string prompt,string[] pnArr, int i)
        {
            string stat;
            int num=0;
            Boolean valid = false;
            do
            {
                Write("Enter "+prompt+" for " + pnArr[i - 1] + ": ");
                stat = ReadLine();
                try
                {
                    num = Convert.ToInt32(stat);
                    valid = num >= 0;
                    if (!valid)
                    {
                        WriteLine("Stats must be 0 or greater. Press enter to try again.");
                        ReadLine();
                    }

                }
                catch (FormatException)
                {
                    WriteLine("Stats must be whole numbers. Press enter to try again.");
                    ReadLine();
                }
                catch (OverflowException)
                {
                    WriteLine("Too large of a number was entered. Press enter to try again.");
                    ReadLine();
                }
            }
            while (!valid);
            return num;
        }
    }
}
