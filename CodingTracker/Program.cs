using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Configuration;
using ConsoleTableExt;

namespace CodingTracker
{
    internal class Program
    {
       
        static void Main(string[] args)
        {
            //Sets up database
            var databaseManager = new DatabaseManager();
            databaseManager.InitializeDatabase();

            MainMenu();

            string input = GetMenuInput();

            switch (input)
            {
                case "A":
                    databaseManager.AddToDatabase();
                    break;
                case "R":
                    databaseManager.RemoveFromDatabase();
                    break;
                    //case "U":
                    //    StartUpdate();
                    //    break;
                    case "D":
                    databaseManager.ShowTable();
                    string userInput = GetMenuInput();
                    userInput = null;
                    break;
                case "E":
                    Environment.Exit(0);
                    break;
            }

        }



        static void MainMenu()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Welcome to the Coding Tracker App!\n");
            Console.WriteLine("Choose from the menu below to use the app");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Type 'A' to add a habit\n");
            Console.WriteLine("Type 'R' to remove habit\n");
            Console.WriteLine("Type 'U' to update an entry\n");
            Console.WriteLine("Type 'D' to display all Habits\n");
            Console.WriteLine("Type 'E' to exit program");
        }

        public static string GetMenuInput()
        {
            string userInput = Console.ReadLine().ToUpper();
            return userInput;
        }

        public static string GetUserInput()
        {
            string userInput = Console.ReadLine();
            return userInput;
        }
    }
}
