using System;
using NetworkRail.SQL;
using UnityEngine;
using UnityEngine.UIElements;

public class UserLogin : _UILayoutInterface
{
	#region [ Document References ]
	// - - -
		private UIDocument _document;
		private VisualElement _root;
		
		private VisualElement _navigationBannerLayout;
		private VisualElement _userLoginLayout;
		private VisualElement _userPortalLayout;
		
		private DropdownField _departmentDropdown;
		private TextField _usernameField;
		private TextField _passwordField;
		private Button _loginButton;
	// - - -
	#endregion

	#region [ References ]
	// - - -
		private UserPortal _userPortal;
	// - - -
	#endregion
	
	
	
	
	
	private void Awake()
	{
		_document = FindObjectOfType<UIDocument>();
		_userPortal = FindObjectOfType<UserPortal>();
		
		_root = _document.rootVisualElement;

		_navigationBannerLayout = _root.Q<VisualElement>("_NavigationBannerLayout");
		_userLoginLayout		= _root.Q<VisualElement>("_UserLoginLayout");
		_userPortalLayout		= _root.Q<VisualElement>("_UserPortalLayout");
		
		_departmentDropdown = _userLoginLayout.Q<DropdownField>("DepartmentDropdown");
		_usernameField		= _userLoginLayout.Q<TextField>("UsernameField");
		_passwordField		= _userLoginLayout.Q<TextField>("PasswordField");
		_loginButton		= _userLoginLayout.Q<Button>("LoginButton");
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

	
	
	public override void ResetUIDefaults()
	{
		_departmentDropdown.choices = Query.ListJobDepartment().departments;
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
		Query.QueryResultValidateLogin results = Query.ValidateLogin(_departmentDropdown.value, _usernameField.value, _passwordField.value);

		switch (results.loginSuccess)
		{
			case false: NotificationManager.Instance.ShowNotification("Login Error", results.message); break;
			case true:
			{
				UIManager.Enable(_navigationBannerLayout);
				UIManager.Swap(_userLoginLayout, _userPortalLayout, true);

				UserData.Department = _departmentDropdown.value;
				UserData.Username = _usernameField.text;
			
				_userPortal.ResetUIDefaults();
			} break;
		}
	}
}
