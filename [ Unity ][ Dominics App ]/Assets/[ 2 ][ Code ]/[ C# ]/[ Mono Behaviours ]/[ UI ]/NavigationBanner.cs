using System;
using System.Collections;
using System.Collections.Generic;
using NetworkRail.SQL;
using UnityEngine;
using UnityEngine.UIElements;

public class NavigationBanner : _UILayoutInterface
{
	#region Document References
	// - - -
		private UIDocument _document;
		private VisualElement _root;

		private VisualElement _navigationBannerLayout;
		private VisualElement _userLoginLayout;
		private VisualElement _userPortalLayout;
		private VisualElement _userDetailsLayout;
		
		
		private VisualElement _dropdownLayout;
		private VisualElement _bannerLayout;
	
		private Button _navigationDropdownButton;
		
		private Button _userPortalButton;
		private Button _manageUserDataButton;
		private Button _logoutButton;
	// - - -
	#endregion

	#region References
	// - - -
		private UserLogin _userLogin;
		private UserPortal _userPortal;
		private UserDetails _userDetails;
	// - - -
	#endregion
	
	
	
	
		
	private void Awake()
	{
		_document = FindObjectOfType<UIDocument>();
		_root = _document.rootVisualElement;
		
		_userLogin = FindObjectOfType<UserLogin>();
		_userPortal = FindObjectOfType<UserPortal>();
		_userDetails = FindObjectOfType<UserDetails>();
		
		
		_navigationBannerLayout = _root.Q<VisualElement>("_NavigationBannerLayout");
		_userLoginLayout		= _root.Q<VisualElement>("_UserLoginLayout");
		_userPortalLayout		= _root.Q<VisualElement>("_UserPortalLayout");
		_userDetailsLayout		= _root.Q<VisualElement>("_UserDetailsLayout");
		
		_bannerLayout	= _navigationBannerLayout.Q<VisualElement>("BannerLayout");
		_dropdownLayout = _navigationBannerLayout.Q<VisualElement>("DropdownLayout");
		
		_navigationDropdownButton = _navigationBannerLayout.Q<Button>("NavigationBannerDropdownButton");
		
		_userPortalButton		= _dropdownLayout.Q<Button>("UserPortalButton");
		_manageUserDataButton	= _dropdownLayout.Q<Button>("ManageUserDetailsButton");
		_logoutButton			= _dropdownLayout.Q<Button>("LogoutButton");
		
		UIManager.Disable(_dropdownLayout);
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
		switch (_dropdownLayout.style.display.value)
		{
			case DisplayStyle.Flex: UIManager.Disable(_dropdownLayout); break;
			case DisplayStyle.None: UIManager.Enable(_dropdownLayout); break;
		}
	}
	
	
	
	private void OnUserPortalButtonPressed(ClickEvent clickEvent)
	{
		UIManager.Swap(UIManager.ActiveUIPanel, _userPortalLayout, true);
		UIManager.Disable(_dropdownLayout);
		
		_userPortal.ResetUIDefaults();
	}
	
	private void OnManageUserButtonPressed(ClickEvent clickEvent)
	{
		UIManager.Swap(UIManager.ActiveUIPanel, _userDetailsLayout, true);
		UIManager.Disable(_dropdownLayout);
		
		_userDetails.ResetUIDefaults();
	}
	
	private void OnLogoutButtonPressed(ClickEvent clickEvent)
	{
		Query.LogoutUserLogin(UserData.Department, UserData.Username);
		
		UIManager.FadeTransition(UIManager.ActiveUIPanel, _userLoginLayout, true);
		UIManager.FadeOut(_navigationBannerLayout);
		UIManager.Disable(_dropdownLayout);
		
		_userLogin.ResetUIDefaults();
	}
}
