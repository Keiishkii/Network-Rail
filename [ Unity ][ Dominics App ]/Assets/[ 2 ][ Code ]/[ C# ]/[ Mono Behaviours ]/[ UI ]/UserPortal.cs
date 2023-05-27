using System;
using System.Collections;
using System.Collections.Generic;
using NetworkRail.SQL;
using UnityEngine;
using UnityEngine.UIElements;

public class UserPortal : _UILayoutInterface
{
    #region [ Document References ]
    // - - -
        private UIDocument _document;
        private VisualElement _root;
        
        private VisualElement _userPortalLayout;
        private VisualElement _manageUsersLayout;
        private VisualElement _manageJobsLayout;
        private VisualElement _userSkillsLayout;
        private VisualElement _userShiftsLayout;
        
        private Label _userDetailsDepartment;
        private Label _userDetailsUsername;
        private Label _userDetailsFullname;

        private Button _jobControllerMenuButton;
        
        private VisualElement _jobControllerResources;
        private Button _manageUserButton;
        private Button _manageJobsButton;
        
        private Button _PTSHolderMenuButton;
        
        private VisualElement _PTSHolderResources;
        private Button _userSkillsButton;
        private Button _shiftsAvailableButton;
    // - - -
    #endregion

    #region [ References ]
    // - - -
        private ManageUsers _manageUsers;
        private ManageJobs _manageJobs;
        private UserShifts _userShifts;
        private UserSkills _userSkills;
    // - - -
    #endregion

    #region [ Behaviour ]
    // - - -
        private Query.QueryResultUserPortal _userPortalData;
    // - - -
    #endregion
    
    
    
    
    
    private void Awake()
    {
        _document = FindObjectOfType<UIDocument>();
        _root = _document.rootVisualElement;

        _manageUsers = FindObjectOfType<ManageUsers>();
        _manageJobs = FindObjectOfType<ManageJobs>();
        _userShifts = FindObjectOfType<UserShifts>();
        _userSkills = FindObjectOfType<UserSkills>();
        
        _userPortalLayout       = _root.Q<VisualElement>("_UserPortalLayout");
        _manageUsersLayout      = _root.Q<VisualElement>("_ManageUsersLayout");
        _manageJobsLayout       = _root.Q<VisualElement>("_ManageJobsLayout");
        _userSkillsLayout       = _root.Q<VisualElement>("_UserSkillsLayout");
        _userShiftsLayout       = _root.Q<VisualElement>("_UserShiftsLayout");

        
        VisualElement userData  = _userPortalLayout.Q<VisualElement>("UserData");
        
        _userDetailsDepartment  = userData.Q<Label>("Department");
        _userDetailsUsername    = userData.Q<Label>("Username");
        _userDetailsFullname    = userData.Q<Label>("FullName");

        
        VisualElement resources = _userPortalLayout.Q<VisualElement>("Resources");
            
        _jobControllerMenuButton    = resources.Q<Button>("JobController");
        _jobControllerResources     = resources.Q<VisualElement>("JobControllerResources");
        _manageUserButton           = _jobControllerResources.Q<Button>("ManageUsersButton");
        _manageJobsButton           = _jobControllerResources.Q<Button>("ManageJobsButton");

        _PTSHolderMenuButton        = resources.Q<Button>("PTSHolder");
        _PTSHolderResources         = resources.Q<VisualElement>("PTSHolderResources");
        _userSkillsButton           = _PTSHolderResources.Q<Button>("UserSkillsButton");
        _shiftsAvailableButton      = _PTSHolderResources.Q<Button>("ShiftsAvailableButton");
    }

    private void OnEnable()
    {
        _jobControllerMenuButton.RegisterCallback<ClickEvent>(OnJobControllerMenuButtonPressed);
        _manageUserButton.RegisterCallback<ClickEvent>(OnManageUsersButtonPressed);
        _manageJobsButton.RegisterCallback<ClickEvent>(OnManageJobsButtonPressed);
        
        _PTSHolderMenuButton.RegisterCallback<ClickEvent>(OnPTSHolderMenuButtonPressed);
        _userSkillsButton.RegisterCallback<ClickEvent>(OnUserSkillsButtonPressed);
        _shiftsAvailableButton.RegisterCallback<ClickEvent>(OnShiftsAvailableButtonPressed);
    }

    private void OnDisable()
    {
        _jobControllerMenuButton.UnregisterCallback<ClickEvent>(OnJobControllerMenuButtonPressed);
        _manageUserButton.UnregisterCallback<ClickEvent>(OnManageUsersButtonPressed);
        _manageJobsButton.UnregisterCallback<ClickEvent>(OnManageJobsButtonPressed);
        
        _PTSHolderMenuButton.UnregisterCallback<ClickEvent>(OnPTSHolderMenuButtonPressed);
        _userSkillsButton.UnregisterCallback<ClickEvent>(OnUserSkillsButtonPressed);
        _shiftsAvailableButton.UnregisterCallback<ClickEvent>(OnShiftsAvailableButtonPressed);
    }

    

    public override void ResetUIDefaults()
    {
        _userPortalData = Query.UserPortal(UserData.Department, UserData.Username);
        
        _userDetailsDepartment.text = $"Department: {_userPortalData.department}";
        _userDetailsUsername.text = $"Username: {_userPortalData.username}";
        _userDetailsFullname.text = $"Full Name: {_userPortalData.fullName}";
        
        
        _jobControllerMenuButton.SetEnabled(_userPortalData.managementPermission);
        switch (_userPortalData.managementPermission)
        {
            case true: UIManager.Enable(_jobControllerResources); break;
            case false: UIManager.Disable(_jobControllerResources); break;
        }
        
        _PTSHolderMenuButton.SetEnabled(_userPortalData.PTSHolderPermission);
        switch ((!_userPortalData.managementPermission) && _userPortalData.PTSHolderPermission)
        {
            case true: UIManager.Enable(_PTSHolderResources); break;
            case false: UIManager.Disable(_PTSHolderResources); break;
        }
    }

    private void OnJobControllerMenuButtonPressed(ClickEvent clickEvent)
    {
        UIManager.Enable(_jobControllerResources);
        UIManager.Disable(_PTSHolderResources);
    }

    private void OnPTSHolderMenuButtonPressed(ClickEvent clickEvent)
    {
        UIManager.Disable(_jobControllerResources);
        UIManager.Enable(_PTSHolderResources);
    }

    private void OnManageUsersButtonPressed(ClickEvent clickEvent)
    {
        UIManager.Swap(_userPortalLayout, _manageUsersLayout, true);   
        _manageUsers.ResetUIDefaults();
    }

    private void OnManageJobsButtonPressed(ClickEvent clickEvent)
    {
        UIManager.Swap(_userPortalLayout, _manageJobsLayout, true);
        _manageJobs.ResetUIDefaults();
    }

    private void OnUserSkillsButtonPressed(ClickEvent clickEvent)
    {
        UIManager.Swap(_userPortalLayout, _userSkillsLayout, true);
        _userSkills.ResetUIDefaults();
    }

    private void OnShiftsAvailableButtonPressed(ClickEvent clickEvent)
    {
        UIManager.Swap(_userPortalLayout, _userShiftsLayout, true);
        _userShifts.ResetUIDefaults();
    }
}
