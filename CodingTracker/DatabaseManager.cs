using System;
using System.Data.SQLite;
using System.Configuration;

namespace CodingTracker
{
    public class DatabaseManager
    {
        private static string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
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
            string date = CodingTracker.StartTime.ToString();

            Console.WriteLine("Type 'stop' to end your session and log it to the database.");


            string input = Console.ReadLine().ToLower();

            //Stops session, duration is saved to a string
            if (input == "stop") CodingTracker.EndSession();
            else if (input != "stop") Console.WriteLine("Invalid Input!");
            string duration = CodingTracker.Duration.ToString();

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
    }
}
