using System;
using NetworkRail.SQL;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
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

		PopulateDepartmentDropdownContent();
	}

	private void OnEnable()
	{
		_loginButton.clicked += OnLoginButtonPressed;
	}

	private void OnDisable()
	{
		_loginButton.clicked -= OnLoginButtonPressed;
	}



	private void PopulateDepartmentDropdownContent()
	{
		_departmentDropdown.choices = SQLManager.GetListOfDepartments();
	}
	
	private void OnLoginButtonPressed()
	{
		Debug.Log($"Username: {_usernameField.text}");
		Debug.Log($"Password: {_passwordField.text}");
	}
}
