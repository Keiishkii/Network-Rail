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
            
            
            
            public static List<string> QueryListOfDepartments()
            {
                List<string> departments = new List<string>();
                RunOnConnection((connection) =>
                {
                    String storeProcedure = "[dbo].[List_Job_Department]";
                    
                    using SqlCommand command = new SqlCommand(storeProcedure, connection) { CommandType = CommandType.StoredProcedure };
                    using SqlDataReader reader = command.ExecuteReader();
                    
                    while (reader.Read()) departments.Add(reader.GetString(0));
                });
                
                return departments;
            }
            
            public static (bool, string) QueryLogin(string department, string username, string password)
            {
                int successfullyLoggedIn = 0;
                string message = "";
                
                RunOnConnection((connection) =>
                {
                    String storeProcedure = "[dbo].[Validate_Login]";
                    
                    using SqlCommand command = new SqlCommand(storeProcedure, connection) { CommandType = CommandType.StoredProcedure };
                    command.Parameters.Add("@DEPARTMENT", SqlDbType.VarChar).Value = department;
                    command.Parameters.Add("@USER_LOGIN", SqlDbType.VarChar).Value = username;
                    command.Parameters.Add("@PASSWORD", SqlDbType.VarChar).Value = password;
                    
                    using SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        successfullyLoggedIn = reader.GetInt32(0);
                        message = reader.GetString(1);
                    };
                });
                
                Debug.Log(successfullyLoggedIn);
                return ((successfullyLoggedIn == 1), message);
            }
        }
    }
}