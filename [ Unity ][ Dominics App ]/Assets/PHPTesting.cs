using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PHPTesting : MonoBehaviour
{
	#region [ Editor ]
	#if UNITY_EDITOR
	[CustomEditor(typeof(PHPTesting))]
	private class PHPTestingInspector : Editor
	{
		private PHPTesting _target;
		private void OnEnable() => _target = (PHPTesting) target;

		public override void OnInspectorGUI()
		{
			if (GUILayout.Button("Function One"))
			{
				_target.GetDate();
			}
		}
	}
	#endif
	#endregion

	private void GetDate() => StartCoroutine(GetDateCoroutine());
	private IEnumerator GetDateCoroutine()
	{
		using UnityWebRequest www = UnityWebRequest.Get("localhost:8000/GetDate.php");
		yield return www.SendWebRequest();
		
		if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError || www.result == UnityWebRequest.Result.DataProcessingError) Debug.Log($"Error: {www.error}");
		else
		{
			Debug.Log(www.downloadHandler.text);
		}
	}
}
