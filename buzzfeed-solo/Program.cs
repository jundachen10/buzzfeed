using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace buzzfeed_solo;

public sealed class Settings
{
    public required string KeyOne { get; set; } //had to change this to string from int
    public required bool KeyTwo { get; set; }
    public required NestedSettings KeyThree { get; set; } = null!;
}

public sealed class NestedSettings
{
    public required string Message { get; set; } = null!;
}
class Program
{
    static void Main(string[] args)
    {
        //step 1: connect to the remote database
        ////make sure to de-couple the remote server credentials from this code base using a seperate configuration file to store creds.

        //build a configuration object
        IConfigurationRoot config = new ConfigurationBuilder()
        .AddJsonFile("dbcredentials.json")
        .Build();

        // Get values from the config given their key and their target type.
        Settings? settings = config.GetRequiredSection("Settings").Get<Settings>();

        //test if json config file is being loaded and if fake connection string can be printed

        Console.WriteLine("hello world");
        Console.WriteLine($"{settings?.KeyOne}");
        Console.ReadLine();


        //step 2: prompt user for unique user ID to store into Users table to allow for progress and result saving
        //step 3: show user list of available quizzes to take from Quizzes table
        //step 4: ask user questions from selected quiz
        //step 5: score results (column=ResultID in UserResultScores table has the most tallies)
        //step 6: take all the scoring data stored in the UserResultScores Table and match to Results Title and quiz ID
        //step 7: print user's quiz result text
    }
}

