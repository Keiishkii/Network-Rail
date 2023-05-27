using System;
using System.Collections;
using System.Collections.Generic;
using NetworkRail.SQL;
using UnityEngine;
using UnityEngine.UIElements;

public class ManageUsers : _UILayoutInterface
{
    #region [ Document References ]
    // - - -
        private UIDocument _document;
        private VisualElement _root;
        
        [SerializeField] private VisualTreeAsset _manageUserListElement;

        private VisualElement _manageUsersLayout;
        private VisualElement _createUserLayout;
        
        private VisualElement _manageUsersListLayout;
        
        private readonly List<TemplateContainer> _templateContainers = new List<TemplateContainer>();
        private readonly List<Toggle> _PTSPermissionToggles = new List<Toggle>();
        private readonly List<Toggle> _managementPermissionToggles = new List<Toggle>();
        private readonly List<Label> _nameLabels = new List<Label>();
        
        private Button _applyChangesButton;
        private Button _createUserButton;
    // - - -
    #endregion

    #region [ References ]
    // - - -
        private CreateUser _createUser;
    // - - -
    #endregion
    
    #region [ Behaviour ]
    // - - -
        private Query.QueryResultManageUserPermissions _queryResultManageUserPermissions;
        private string _userPermissionsTable;
    // - - -
    #endregion
    
    


    private void Awake()
    {
        _document = FindObjectOfType<UIDocument>();
        _root = _document.rootVisualElement;

        _createUser = FindObjectOfType<CreateUser>();
        
        _manageUsersLayout = _root.Q<VisualElement>("_ManageUsersLayout");
        _createUserLayout = _root.Q<VisualElement>("_CreateUserLayout");

        _manageUsersListLayout = _manageUsersLayout.Q<VisualElement>("UserListLayoutContainer").Q<VisualElement>("Contents");
        
        _applyChangesButton = _manageUsersLayout.Q<Button>("ApplyChangesButton");
        _createUserButton = _manageUsersLayout.Q<Button>("CreateUserButton");
    }

    private void OnEnable()
    {
        _applyChangesButton.RegisterCallback<ClickEvent>(OnApplyChangesButtonPressed);
        _createUserButton.RegisterCallback<ClickEvent>(OnCreateUserButtonPressed);
    }

    private void OnDisable()
    {
        _applyChangesButton.UnregisterCallback<ClickEvent>(OnApplyChangesButtonPressed);
        _createUserButton.UnregisterCallback<ClickEvent>(OnCreateUserButtonPressed);
    }
    
    

    public override void ResetUIDefaults()
    {
        _queryResultManageUserPermissions = Query.ManageUserPermissions(UserData.Department, UserData.Username);
        _userPermissionsTable = Query.GetManageUserTableName(UserData.Department, UserData.Username);        

        ClearListVisualElements();
        CreateAndAssignVisualElements();

        for (int i = 0; i < _templateContainers.Count; i++)
        {
            _nameLabels[i].text = _queryResultManageUserPermissions.userPermissionsData[i].userFullName;
            _PTSPermissionToggles[i].SetValueWithoutNotify(_queryResultManageUserPermissions.userPermissionsData[i].PTSPermissions);
            _managementPermissionToggles[i].SetValueWithoutNotify(_queryResultManageUserPermissions.userPermissionsData[i].managementPermissions);
            
            _PTSPermissionToggles[i].RegisterCallback<ChangeEvent<bool>, int>(OnPTSPermissionsValueChanged, i);
            _managementPermissionToggles[i].RegisterCallback<ChangeEvent<bool>, int>(OnManagementPermissionsValueChanged, i);
        }
        
        
        _applyChangesButton.SetEnabled(false);
    }

    private void ClearListVisualElements()
    {
        for (int i = _templateContainers.Count - 1; i >= 0; i--)
        {
            _PTSPermissionToggles[i].UnregisterCallback<ChangeEvent<bool>, int>(OnPTSPermissionsValueChanged);
            _managementPermissionToggles[i].UnregisterCallback<ChangeEvent<bool>, int>(OnManagementPermissionsValueChanged);
            
            _manageUsersListLayout.Remove(_templateContainers[i]);
        }
        
        _templateContainers.Clear();
        
        _nameLabels.Clear();
        _PTSPermissionToggles.Clear();
        _managementPermissionToggles.Clear();
    }

    private void CreateAndAssignVisualElements()
    {
        for (int i = 0; i < _queryResultManageUserPermissions.userPermissionsData.Count; i++)
        {
            TemplateContainer elementContainer = _manageUserListElement.Instantiate();
            
            _manageUsersListLayout.Add(elementContainer);
            _templateContainers.Add(elementContainer);
            
            _nameLabels.Add(elementContainer.Q<Label>("FullName"));
            _PTSPermissionToggles.Add(elementContainer.Q<Toggle>("PTSPermissionToggle"));
            _managementPermissionToggles.Add(elementContainer.Q<Toggle>("ManagementPermissionToggle"));
        }
    }
    
    

    private void OnPTSPermissionsValueChanged(ChangeEvent<bool> changeEvent, int index)
    {
        Debug.Log("Hello!");
        
        if (changeEvent.newValue == changeEvent.previousValue) return;
        Query.RunNonQuery($"UPDATE {_userPermissionsTable} SET {("PTS")} = '{(changeEvent.newValue ? 1 : 0)}' WHERE {("User_Login")} = '{_queryResultManageUserPermissions.userPermissionsData[index].username}'");
        
        _applyChangesButton.SetEnabled(true);
    }
    
    private void OnManagementPermissionsValueChanged(ChangeEvent<bool> changeEvent, int index)
    {
        Debug.Log("Hello!");
        
        if (changeEvent.newValue == changeEvent.previousValue) return;
        Query.RunNonQuery($"UPDATE {_userPermissionsTable} SET {("JC")} = '{(changeEvent.newValue ? 1 : 0)}' WHERE {("User_Login")} = '{_queryResultManageUserPermissions.userPermissionsData[index].username}'");
        
        _applyChangesButton.SetEnabled(true);
    }

    
    
    private void OnApplyChangesButtonPressed(ClickEvent clickEvent)
    {
        Query.ApplyUserPermissionChanges(UserData.Department, UserData.Username);
        _applyChangesButton.SetEnabled(false);
    }

    private void OnCreateUserButtonPressed(ClickEvent clickEvent)
    {
        UIManager.Swap(_manageUsersLayout, _createUserLayout, true);   
        _createUser.ResetUIDefaults();
    }
}
