using System;
using System.Data;
using System.Data.SqlClient;
using UnityEditor;
using UnityEngine;

public class SQLTest : MonoBehaviour
{
    #region Custom Inspector
    // - - -
        [CustomEditor(typeof(SQLTest))]
        public class SQLTestInspector : Editor
        {
            #region Behaviour
            // - - -
                [SerializeField] [HideInInspector] private bool _showBaseInspector;
                private SQLTest _target;
            // - - -
            #endregion

            
            
            private void OnEnable() => _target = (SQLTest) target;
            public override void OnInspectorGUI()
            {
                if (Application.isPlaying && GUILayout.Button("Run Query")) _target.RunOnConnection(_target.RunTextBasedQuery);
                
                if (Application.isPlaying && GUILayout.Button("Run Stored Procedure Query")) _target.RunOnConnection(_target.RunStoredProcedureQuery);
                //if (Application.isPlaying && GUILayout.Button("Run Non Query Stored Procedure")) _target.RunOnConnection(_target.RunNonQueryStoredProcedure);
              
                _showBaseInspector = EditorGUILayout.Foldout(_showBaseInspector, "Base Inspector");
                if (_showBaseInspector) base.OnInspectorGUI();
            }
        }
    // - - - 
    #endregion





    private void RunOnConnection(Action<SqlConnection> action)
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


    
    private void RunTextBasedQuery(SqlConnection connection)
    {
        //string query = "SELECT Job_Department, User_Login, User_Password FROM [dbo].[User_Login_Lookup]";
        string query = "select Job_Task from [dbo].[##OffTrack_Skills]";
        using SqlCommand command = new SqlCommand(query, connection)
        {
            CommandType = CommandType.Text
        };
        
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read()) Debug.Log($"{reader.GetString(0)}");
        }
    }
    
    private void RunNonQueryStoredProcedure(SqlConnection connection)
    {
        String storeProcedure = "EXECUTE Network_Rail.dbo.List_Job_Skill 'Off-Track'";
        using SqlCommand command = new SqlCommand(storeProcedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.ExecuteNonQuery();
    }
    
    private void RunStoredProcedureQuery(SqlConnection connection)
    {
        String storeProcedure = "[dbo].[List_Job_Skill]";
        using SqlCommand command = new SqlCommand(storeProcedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.Add("@DEPARTMENT", SqlDbType.VarChar).Value = "Off-Track";



        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read()) Debug.Log($"{reader.GetString(0)}");
        }
    }
}
