using System;
using System.Data.SQLite;
using System.Configuration;
using System.Collections.Generic;
using ConsoleTableExt;

namespace CodingTracker
{
    public class DatabaseManager
    {
        private static string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
        private static List<List<object>> items;
        private static List<object> id = new List<object>();
        private static List<object> date = new List<object>();
        private static List<object> duration = new List<object>();

        public void InitializeDatabase()
        {
            DatabaseManager databaseManager = new DatabaseManager();
            databaseManager.CreateTable(connectionString);
        }

        public void CreateTable(string connectionString)
        {
            using(var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var tableCommand = connection.CreateCommand();

                tableCommand.CommandText = @"CREATE TABLE IF NOT EXISTS CodingTime(
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            Date varchar(50),
                                            Duration varchar(50)
                                            )";
                tableCommand.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void AddToDatabase()
        {
            CodingTracker.StartSession();
            string date = CodingTracker.StartTime.ToString(@"d");

            Console.WriteLine("Type 'stop' to end your session and log it to the database.");


            string input = Console.ReadLine().ToLower();

            //Stops session, duration is saved to a string
            if (input == "stop") CodingTracker.EndSession();
            else if (input != "stop") Console.WriteLine("Invalid Input!");
            string duration = CodingTracker.Duration.ToString(@"h\:mm\:ss");

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var databaseCommand = connection.CreateCommand();
                databaseCommand.CommandText = $@"INSERT INTO CodingTime (Date, Duration)
                                                 VALUES ('{date}', '{duration}');";
                databaseCommand.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void RemoveFromDatabase()
        {
            Console.WriteLine("Which entry would you like to remove?\n");
            Console.WriteLine("Enter only the row number");

            ShowTable();

            string input = Program.GetUserInput();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var databaseCommand = connection.CreateCommand();
                databaseCommand.CommandText = $@"DELETE FROM CodingTime
                                                WHERE Id='{input}'";
                databaseCommand.ExecuteNonQuery();
                connection.Close();
            }

        }

        public void ShowTable()
        {
            Console.WriteLine();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM CodingTime", connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if(i == 0)
                                {
                                    id.Add(reader.GetValue(i).ToString());
                                }
                                if(i == 1)
                                {
                                    date.Add(reader.GetValue(i).ToString());
                                }
                                if(i == 2)
                                {
                                    duration.Add(reader.GetValue(i).ToString());
                                }
                                items = new List<List<object>>
                                {
                                    new List<object>{id},
                                    new List<object>{date},
                                    new List<object>{duration},
                                };
                            }
                        }
                    }
                }
                connection.Close();
                ConsoleTableBuilder.From(items).WithTitle("CodingTime").WithColumn("Id", "Date", "Duration").WithMinLength(new Dictionary<int, int> {{ 1, 25 },{ 2, 25 } }).ExportAndWriteLine();
            }
        }
    }
}
