using System.Data.SqlClient;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;
namespace buzzfeed_solo;

//these below classes are from the tutorial example link in readme
//we only need to use one key in the settings json to store our db creds so will need to remove these later.

public sealed class Settings //sealed so can't be inherited, and other cla
{
    public string ConnectionString { get; set; } //had to change this to string from int
}

class Program
{
    static void Main(string[] args)
    {
        //step 1: connect to the remote database.

        //step 1a: de-couple the remote server credentials from this repo by using a seperate configuration file to store the connection string.

        //loading the config file by building a configuration object first.
        IConfigurationRoot config = new ConfigurationBuilder()
        .AddJsonFile("dbcredentials.json")//file path .../bin/debug/net7.0/dbcredentials.json
        .Build();

        //get values from the config file given their key and their target type.

        Settings? settings = config.GetRequiredSection("Settings").Get<Settings>();

        //test if json config file is being loaded and if fake connection string can be printed.
        Console.WriteLine($"{settings?.ConnectionString}");
        Console.ReadLine();

        //CONNECTION BLOCK using the config file loaded above to pass the connection string.
        SqlConnection connection = new
            SqlConnection(@$"{settings?.ConnectionString}");

        //NOTE: the fake string from the separate json file is being loaded correctly and the json file is being ignored correctly in git ignore so it doesnt end up on repo.

        //step 2: prompt user for name, then retrieve a unique userID so we can save scores and results to the current user.
        Console.WriteLine("what is your name?");
        string name = Console.ReadLine();
        Console.WriteLine($"your name is {name}");

        //step 2a: need to code still: retrieve a unique row ID from the database to assign the current user.
        //step 2b: need to code still: give the user the option to not continue with the program and exit.

        connection.Open();//open connection

        string sql = "";
        sql = $"INSERT INTO Users (Name) VALUES ('{name}');";

        //write user input into User's table
        SqlCommand command = new SqlCommand(sql, connection);
        command.ExecuteNonQuery();

        connection.Close();//connection close

        //test to see if user input (name) was saved into database

        connection.Open();//connection open

        sql = "SELECT * FROM Users;";
        command = new SqlCommand(sql, connection);

        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine($"{reader["Name"]})");
        }

        Console.ReadLine();
        reader.Close();

        connection.Close();//connection close

        //step 3: show user list of available quizzes to take from Quizzes table (these quiz titles are preloaded into the database).
        //Start Quiz

        connection.Open();//connection open

        command = new SqlCommand("SELECT * FROM Quizzes;", connection);

        //Show the user a list of available quizes they can take from the QuizId and QuizTitle columns in the Quizzes table.
        reader = command.ExecuteReader();//reader open

        while (reader.Read())
        {
            Console.Write(reader["QuizId"] + ": ");
            Console.WriteLine(reader["QuizTitle"]);
        }

        Console.ReadLine();
        reader.Close();//reader close

        connection.Close();//connection close

        //step 4: have user be able to select a quiz to take by entering in the QuizId on line 90.

        //step 5: print out questions from the quiz that was selected by QuizId on line 90. The questions are located in the Questions table in the Title column. Questions (Title cloumn) are matched to quizses by QuizId column.

        //step 6: also print out the possible answer choices for each question located in the Answers table. Answers matched to questions by QuestionId column.

        //step 5: score results (column=ResultID in UserResultScores table has the most tallies)
        //step 6: take all the scoring data stored in the UserResultScores Table and match to Results Title and quiz ID
        //step 7: print user's quiz result text
    }
}

