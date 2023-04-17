using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UnityEngine;

namespace NetworkRail
{
    namespace SQL
    {
        public static class SQLManager
        {
            public static void RunOnConnection(Action<SqlConnection> action)
            {
                try
                {
                    using SqlConnection connection = new SqlConnection(new SqlConnectionStringBuilder
                    {
                        DataSource = "networkrail.database.windows.net", 
                        UserID = "Net_Rail", 
                        Password = "AEpgJ#K$LO=ZM^%#|", 
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
            
            
            
            public static List<string> GetListOfDepartments()
            {
                List<string> departments = new List<string>();
                RunOnConnection((connection) =>
                {
                    String storeProcedure = "EXECUTE Network_Rail.dbo.List_Job_Skill 'Off-Track'";
                    using SqlCommand command = new SqlCommand(storeProcedure, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) departments.Add(reader.GetString(0));
                });
                
                return departments;
            }
        }
    }
}