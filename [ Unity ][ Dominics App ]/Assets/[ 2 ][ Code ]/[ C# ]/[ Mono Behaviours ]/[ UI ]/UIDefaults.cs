using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIDefaults : MonoBehaviour
{
    #region [ Document References ]
    // - - -
        private UIDocument _document;
        private VisualElement _root;

        private VisualElement _notification;
        private VisualElement _navigationBanner;
        
        private VisualElement _splashScreen;
        private VisualElement _userLogin;
        private VisualElement _userPortal;
        private VisualElement _manageUsers;
        private VisualElement _createUser;
        private VisualElement _userDetails;
        private VisualElement _manageJobs;
        private VisualElement _jobDetails;
        private VisualElement _jobSkillDetails;
        private VisualElement _createJob;
        private VisualElement _userSkills;
        private VisualElement _userShifts;
        private VisualElement _jobSkillsToUsers;
    // - - -
    #endregion
    
    
    
    

    private void Awake()
    {
        _document = FindObjectOfType<UIDocument>();
        _root = _document.rootVisualElement;
        //_root.styleSheets.Add(_defaultTextStyleSheet);

        _notification = _root.Q<VisualElement>("Notification");
        _navigationBanner = _root.Q<VisualElement>("NavigationBanner");
        
        _splashScreen = _root.Q<VisualElement>("SplashScreen");
        _userLogin = _root.Q<VisualElement>("UserLogin");
        _userPortal = _root.Q<VisualElement>("UserPortal");
        _manageUsers = _root.Q<VisualElement>("ManageUsers");
        _createUser = _root.Q<VisualElement>("CreateUser");
        _userDetails = _root.Q<VisualElement>("UserDetails");
        _manageJobs = _root.Q<VisualElement>("ManageJobs");
        _jobDetails = _root.Q<VisualElement>("JobDetails");
        _jobSkillDetails = _root.Q<VisualElement>("JobSkillDetails");
        _createJob = _root.Q<VisualElement>("CreateJob");
        _userSkills = _root.Q<VisualElement>("UserSkills");
        _userShifts = _root.Q<VisualElement>("UserShifts");
        _jobSkillsToUsers = _root.Q<VisualElement>("JobSkillsToUsers");
    }

    private void Start()
    {
        Screen.SetResolution(Display.main.renderingWidth, Display.main.renderingHeight, true);
    
        UIManager.Enable(_splashScreen, true);
        
        UIManager.Disable(_notification);
        UIManager.Disable(_navigationBanner);
     
        UIManager.Disable(_userLogin);
        UIManager.Disable(_userPortal);
        UIManager.Disable(_manageUsers);
        UIManager.Disable(_createUser);
        UIManager.Disable(_userDetails);
        UIManager.Disable(_manageJobs);
        UIManager.Disable(_jobDetails);
        UIManager.Disable(_jobSkillDetails);
        UIManager.Disable(_createJob);
        UIManager.Disable(_userSkills);
        UIManager.Disable(_userShifts);
        UIManager.Disable(_jobSkillsToUsers);
    }
}
