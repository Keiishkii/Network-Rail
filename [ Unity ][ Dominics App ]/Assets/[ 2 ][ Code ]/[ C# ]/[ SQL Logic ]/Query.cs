using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UnityEngine;

namespace NetworkRail.SQL
{
	public static class Query
	{
        #region [ Logout User Login ]
        public struct QueryResultValidateLogin
        {
            public bool loginSuccess;
            public string message;
        }
                
        public static QueryResultValidateLogin ValidateLogin(string department, string username, string password)
        {
            QueryResultValidateLogin results = new QueryResultValidateLogin();
            SQLManager.RunOnConnection((connection) =>
            {
                String storeProcedure = "[dbo].[Validate_Login]";
                        
                using SqlCommand command = new SqlCommand(storeProcedure, connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add("@DEPARTMENT", SqlDbType.VarChar).Value = department;
                command.Parameters.Add("@USER_LOGIN", SqlDbType.VarChar).Value = username;
                command.Parameters.Add("@PASSWORD", SqlDbType.VarChar).Value = password;
                        
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    results.loginSuccess = (reader.GetInt32(0) == 1);
                    results.message = reader.GetString(1);
                };
            });
                    
            return results;
        }
        #endregion
        
        #region [ Logout User Login ]
        public static void LogoutUserLogin(string department, string username)
        {
            SQLManager.RunOnConnection((connection) =>
            {
                String storeProcedure = "[dbo].[Logout_User_Login]";
                        
                using SqlCommand command = new SqlCommand(storeProcedure, connection) { CommandType = CommandType.StoredProcedure };
                using SqlDataReader reader = command.ExecuteReader();
            });
        }
        #endregion
        
        
        
        #region [ User Portal ]
        public class QueryResultUserPortal
        {
            public string department;
            public string username;
            
            public string fullName;
            
            public bool managementPermission;
            public bool PTSHolderPermission;
            
            public string managementPermissionString;
            public string PTSHolderPermissionString;
        }
        
        public static QueryResultUserPortal UserPortal(string department, string username)
        {
            QueryResultUserPortal results = new QueryResultUserPortal();
            SQLManager.RunOnConnection((connection) =>
            {
                String storeProcedure = "[dbo].[User_Portal]";
                
                using SqlCommand command = new SqlCommand(storeProcedure, connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add("@DEPARTMENT", SqlDbType.VarChar).Value = department;
                command.Parameters.Add("@USER_LOGIN", SqlDbType.VarChar).Value = username;
                
                using SqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    results.department = reader.GetString(0);
                    results.username = reader.GetString(1);
                    results.fullName = reader.GetString(2);
                    
                    results.managementPermission = (reader.GetInt32(3) == 1);
                    results.PTSHolderPermission = (reader.GetInt32(4) == 1);
                    
                    results.managementPermissionString = reader.GetString(5);
                    results.PTSHolderPermissionString = reader.GetString(6);
                };
            });

            return results;
        }
        #endregion
        
        

        #region [ Select User Permissions ]
        public class QueryResultManageUserPermissions
        {
            public List<UserPermissionsData> userPermissionsData = new List<UserPermissionsData>();
        }

        public class UserPermissionsData
        {
            public string jobDepartment;
            public string jobControllerLogin;
            public string jobController;
            
            public string tempTableName;
            
            public string username;
            public string userFullName;
            public bool PTSPermissions;
            public bool managementPermissions;
        }

        public static QueryResultManageUserPermissions ManageUserPermissions(string department, string username)
        {
            QueryResultManageUserPermissions results = new QueryResultManageUserPermissions();
            SQLManager.RunOnConnection((connection) =>
            {
                String storeProcedure = "[dbo].[Manage_User_Permissions]";
                
                using SqlCommand command = new SqlCommand(storeProcedure, connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add("@DEPARTMENT", SqlDbType.VarChar).Value = department;
                command.Parameters.Add("@USER_LOGIN", SqlDbType.VarChar).Value = username;
                
                using SqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    results.userPermissionsData.Add(new UserPermissionsData()
                    {
                        jobDepartment = reader.GetString(0),
                        jobControllerLogin = reader.GetString(1),
                        jobController = reader.GetString(2),
                        tempTableName = reader.GetString(3),
                        username = reader.GetString(4),
                        userFullName = reader.GetString(5),
                        PTSPermissions = (reader.GetInt32(6) == 1),
                        managementPermissions = (reader.GetInt32(7) == 1)
                    });
                }
            });

            return results;
        }
        #endregion

        #region [ Get User Login Details Temp Table Name ]
        public static string GetManageUserTableName(string department, string username)
        {
            string tableName = "";
            SQLManager.RunOnConnection((connection) =>
            {
                String storeProcedure = "SELECT REPLACE(REPLACE(@USER_LOGIN + '_' + @DEPARTMENT + '_' + 'Manage_User_Permissions', ' ', ''), '-', '')";
                        
                using SqlCommand command = new SqlCommand(storeProcedure, connection) { CommandType = CommandType.Text };
                command.Parameters.Add("@DEPARTMENT", SqlDbType.VarChar).Value = department;
                command.Parameters.Add("@USER_LOGIN", SqlDbType.VarChar).Value = username;
                
                using SqlDataReader reader = command.ExecuteReader();
                        
                while (reader.Read()) tableName = reader.GetString(0);
            });
                    
            return tableName;
        }
        #endregion

        #region MyRegion
        // - - -
        public static void ApplyUserPermissionChanges(string department, string username)
        {
            SQLManager.RunOnConnection((connection) =>
            {
                String storeProcedure = "[dbo].[Alter_User_Permissions]";
                        
                using SqlCommand command = new SqlCommand(storeProcedure, connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add("@DEPARTMENT", SqlDbType.VarChar).Value = department;
                command.Parameters.Add("@USER_LOGIN", SqlDbType.VarChar).Value = username;
                
                command.ExecuteNonQuery();
            });
        }
        // - - -
        #endregion



        #region [ Select User Login Details ]
        public class QueryResultSelectUserLoginDetails
        {
            public string department;
            public string username;
                    
            public string tableName;
                    
            public string firstName;
            public string lastName;
            public string password;
            public string emailAddress;
                    
            public string county;
            public string region;
            public string district;
                    
            public float latitude;
            public float longitude;
                    
            public int travelDistance;
            public string jobAvailability;
        }
        
        public static QueryResultSelectUserLoginDetails SelectUserLoginDetails(string department, string username)
        {
            QueryResultSelectUserLoginDetails results = new QueryResultSelectUserLoginDetails();
            SQLManager.RunOnConnection((connection) =>
            {
                String storeProcedure = "[dbo].[Select_User_Login_Details]";
                
                using SqlCommand command = new SqlCommand(storeProcedure, connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add("@DEPARTMENT", SqlDbType.VarChar).Value = department;
                command.Parameters.Add("@USER_LOGIN", SqlDbType.VarChar).Value = username;
                
                using SqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    results.department = reader.GetString(0);
                    results.username = reader.GetString(1);

                    results.tableName = reader.GetString(2);
                    
                    results.firstName = reader.GetString(3);
                    results.lastName = reader.GetString(4);
                    results.password = reader.GetString(5);
                    results.emailAddress = reader.GetString(6);
                    
                    results.county = reader.GetString(7);
                    results.region = reader.GetString(8);
                    results.district = reader.GetString(9);


                    results.latitude = (reader.IsDBNull(10)) ? 0 : (float) reader.GetDecimal(10);
                    results.longitude = (reader.IsDBNull(11)) ? 0 : (float) reader.GetDecimal(11);

                    results.travelDistance = reader.GetInt32(12);
                    results.jobAvailability = reader.GetString(13);
                }
            });

            return results;
        }
        #endregion

        #region [ Get User Login Details Temp Table Name ]
        public static string GetLoginDetailsTempTableName(string department, string username)
        {
            string tableName = "";
            SQLManager.RunOnConnection((connection) =>
            {
                String storeProcedure = "SELECT REPLACE(REPLACE(@USER_LOGIN + '_' + @DEPARTMENT + '_' + 'User_Login', ' ', ''), '-', '')";
                        
                using SqlCommand command = new SqlCommand(storeProcedure, connection) { CommandType = CommandType.Text };
                command.Parameters.Add("@DEPARTMENT", SqlDbType.VarChar).Value = department;
                command.Parameters.Add("@USER_LOGIN", SqlDbType.VarChar).Value = username;
                
                using SqlDataReader reader = command.ExecuteReader();
                        
                while (reader.Read()) tableName = reader.GetString(0);
            });
                    
            return tableName;
        }
        #endregion
        
        #region [ List Departments Query ]
        // - - -
        public class QueryResultListJobDepartment
        {
            public List<string> departments = new List<string>();
        }
        
        public static QueryResultListJobDepartment ListJobDepartment()
        {
            QueryResultListJobDepartment results = new QueryResultListJobDepartment();
            SQLManager.RunOnConnection((connection) =>
            {
                String storeProcedure = "[dbo].[List_Job_Department]";
                        
                using SqlCommand command = new SqlCommand(storeProcedure, connection) { CommandType = CommandType.StoredProcedure };
                using SqlDataReader reader = command.ExecuteReader();
                        
                while (reader.Read()) results.departments.Add(reader.GetString(0));
            });
                    
            return results;
        }
        // - - -
        #endregion
        
        #region [ List Post Code County ]
        public class QueryResultListPostCodeCounty
        {
            public List<string> counties = new List<string>();
        }
        
        public static QueryResultListPostCodeCounty ListPostCodeCounty()
        {
            QueryResultListPostCodeCounty results = new QueryResultListPostCodeCounty();
            SQLManager.RunOnConnection((connection) =>
            {
                String storeProcedure = "[dbo].[List_Post_Code_County]";
                        
                using SqlCommand command = new SqlCommand(storeProcedure, connection) { CommandType = CommandType.StoredProcedure };
                using SqlDataReader reader = command.ExecuteReader();
                        
                while (reader.Read()) results.counties.Add(reader.GetString(0));
            });
                    
            return results;
        }
        #endregion
        
        #region [ List Post Code Region ]
        public class QueryResultListPostCodeRegion
        {
            public List<string> regions = new List<string>();
        }
        
        public static QueryResultListPostCodeRegion ListPostCodeRegion(string county)
        {
            QueryResultListPostCodeRegion results = new QueryResultListPostCodeRegion();
            SQLManager.RunOnConnection((connection) =>
            {
                String storeProcedure = "[dbo].[List_Post_Code_Region]";
                        
                using SqlCommand command = new SqlCommand(storeProcedure, connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add("@POST_CODE_COUNTY", SqlDbType.VarChar).Value = county;
                
                using SqlDataReader reader = command.ExecuteReader();
                        
                while (reader.Read()) results.regions.Add(reader.GetString(0));
            });
                    
            return results;
        }
        #endregion
        
        #region [ List Post Code District ]
        public class QueryResultListPostCodeDistrict
        {
            public List<string> districts = new List<string>();
        }
        
        public static QueryResultListPostCodeDistrict ListPostCodeDistrict(string region)
        {
            QueryResultListPostCodeDistrict results = new QueryResultListPostCodeDistrict();
            SQLManager.RunOnConnection((connection) =>
            {
                String storeProcedure = "[dbo].[List_Post_Code_District]";
                        
                using SqlCommand command = new SqlCommand(storeProcedure, connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add("@POST_CODE_REGION", SqlDbType.VarChar).Value = region;
                
                using SqlDataReader reader = command.ExecuteReader();
                        
                while (reader.Read()) results.districts.Add(reader.GetString(0));
            });
                    
            return results;
        }
        #endregion
        
        #region [ List Travel Distance ]
        public class QueryResultListTravelDistance
        {
            public List<int> travelDistances = new List<int>();
        }
        
        public static QueryResultListTravelDistance ListTravelDistance(string region)
        {
            QueryResultListTravelDistance results = new QueryResultListTravelDistance();
            SQLManager.RunOnConnection((connection) =>
            {
                String storeProcedure = "[dbo].[List_Travel_Distance]";
                        
                using SqlCommand command = new SqlCommand(storeProcedure, connection) { CommandType = CommandType.StoredProcedure };
                using SqlDataReader reader = command.ExecuteReader();
                        
                while (reader.Read()) results.travelDistances.Add(reader.GetInt32(0));
            });
                    
            return results;
        }
        #endregion
        
        #region [ List Availability ]
        public class QueryResultListAvailability
        {
            public List<string> availability = new List<string>();
        }
        
        public static QueryResultListAvailability ListAvailability()
        {
            QueryResultListAvailability results = new QueryResultListAvailability();
            SQLManager.RunOnConnection((connection) =>
            {
                String storeProcedure = "[dbo].[List_Availability]";
                        
                using SqlCommand command = new SqlCommand(storeProcedure, connection) { CommandType = CommandType.StoredProcedure };
                using SqlDataReader reader = command.ExecuteReader();
                        
                while (reader.Read()) results.availability.Add(reader.GetString(0));
            });
                    
            return results;
        }
        #endregion
        
        #region [ Alter User Login Details ]
        public static void ApplyUserLoginDetailsChanges(string department, string username)
        {
            SQLManager.RunOnConnection((connection) =>
            {
                String storeProcedure = "[dbo].[Alter_User_Login_Details]";
                        
                using SqlCommand command = new SqlCommand(storeProcedure, connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add("@DEPARTMENT", SqlDbType.VarChar).Value = department;
                command.Parameters.Add("@USER_LOGIN", SqlDbType.VarChar).Value = username;
                
                command.ExecuteNonQuery();
            });
        }
        #endregion



        #region [ Create User ]
        public static void CreateUser(string department, string username, string newUserUsername, string newUserPassword, bool ptsPermissions, bool managementPermissions)
        {
            SQLManager.RunOnConnection((connection) =>
            {
                String storeProcedure = "[dbo].[Create_Alter_Login]";
                        
                using SqlCommand command = new SqlCommand(storeProcedure, connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add("@DEPARTMENT", SqlDbType.VarChar).Value = department;
                command.Parameters.Add("@USER_LOGIN", SqlDbType.VarChar).Value = username;
                command.Parameters.Add("@NEW_USER_LOGIN", SqlDbType.VarChar).Value = newUserUsername;
                command.Parameters.Add("@PASSWORD", SqlDbType.VarChar).Value = newUserPassword;
                command.Parameters.Add("@LOGIN_STATUS", SqlDbType.VarChar).Value = (true) ? ("Available") : ("Not Available");
                command.Parameters.Add("@PTS_HOLDER", SqlDbType.VarChar).Value = (ptsPermissions) ? ("Available") : ("Not Available");
                command.Parameters.Add("@JOB_CONTROLLER", SqlDbType.VarChar).Value = (managementPermissions) ? ("Available") : ("Not Available");
                command.Parameters.Add("@NEW_LOGIN_FLAG", SqlDbType.Int).Value = 1;
                
                command.ExecuteNonQuery();
            });
        }
        #endregion
        
        
        
        #region [ Update User Details Property ]
        public static void RunNonQuery(string code)
        {
            SQLManager.RunOnConnection((connection) =>
            {
                using SqlCommand command = new SqlCommand(code, connection) { CommandType = CommandType.Text };
                
                command.ExecuteNonQuery();
            });
        }
        #endregion
    }
}
