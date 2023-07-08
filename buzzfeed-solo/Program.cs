using System.Data.SqlClient;
namespace buzzfeed_solo;

class Program
{
    static void Main(string[] args)
    {
        //step 1: connect to the remote database
        //step 2: prompt user for unique user ID to store into Users table to allow for progress and result saving
        //step 3: show user list of available quizzes to take from Quizzes table
        //step 4: ask user questions from selected quiz
        //step 5: score results (column=ResultID in UserResultScores table has the most tallies)
        //step 6: take all the scoring data stored in the UserResultScores Table and match to Results Title and quiz ID
        //step 7: print user's quiz result text
    }
}

