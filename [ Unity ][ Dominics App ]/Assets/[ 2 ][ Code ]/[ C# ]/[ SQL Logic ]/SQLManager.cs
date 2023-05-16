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
            #region Establish Connection and Run Query
            // - - -
                private static void RunOnConnection(Action<SqlConnection> action)
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
            // - - -
            #endregion

            #region List Departments Query
            // - - -
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
            // - - -
            #endregion
            
            #region Login User
            // - - -
                public struct LoginQueryResults
                {
                    public bool resultLoginSuccess;
                    public string resultMessage;
                }
                
                public static LoginQueryResults QueryLogin(string department, string username, string password)
                {
                    LoginQueryResults results = new LoginQueryResults();
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
                            results.resultLoginSuccess = (reader.GetInt32(0) == 1);
                            results.resultMessage = reader.GetString(1);
                        };
                    });
                    
                    return results;
                }
            // - - -
            #endregion
            
            #region Logout User Query
            // - - -
                public static void QueryLogout(string department, string username)
                {
                    RunOnConnection((connection) =>
                    {
                        String storeProcedure = "[dbo].[Logout_User_Login]";
                        
                        using SqlCommand command = new SqlCommand(storeProcedure, connection) { CommandType = CommandType.StoredProcedure };
                        using SqlDataReader reader = command.ExecuteReader();
                    });
                }
            // - - -
            #endregion

            #region User Portal Query
            // - - -
                public struct UserPortalQueryResult
                {
                    public string resultDepartment;
                    public string resultUsername;
                    public string resultFullName;
                    
                    public bool resultJobControllerPermission;
                    public bool resultPTSHolderPermission;
                    
                    public string resultJobControllerPermissionString;
                    public string resultPTSHolderPermissionString;
                }
                
                public static UserPortalQueryResult QueryUserPortal(string department, string username)
                {
                    UserPortalQueryResult results = new UserPortalQueryResult();
                    RunOnConnection((connection) =>
                    {
                        String storeProcedure = "[dbo].[User_Portal]";
                        
                        using SqlCommand command = new SqlCommand(storeProcedure, connection) { CommandType = CommandType.StoredProcedure };
                        command.Parameters.Add("@DEPARTMENT", SqlDbType.VarChar).Value = department;
                        command.Parameters.Add("@USER_LOGIN", SqlDbType.VarChar).Value = username;
                        
                        using SqlDataReader reader = command.ExecuteReader();
                        
                        while (reader.Read())
                        {
                            results.resultDepartment = reader.GetString(0);
                            results.resultUsername = reader.GetString(1);
                            results.resultFullName = reader.GetString(2);
                            
                            results.resultJobControllerPermission = (reader.GetInt32(3) == 1);
                            results.resultPTSHolderPermission = (reader.GetInt32(4) == 1);
                            
                            results.resultJobControllerPermissionString = reader.GetString(5);
                            results.resultPTSHolderPermissionString = reader.GetString(6);
                        };
                    });

                    return results;
                }
            // - - -
            #endregion
        }
    }
}