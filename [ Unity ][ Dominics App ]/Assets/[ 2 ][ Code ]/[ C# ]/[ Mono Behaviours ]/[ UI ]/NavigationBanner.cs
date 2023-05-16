using System;
using System.Collections;
using System.Collections.Generic;
using NetworkRail.SQL;
using UnityEngine;
using UnityEngine.UIElements;

public class NavigationBanner : MonoBehaviour
{
	#region Document References
	// - - -
		private UIDocument _document;
		private VisualElement _root;

		private VisualElement _navigationBanner;
		private VisualElement _dropdownList;
		private VisualElement _userLogin;
		private VisualElement _userPortal;

		private UserLogin _userLoginScript;
		private UserPortal _userPortalScript;
	// - - -
	#endregion

	#region References
	// - - -
		private Button _navigationDropdownButton;
		
		private Button _userPortalButton;
		private Button _manageUserDataButton;
		private Button _logoutButton;
	// - - -
	#endregion
	
	
	
	
		
	private void Awake()
	{
		_document = FindObjectOfType<UIDocument>();
		
		_userLoginScript = FindObjectOfType<UserLogin>();
		_userPortalScript = FindObjectOfType<UserPortal>();
		
		
		_root = _document.rootVisualElement;

		_navigationBanner = _root.Q<VisualElement>("NavigationBanner");
		_dropdownList = _root.Q<VisualElement>("Dropdown");
		_userLogin = _root.Q<VisualElement>("UserLogin");
		_userPortal = _root.Q<VisualElement>("UserPortal");
		
		_navigationDropdownButton = _navigationBanner.Q<Button>("NavigationBannerDropdownButton");
		
		_userPortalButton = _navigationBanner.Q<Button>("UserPortalButton");
		_manageUserDataButton = _navigationBanner.Q<Button>("ManageUserDetailsButton");
		_logoutButton = _navigationBanner.Q<Button>("LogoutButton");
		
		UIManager.Disable(_dropdownList);
	}

	private void OnEnable()
	{
		_navigationDropdownButton.RegisterCallback<ClickEvent>(OnNavigationDropdownButtonPressed);
		
		_userPortalButton.RegisterCallback<ClickEvent>(OnUserPortalButtonPressed);
		_manageUserDataButton.RegisterCallback<ClickEvent>(OnManageUserButtonPressed);
		_logoutButton.RegisterCallback<ClickEvent>(OnLogoutButtonPressed);
	}

	private void OnDisable()
	{
		_navigationDropdownButton.UnregisterCallback<ClickEvent>(OnNavigationDropdownButtonPressed);
		
		_userPortalButton.UnregisterCallback<ClickEvent>(OnUserPortalButtonPressed);
		_manageUserDataButton.UnregisterCallback<ClickEvent>(OnManageUserButtonPressed);
		_logoutButton.UnregisterCallback<ClickEvent>(OnLogoutButtonPressed);
	}



	private void OnNavigationDropdownButtonPressed(ClickEvent clickEvent)
	{
		switch (_dropdownList.style.display.value)
		{
			case DisplayStyle.Flex: UIManager.Disable(_dropdownList); break;
			case DisplayStyle.None: UIManager.Enable(_dropdownList); break;
		}
	}
	
	
	
	private void OnUserPortalButtonPressed(ClickEvent clickEvent)
	{
		UIManager.Swap(UIManager.ActiveUIPanel, _userPortal, true);
		UIManager.Disable(_dropdownList);
		
		_userPortalScript.SetUIDefaults();
	}
	
	private void OnManageUserButtonPressed(ClickEvent clickEvent)
	{
		UIManager.Disable(_dropdownList);
	}
	
	private void OnLogoutButtonPressed(ClickEvent clickEvent)
	{
		SQLManager.QueryLogout(UserData.Department, UserData.Username);
		
		UIManager.FadeTransition(UIManager.ActiveUIPanel, _userLogin, true);
		UIManager.FadeOut(_navigationBanner);
		UIManager.Disable(_dropdownList);
		
		_userLoginScript.SetUIDefaults();
	}
}
