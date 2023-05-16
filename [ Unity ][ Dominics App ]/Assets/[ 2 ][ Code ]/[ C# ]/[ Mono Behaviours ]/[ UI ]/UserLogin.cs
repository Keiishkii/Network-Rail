using System;
using NetworkRail.SQL;
using UnityEngine;
using UnityEngine.UIElements;

public class UserLogin : MonoBehaviour
{
	#region [ Document References ]
	// - - -
		private UIDocument _document;
		
		private VisualElement _root;
		private VisualElement _navigationBanner;
		private VisualElement _userLogin;
		private VisualElement _userPortal;

		private UserPortal _userPortalScript;
	// - - -
	#endregion

	#region [ References ]
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
		_userPortalScript = FindObjectOfType<UserPortal>();
		
		_root = _document.rootVisualElement;

		_navigationBanner = _root.Q<VisualElement>("NavigationBanner");
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

	
	
	public void SetUIDefaults()
	{
		_departmentDropdown.choices = SQLManager.QueryListOfDepartments();
		_departmentDropdown.index = -1;
		
		_usernameField.SetEnabled(false);
		_usernameField.value = String.Empty;
		
		_passwordField.SetEnabled(false);
		_passwordField.value = String.Empty;
	}

	private void DepartmentDropdownValueChanged(ChangeEvent<string> evt)
	{
		_usernameField.SetEnabled(true);
		_passwordField.SetEnabled(true);
	}
	
	private void OnLoginButtonPressed(ClickEvent clickEvent)
	{
		SQLManager.LoginQueryResults results = SQLManager.QueryLogin(_departmentDropdown.value, _usernameField.value, _passwordField.value);
		Debug.Log($"{((results.resultLoginSuccess) ? ("[ Successful Login ]") : ("[ Unsuccessful Login ]"))} - [{results.resultMessage}]");
		
		if (results.resultLoginSuccess)
		{
			UIManager.Enable(_navigationBanner);
			UIManager.Swap(_userLogin, _userPortal, true);

			UserData.Department = _departmentDropdown.value;
			UserData.Username = _usernameField.text;
			
			_userPortalScript.SetUIDefaults();
		}
		else
		{
			NotificationManager.Instance.ShowNotification("Login Error", results.resultMessage);
		}
	}
}
