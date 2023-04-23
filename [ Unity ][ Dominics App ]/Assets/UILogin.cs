using System;
using NetworkRail.SQL;
using UnityEngine;
using UnityEngine.UIElements;

public class UILogin : MonoBehaviour
{
	#region Components
	// - - -
		private UIDocument _UIDocument;
	// - - -
	#endregion

	#region References
	// - - -
		private VisualElement _rootVisualElement;

		private DropdownField _departmentDropdown;
		private TextField _usernameField;
		private TextField _passwordField;
		private Button _loginButton;
	// - - -
	#endregion
	
	
	
	
	
	private void Awake()
	{
		_UIDocument = GetComponent<UIDocument>();
		_rootVisualElement = _UIDocument.rootVisualElement;
		
		_departmentDropdown = _rootVisualElement.Q<DropdownField>("DepartmentDropdown");
		_usernameField = _rootVisualElement.Q<TextField>("UsernameField");
		_passwordField = _rootVisualElement.Q<TextField>("PasswordField");
		_loginButton = _rootVisualElement.Q<Button>("LoginButton");

		SetUIDefaults();
	}

	private void OnEnable()
	{
		_departmentDropdown.RegisterValueChangedCallback(DepartmentDropdownValueChanged);
		_loginButton.clicked += OnLoginButtonPressed;
	}

	private void OnDisable()
	{
		_loginButton.clicked -= OnLoginButtonPressed;
	}



	private void SetUIDefaults()
	{
		_departmentDropdown.choices = SQLManager.QueryListOfDepartments();
		
		_usernameField.SetEnabled(false);
		_passwordField.SetEnabled(false);
	}
	
	
	
	private void DepartmentDropdownValueChanged(ChangeEvent<string> value)
	{
		_usernameField.SetEnabled(true);
		_passwordField.SetEnabled(true);
	}
	
	private void OnLoginButtonPressed()
	{
		Debug.Log($"Department: {_departmentDropdown.text}");
		Debug.Log($"Username: {_usernameField.text}");
		Debug.Log($"Password: {_passwordField.text}");

		(bool successfulLogin, string message) = SQLManager.QueryLogin(_departmentDropdown.value, _usernameField.text, _passwordField.text);
		if (successfulLogin)
		{
			
		}
		
		Debug.Log($"[{((successfulLogin) ? ("SUCCESS") : ("FAILURE"))}] Password: {message}");
	}
}
