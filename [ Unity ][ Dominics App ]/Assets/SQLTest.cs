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
                if (Application.isPlaying && GUILayout.Button("Run Query")) NetworkRail.SQL.SQLManager.RunOnConnection(_target.RunTextBasedQuery);
                
                if (Application.isPlaying && GUILayout.Button("Run Stored Procedure Query")) NetworkRail.SQL.SQLManager.RunOnConnection(_target.RunStoredProcedureQuery);
                if (Application.isPlaying && GUILayout.Button("Run Non Query Stored Procedure")) NetworkRail.SQL.SQLManager.RunOnConnection(_target.RunNonQueryStoredProcedure);
              
                _showBaseInspector = EditorGUILayout.Foldout(_showBaseInspector, "Base Inspector");
                if (_showBaseInspector) base.OnInspectorGUI();
            }
        }
    // - - - 
    #endregion



    

    
    private void RunTextBasedQuery(SqlConnection connection)
    {
        string query = "SELECT Job_Department, User_Login, User_Password FROM [dbo].[User_Login_Lookup]";
        using SqlCommand command = new SqlCommand(query, connection)
        {
            CommandType = CommandType.Text
        };
        
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read()) Debug.Log($"{reader.GetString(0)} {reader.GetString(1)}");
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
        String storeProcedure = "EXECUTE Network_Rail.dbo.List_Job_Skill 'Off-Track'";
        using SqlCommand command = new SqlCommand(storeProcedure, connection)
        {
            CommandType = CommandType.StoredProcedure
        };
        
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read()) Debug.Log($"{reader.GetString(0)} {reader.GetString(1)}");
        }
    }
}
