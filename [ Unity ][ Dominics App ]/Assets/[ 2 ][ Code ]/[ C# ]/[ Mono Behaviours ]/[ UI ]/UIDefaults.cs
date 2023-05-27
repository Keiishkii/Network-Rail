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

        private VisualElement _notificationLayout;
        private VisualElement _navigationBannerLayout;
        
        private VisualElement _splashScreenLayout;
        private VisualElement _userLoginLayout;
        private VisualElement _userPortalLayout;
        private VisualElement _manageUsersLayout;
        private VisualElement _createUserLayout;
        private VisualElement _userDetailsLayout;
        private VisualElement _manageJobsLayout;
        private VisualElement _jobDetailsLayout;
        private VisualElement _jobSkillDetailsLayout;
        private VisualElement _createJobLayout;
        private VisualElement _userSkillsLayout;
        private VisualElement _userShiftsLayout;
        private VisualElement _jobSkillsToUsersLayout;
    // - - -
    #endregion
    
    
    
    

    private void Awake()
    {
        _document = FindObjectOfType<UIDocument>();
        _root = _document.rootVisualElement;

        _notificationLayout     = _root.Q<VisualElement>("_NotificationLayout");
        _navigationBannerLayout = _root.Q<VisualElement>("_NavigationBannerLayout");
        
        _splashScreenLayout     = _root.Q<VisualElement>("_SplashScreenLayout");
        _userLoginLayout        = _root.Q<VisualElement>("_UserLoginLayout");
        _userPortalLayout       = _root.Q<VisualElement>("_UserPortalLayout");
        _manageUsersLayout      = _root.Q<VisualElement>("_ManageUsersLayout");
        _createUserLayout       = _root.Q<VisualElement>("_CreateUserLayout");
        _userDetailsLayout      = _root.Q<VisualElement>("_UserDetailsLayout");
        _manageJobsLayout       = _root.Q<VisualElement>("_ManageJobsLayout");
        _jobDetailsLayout       = _root.Q<VisualElement>("_JobDetailsLayout");
        _jobSkillDetailsLayout  = _root.Q<VisualElement>("_JobSkillDetailsLayout");
        _createJobLayout        = _root.Q<VisualElement>("_CreateJobLayout");
        _userSkillsLayout       = _root.Q<VisualElement>("_UserSkillsLayout");
        _userShiftsLayout       = _root.Q<VisualElement>("_UserShiftsLayout");
        _jobSkillsToUsersLayout = _root.Q<VisualElement>("_JobSkillsToUsersLayout");
    }

    private void Start()
    {
        Screen.SetResolution(Display.main.renderingWidth, Display.main.renderingHeight, true);
    
        UIManager.Enable(_splashScreenLayout, true);
        
        UIManager.Disable(_notificationLayout);
        UIManager.Disable(_navigationBannerLayout);
     
        UIManager.Disable(_userLoginLayout);
        UIManager.Disable(_userPortalLayout);
        UIManager.Disable(_manageUsersLayout);
        UIManager.Disable(_createUserLayout);
        UIManager.Disable(_userDetailsLayout);
        UIManager.Disable(_manageJobsLayout);
        UIManager.Disable(_jobDetailsLayout);
        UIManager.Disable(_jobSkillDetailsLayout);
        UIManager.Disable(_createJobLayout);
        UIManager.Disable(_userSkillsLayout);
        UIManager.Disable(_userShiftsLayout);
        UIManager.Disable(_jobSkillsToUsersLayout);
    }
}
