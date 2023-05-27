using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UnityEngine;

namespace NetworkRail.SQL
{
    public static class SQLManager
    {
        #region Establish Connection and Run Query
        // - - -
            public static void RunOnConnection(Action<SqlConnection> action)
            {
                try
                {
                    using SqlConnection connection = new SqlConnection(new SqlConnectionStringBuilder
                    {
                        DataSource = "networkrail.database.windows.net", 
                        UserID = "Net_Rail", 
                        Password = "AEpgJ#p=ZM^%#|", 
                        InitialCatalog = "Network_Rail"
                    }.ConnectionString);
            
                    connection.Open();
                    action.Invoke(connection);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }            
        // - - -
        #endregion
    }
}