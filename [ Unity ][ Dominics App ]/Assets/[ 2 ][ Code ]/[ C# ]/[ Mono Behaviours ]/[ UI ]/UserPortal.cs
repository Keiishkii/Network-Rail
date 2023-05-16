using System;
using System.Collections;
using System.Collections.Generic;
using NetworkRail.SQL;
using UnityEngine;
using UnityEngine.UIElements;

public class UserPortal : MonoBehaviour
{
    #region [ Document References ]
    // - - -
        private UIDocument _document;
        
        private VisualElement _root;
        private VisualElement _userPortal;
        
        private VisualElement _manageUsers;
        private VisualElement _manageJobs;
        private VisualElement _userSkills;
        private VisualElement _shiftsAvailable;
    // - - -
    #endregion

    #region [ References ]
    // - - -
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

    #region [ Behaviour ]
    // - - -
        private SQLManager.UserPortalQueryResult _userPortalData;
    // - - -
    #endregion
    
    
    
    
    
    private void Awake()
    {
        _document = FindObjectOfType<UIDocument>();
        _root = _document.rootVisualElement;

        _userPortal = _root.Q<VisualElement>("UserPortal");
        
        _manageUsers = _root.Q<VisualElement>("ManageUsers");
        _manageJobs = _root.Q<VisualElement>("ManageJobs");
        _userSkills = _root.Q<VisualElement>("UserSkills");
        _shiftsAvailable = _root.Q<VisualElement>("ShiftsAvailable");

        #region [ User Data ]
            VisualElement userData = _userPortal.Q<VisualElement>("UserData");
            
            _userDetailsDepartment = userData.Q<Label>("Department");
            _userDetailsUsername = userData.Q<Label>("Username");
            _userDetailsFullname = userData.Q<Label>("FullName");
        #endregion

        #region [ Resources ]
            VisualElement resources = _userPortal.Q<VisualElement>("Resources");
            
            #region [ Job Controller ]
                _jobControllerMenuButton = resources.Q<Button>("JobController");
                _jobControllerResources = resources.Q<VisualElement>("JobControllerResources");
                
                _manageUserButton = _jobControllerResources.Q<Button>("ManageUsersButton");
                _manageJobsButton = _jobControllerResources.Q<Button>("ManageJobsButton");
            #endregion
            #region [ PTS Holder ]
                _PTSHolderMenuButton = resources.Q<Button>("PTSHolder");
                _PTSHolderResources = resources.Q<VisualElement>("PTSHolderResources");
                
                _userSkillsButton = _PTSHolderResources.Q<Button>("UserSkillsButton");
                _shiftsAvailableButton = _PTSHolderResources.Q<Button>("ShiftsAvailableButton");
            #endregion
        #endregion
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

    

    public void SetUIDefaults()
    {
        _userPortalData = SQLManager.QueryUserPortal(UserData.Department, UserData.Username);

        Debug.Log($"Department: {_userPortalData.resultDepartment}");
        Debug.Log($"Username: {_userPortalData.resultUsername}");
        Debug.Log($"Full Name: {_userPortalData.resultFullName}");
        
        _userDetailsDepartment.text = $"Department: {_userPortalData.resultDepartment}";
        _userDetailsUsername.text = $"Username: {_userPortalData.resultUsername}";
        _userDetailsFullname.text = $"Full Name: {_userPortalData.resultFullName}";
        
        
        
        _jobControllerMenuButton.SetEnabled(_userPortalData.resultJobControllerPermission);
        switch (_userPortalData.resultJobControllerPermission)
        {
            case true: UIManager.Enable(_jobControllerResources); break;
            case false: UIManager.Disable(_jobControllerResources); break;
        }
        
        _PTSHolderMenuButton.SetEnabled(_userPortalData.resultPTSHolderPermission);
        switch ((!_userPortalData.resultJobControllerPermission) && _userPortalData.resultPTSHolderPermission)
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
        UIManager.Swap(_userPortal, _manageUsers, true);   
    }

    private void OnManageJobsButtonPressed(ClickEvent clickEvent)
    {
        UIManager.Swap(_userPortal, _manageJobs, true);
    }

    private void OnUserSkillsButtonPressed(ClickEvent clickEvent)
    {
        UIManager.Swap(_userPortal, _userSkills, true);
    }

    private void OnShiftsAvailableButtonPressed(ClickEvent clickEvent)
    {
        UIManager.Swap(_userPortal, _shiftsAvailable, true);
    }
}
