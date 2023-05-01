using System;
using NetworkRail.SQL;
using UnityEngine;
using UnityEngine.UIElements;

public class UserLogin : MonoBehaviour
{
	#region References
	// - - -
		private UIDocument _document;
		private VisualElement _root;

		private VisualElement _userLogin;
		private VisualElement _userPortal;
	// - - -
	#endregion

	#region References
	// - - -
		private DropdownField _departmentDropdown;
		private TextField _usernameField;
		private TextField _passwordField;
		private Button _loginButton;
	// - - -
	#endregion
	
	
	
	
	
	private void Awake()
	{
		_document = FindObjectOfType<UIDocument>();
		_root = _document.rootVisualElement;

		_userLogin = _root.Q<VisualElement>("UserLogin");
		_userPortal = _root.Q<VisualElement>("UserPortal");
		
		_departmentDropdown = _userLogin.Q<DropdownField>("DepartmentDropdown");
		_usernameField = _userLogin.Q<TextField>("UsernameField");
		_passwordField = _userLogin.Q<TextField>("PasswordField");
		_loginButton = _userLogin.Q<Button>("LoginButton");
	}

	private void OnEnable()
	{
		_departmentDropdown.RegisterCallback<ChangeEvent<string>>(DepartmentDropdownValueChanged);
		_loginButton.RegisterCallback<ClickEvent>(OnLoginButtonPressed);
	}

	private void OnDisable()
	{
		_departmentDropdown.UnregisterCallback<ChangeEvent<string>>(DepartmentDropdownValueChanged);
		_loginButton.UnregisterCallback<ClickEvent>(OnLoginButtonPressed);
	}

	private void Start() => SetUIDefaults();

	private void SetUIDefaults()
	{
		_departmentDropdown.choices = SQLManager.QueryListOfDepartments();
		
		_usernameField.SetEnabled(false);
		_passwordField.SetEnabled(false);
	}
	
	
	
	private void DepartmentDropdownValueChanged(ChangeEvent<string> evt)
	{
		_usernameField.SetEnabled(true);
		_passwordField.SetEnabled(true);
	}
	
	private void OnLoginButtonPressed(ClickEvent clickEvent)
	{
		(bool successfulLogin, string message) = SQLManager.QueryLogin(_departmentDropdown.value, _usernameField.text, _passwordField.text);
		if (successfulLogin)
		{
			Debug.Log($"[ Successful Login ] - [{message}]");
			UIManager.Swap(_userLogin, _userPortal);
		}
		else
		{
			Debug.Log($"[ Unsuccessful Login ] - [{message}]");
		}
	}
}
