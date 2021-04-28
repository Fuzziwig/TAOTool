using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace TAOTool
{
    public class DBAccess
    {
        //method for saving readings list to our database by using a stored procedure and passing parameters to it
        public void createReadings(List<Reading> readingList)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("tao1DB")))
            {
                connection.Execute("dbo.WriteReading @RDate, @MWH, @M3, @UserID", readingList);
            }
        }

        //method to check if we have a connection to our database by making a connection and a simple query to it
        public bool isConnected ()
        {
            try { 
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("tao1DB")))
                {
                    var result = connection.Query<int>($"SELECT 1");
                    return result.AsList().Contains(1);
                }
            } catch (SqlException ex)
            {
                return false;
            }
        }

        public int getReadingsRows()
        {
            try
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("tao1DB")))
                {
                    var result = connection.Query<int>($"SELECT COUNT(*) FROM Readings");
                    return result.AsList()[0];
                }
            }
            catch (SqlException ex)
            {
                return 0;
            }
        }
    }
}

