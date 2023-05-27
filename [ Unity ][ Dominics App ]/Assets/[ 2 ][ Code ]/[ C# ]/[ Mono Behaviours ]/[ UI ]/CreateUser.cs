using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NetworkRail.SQL;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateUser : _UILayoutInterface
{
	#region [ Document References ]
	// - - -
		private UIDocument _document;
		private VisualElement _root;
		
		private VisualElement _createUserLayout;
		private VisualElement _manageUsersLayout;
		
		private TextField _usernameInputField;
		private TextField _passwordInputField;
		private Toggle _PTSPermissionToggle;
		private Toggle _managementPermissionToggle;
		
		private Button _returnButton;
		private Button _createUserButton;
	// - - -
	#endregion
	
	#region [ References ]
	// - - -
		private ManageUsers _manageUsers;
	// - - -
	#endregion
	
	#region [ Behaviour ]
	// - - -
		private readonly Dictionary<string, bool> _setValueDictionary = new Dictionary<string, bool>();

		private string _newUserUsername;
		private string _newUserPassword;
		
		private bool _ptsPermissions;
		private bool _managementPermissions;
	// - - -
	#endregion

	
	
	

	private void Awake()
	{
		_document = FindObjectOfType<UIDocument>();
		_root = _document.rootVisualElement;

		_manageUsers = FindObjectOfType<ManageUsers>();
        
		_createUserLayout = _root.Q<VisualElement>("_CreateUserLayout");
		_manageUsersLayout = _root.Q<VisualElement>("_ManageUsersLayout");

		_usernameInputField = _createUserLayout.Q<TextField>("UsernameTextField");
		_passwordInputField = _createUserLayout.Q<TextField>("PasswordTextField");
		_PTSPermissionToggle = _createUserLayout.Q<Toggle>("PTSPermissionToggle");
		_managementPermissionToggle = _createUserLayout.Q<Toggle>("ManagementPermissionToggle");
		
		_returnButton = _createUserLayout.Q<Button>("ReturnButton");
		_createUserButton = _createUserLayout.Q<Button>("CreateUserButton");
	}

	private void OnEnable()
	{
		_usernameInputField.RegisterCallback<ChangeEvent<string>>(OnUsernameValueChanged);
		_passwordInputField.RegisterCallback<ChangeEvent<string>>(OnPasswordValueChanged);
		
		_PTSPermissionToggle.RegisterCallback<ChangeEvent<bool>>(OnPTSPermissionValueChanged);
		_managementPermissionToggle.RegisterCallback<ChangeEvent<bool>>(OnManagementPermissionValueChanged);
		
		_returnButton.RegisterCallback<ClickEvent>(OnReturnButtonPressed);
		_createUserButton.RegisterCallback<ClickEvent>(OnCreateUserButtonPressed);
	}

	private void OnDisable()
	{
		_usernameInputField.UnregisterCallback<ChangeEvent<string>>(OnUsernameValueChanged);
		_passwordInputField.UnregisterCallback<ChangeEvent<string>>(OnPasswordValueChanged);
		
		_PTSPermissionToggle.UnregisterCallback<ChangeEvent<bool>>(OnPTSPermissionValueChanged);
		_managementPermissionToggle.UnregisterCallback<ChangeEvent<bool>>(OnManagementPermissionValueChanged);
		
		_returnButton.UnregisterCallback<ClickEvent>(OnReturnButtonPressed);
		_createUserButton.UnregisterCallback<ClickEvent>(OnCreateUserButtonPressed);
	}

	
	
	public override void ResetUIDefaults()
	{
		_usernameInputField.SetValueWithoutNotify(" - - - ");
		_newUserUsername = "";
		
		_passwordInputField.SetValueWithoutNotify(" - - - ");
		_newUserPassword = "";
		
		_PTSPermissionToggle.SetValueWithoutNotify(false);
		_ptsPermissions = false;
		
		_managementPermissionToggle.SetValueWithoutNotify(false);
		_managementPermissions = false;
		
		_passwordInputField.SetEnabled(false);
		
		_PTSPermissionToggle.SetEnabled(false);
		_managementPermissionToggle.SetEnabled(false);
		
		_createUserButton.SetEnabled(false);
		
		SetDictionaryValue("Username", false);
		SetDictionaryValue("Password", false);
	}

	private void SetDictionaryValue(in string key, in bool value)
	{
		switch (_setValueDictionary.ContainsKey(key))
		{
			case true: _setValueDictionary[key] = value; break;
			case false: _setValueDictionary.Add(key, value); break;
		}
	}



	private void OnUsernameValueChanged(ChangeEvent<string> changeEvent)
	{
		switch (changeEvent.newValue.Length > 0)
		{
			case true:
			{
				_newUserUsername = changeEvent.newValue;
				
				SetDictionaryValue("Username", true);
				CheckForApplicability();
				
				_passwordInputField.SetEnabled(true);
			} break;
			case false:
			{
				SetDictionaryValue("Username", false);
				SetDictionaryValue("Password", false);
				CheckForApplicability();
				
				_passwordInputField.SetValueWithoutNotify(" - - - ");
				_passwordInputField.SetEnabled(false);
				
				_PTSPermissionToggle.SetEnabled(false);
				_managementPermissionToggle.SetEnabled(false);
				_createUserButton.SetEnabled(false);
			} break;
		}
	}
	
	private void OnPasswordValueChanged(ChangeEvent<string> changeEvent)
	{
		switch (changeEvent.newValue.Length > 0)
		{
			case true:
			{
				_newUserPassword = changeEvent.newValue;
				
				SetDictionaryValue("Password", true);
				CheckForApplicability();
				
				_PTSPermissionToggle.SetEnabled(true);
				_managementPermissionToggle.SetEnabled(true);
			} break;
			case false:
			{
				SetDictionaryValue("Password", false);
				CheckForApplicability();
				
				_PTSPermissionToggle.SetEnabled(false);
				_managementPermissionToggle.SetEnabled(false);
				_createUserButton.SetEnabled(false);
			} break;
		}
	}
	
	private void OnPTSPermissionValueChanged(ChangeEvent<bool> changeEvent)
	{
		_ptsPermissions = changeEvent.newValue;
	}
	
	private void OnManagementPermissionValueChanged(ChangeEvent<bool> changeEvent)
	{
		_managementPermissions = changeEvent.newValue;
	}
	
	
	
	private void CheckForApplicability() => _createUserButton.SetEnabled(_setValueDictionary.Values.All(set => set));

	private void OnCreateUserButtonPressed(ClickEvent clickEvent)
	{
		Query.CreateUser(UserData.Department, UserData.Username, _newUserUsername, _newUserPassword, _ptsPermissions, _managementPermissions);
		NotificationManager.Instance.ShowNotification("User Creation", "You created a user!");
			
		UIManager.Swap(_createUserLayout, _manageUsersLayout, true);   
		_manageUsers.ResetUIDefaults();
	}
	
	private void OnReturnButtonPressed(ClickEvent clickEvent)
	{
		UIManager.Swap(_createUserLayout, _manageUsersLayout, true);   
		_manageUsers.ResetUIDefaults();
	}
}
