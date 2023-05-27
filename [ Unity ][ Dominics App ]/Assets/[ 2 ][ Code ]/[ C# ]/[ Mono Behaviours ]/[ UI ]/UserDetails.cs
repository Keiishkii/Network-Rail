using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NetworkRail.SQL;
using UnityEngine;
using UnityEngine.UIElements;

public class UserDetails : _UILayoutInterface
{
    #region [ Document References ]
    // - - -
        private UIDocument _document;
        private VisualElement _root;
            
        private VisualElement _userDetailsLayout;
            
        private Label _departmentLabel;
        private Label _usernameLabel;

        private TextField _firstNameTextField;
        private TextField _lastNameTextField;
        private TextField _passwordTextField;
        private TextField _emailAddressTextField;
        
        private DropdownField _countyDropdownField;
        private DropdownField _regionDropdownField;
        private DropdownField _districtDropdownField;
        private DropdownField _travelDistanceDropdownField;
        private DropdownField _acceptingJobsDropdownField;
        
        private Button _updateButton;
    // - - -
    #endregion

    #region [ Behaviour ]
    // - - -
        private string _userDetailsTableName;
        private readonly Dictionary<string, bool> _setValueDictionary = new Dictionary<string, bool>();
    // - - -
    #endregion
    
    
    
    private void Awake()
    {
        _document = FindObjectOfType<UIDocument>();
        _root = _document.rootVisualElement;

        _userDetailsLayout = _root.Q<VisualElement>("_UserDetailsLayout");

        _departmentLabel    = _userDetailsLayout.Q<Label>("DepartmentValue");
        _usernameLabel      = _userDetailsLayout.Q<Label>("UsernameValue");
        
        _firstNameTextField     = _userDetailsLayout.Q<TextField>("FirstNameInputField");
        _lastNameTextField      = _userDetailsLayout.Q<TextField>("LastNameInputField");
        _passwordTextField      = _userDetailsLayout.Q<TextField>("PasswordInputField");
        _emailAddressTextField  = _userDetailsLayout.Q<TextField>("EmailAddressInputField");
            
        _countyDropdownField            = _userDetailsLayout.Q<DropdownField>("CountyDropdown");
        _regionDropdownField            = _userDetailsLayout.Q<DropdownField>("RegionDropdown");
        _districtDropdownField          = _userDetailsLayout.Q<DropdownField>("DistrictDropdown");
        _travelDistanceDropdownField    = _userDetailsLayout.Q<DropdownField>("TravelDistanceDropdown");
        _acceptingJobsDropdownField     = _userDetailsLayout.Q<DropdownField>("AcceptingJobsDropdown");

        _updateButton = _userDetailsLayout.Q<Button>("UpdateChangesButton");
    }

    private void OnEnable()
    {
        _updateButton.RegisterCallback<ClickEvent>(OnUpdateButtonPressed);
        
        _firstNameTextField.RegisterCallback<ChangeEvent<string>>(OnFirstNameChanged);
        _lastNameTextField.RegisterCallback<ChangeEvent<string>>(OnLastNameChanged);
        _passwordTextField.RegisterCallback<ChangeEvent<string>>(OnPasswordChanged);
        _emailAddressTextField.RegisterCallback<ChangeEvent<string>>(OnEmailAddressChanged);
        
        _countyDropdownField.RegisterCallback<ChangeEvent<string>>(OnCountyValueChanged);
        _regionDropdownField.RegisterCallback<ChangeEvent<string>>(OnRegionValueChanged);
        _districtDropdownField.RegisterCallback<ChangeEvent<string>>(OnDistrictValueChanged);
        _travelDistanceDropdownField.RegisterCallback<ChangeEvent<string>>(OnTravelDistanceValueChanged);
        _acceptingJobsDropdownField.RegisterCallback<ChangeEvent<string>>(OnAcceptingJobsValueChanged);
    }

    private void OnDisable()
    {
        _updateButton.UnregisterCallback<ClickEvent>(OnUpdateButtonPressed);
        
        _firstNameTextField.UnregisterCallback<ChangeEvent<string>>(OnFirstNameChanged);
        _lastNameTextField.UnregisterCallback<ChangeEvent<string>>(OnLastNameChanged);
        _passwordTextField.UnregisterCallback<ChangeEvent<string>>(OnPasswordChanged);
        _emailAddressTextField.UnregisterCallback<ChangeEvent<string>>(OnEmailAddressChanged);
        
        _countyDropdownField.UnregisterCallback<ChangeEvent<string>>(OnCountyValueChanged);
        _regionDropdownField.UnregisterCallback<ChangeEvent<string>>(OnRegionValueChanged);
        _districtDropdownField.UnregisterCallback<ChangeEvent<string>>(OnDistrictValueChanged);
        _travelDistanceDropdownField.UnregisterCallback<ChangeEvent<string>>(OnTravelDistanceValueChanged);
        _acceptingJobsDropdownField.UnregisterCallback<ChangeEvent<string>>(OnAcceptingJobsValueChanged);
    }


    public override void ResetUIDefaults()
    {
        Query.QueryResultSelectUserLoginDetails result = Query.SelectUserLoginDetails(UserData.Department, UserData.Username);
        _userDetailsTableName = Query.GetLoginDetailsTempTableName(UserData.Department, UserData.Username);        
        
        _departmentLabel.text = result.department;
        _usernameLabel.text = result.username;
        
        _firstNameTextField.SetValueWithoutNotify(result.firstName);
        _lastNameTextField.SetValueWithoutNotify(result.lastName);
        _passwordTextField.SetValueWithoutNotify(result.password);
        _emailAddressTextField.SetValueWithoutNotify(result.emailAddress);
        
        _countyDropdownField.choices = Query.ListPostCodeCounty().counties;
        _countyDropdownField.SetValueWithoutNotify(result.county);
        
        _regionDropdownField.choices = Query.ListPostCodeRegion(result.county).regions;
        _regionDropdownField.SetValueWithoutNotify(result.region);
        
        _districtDropdownField.choices = Query.ListPostCodeDistrict(result.region).districts;
        _districtDropdownField.SetValueWithoutNotify(result.district);
        
        _travelDistanceDropdownField.choices = Query.ListTravelDistance(result.region).travelDistances.ConvertAll<string>(x => x.ToString());
        _travelDistanceDropdownField.SetValueWithoutNotify(result.travelDistance.ToString());
        
        _acceptingJobsDropdownField.choices = Query.ListAvailability().availability;
        _acceptingJobsDropdownField.SetValueWithoutNotify(result.jobAvailability);

        
        SetDictionaryValue("Changed", false);
        
        SetDictionaryValue("First Name Set", true);
        SetDictionaryValue("Last Name Set", true);
        SetDictionaryValue("Password Set", true);
        SetDictionaryValue("Email Address Set", true);
        
        SetDictionaryValue("County Set", true);
        SetDictionaryValue("Region Set", true);
        SetDictionaryValue("District Set", true);
        SetDictionaryValue("Travel Distance Set", true);
        SetDictionaryValue("Accepting Jobs Set", true);
        
        CheckForApplicability();
    }

    private void SetDictionaryValue(in string key, in bool value)
    {
        switch (_setValueDictionary.ContainsKey(key))
        {
            case true: _setValueDictionary[key] = value; break;
            case false: _setValueDictionary.Add(key, value); break;
        }
    }
    
    

    private void OnFirstNameChanged(ChangeEvent<string> changeEvent)
    {
        if (changeEvent.newValue.Equals(changeEvent.previousValue)) return;
        Query.RunNonQuery($"UPDATE {_userDetailsTableName} SET {("First_Name")} = '{changeEvent.newValue}'");

        SetDictionaryValue("Changed", true);
        SetDictionaryValue("First Name Set", true);
        
        CheckForApplicability();
    }
    
    private void OnLastNameChanged(ChangeEvent<string> changeEvent)
    {
        if (changeEvent.newValue.Equals(changeEvent.previousValue)) return;
        Query.RunNonQuery($"UPDATE {_userDetailsTableName} SET {("Last_Name")} = '{changeEvent.newValue}'");

        
        SetDictionaryValue("Changed", true);
        SetDictionaryValue("Last Name Set", true);
        
        CheckForApplicability();
    }
    
    private void OnPasswordChanged(ChangeEvent<string> changeEvent)
    {
        if (changeEvent.newValue.Equals(changeEvent.previousValue)) return;
        Query.RunNonQuery($"UPDATE {_userDetailsTableName} SET {("User_Password")} = '{changeEvent.newValue}'");
        
        
        SetDictionaryValue("Changed", true);
        SetDictionaryValue("Password Set", true);
        
        CheckForApplicability();
    }
    
    private void OnEmailAddressChanged(ChangeEvent<string> changeEvent)
    {
        if (changeEvent.newValue.Equals(changeEvent.previousValue)) return;
        Query.RunNonQuery($"UPDATE {_userDetailsTableName} SET {("Email_Address")} = '{changeEvent.newValue}'");
        
        
        SetDictionaryValue("Changed", true);
        SetDictionaryValue("Email Address Set", true);
        
        CheckForApplicability();
    }

    private void OnCountyValueChanged(ChangeEvent<string> changeEvent)
    {
        if (changeEvent.newValue.Equals(changeEvent.previousValue)) return;
        Query.RunNonQuery($"UPDATE {_userDetailsTableName} SET {("County")} = '{changeEvent.newValue}'");
        
        _regionDropdownField.choices = Query.ListPostCodeRegion(changeEvent.newValue).regions;
        _regionDropdownField.SetValueWithoutNotify(" - - - ");
        _regionDropdownField.SetEnabled(true);
        
        _districtDropdownField.SetValueWithoutNotify(" - - - ");
        _districtDropdownField.SetEnabled(false);
        
        
        SetDictionaryValue("Changed", true);
        SetDictionaryValue("County Set", true);
        SetDictionaryValue("Region Set", false);
        SetDictionaryValue("District Set", false);
        
        CheckForApplicability();
    }
    
    private void OnRegionValueChanged(ChangeEvent<string> changeEvent)
    {
        if (changeEvent.newValue.Equals(changeEvent.previousValue)) return;
        Query.RunNonQuery($"UPDATE {_userDetailsTableName} SET {("Region")} = '{changeEvent.newValue}'");

        _districtDropdownField.choices = Query.ListPostCodeDistrict(changeEvent.newValue).districts;
        _districtDropdownField.SetValueWithoutNotify(" - - - ");
        _districtDropdownField.SetEnabled(true);
        
        
        SetDictionaryValue("Changed", true);
        SetDictionaryValue("Region Set", true);
        SetDictionaryValue("District Set", false);
        
        CheckForApplicability();
    }
    
    private void OnDistrictValueChanged(ChangeEvent<string> changeEvent)
    {
        if (changeEvent.newValue.Equals(changeEvent.previousValue)) return;
        Query.RunNonQuery($"UPDATE {_userDetailsTableName} SET {("District")} = '{changeEvent.newValue}'");
        
        
        SetDictionaryValue("Changed", true);
        SetDictionaryValue("District Set", true);
        
        CheckForApplicability();
    }

    private void OnTravelDistanceValueChanged(ChangeEvent<string> changeEvent)
    {
        if (changeEvent.newValue.Equals(changeEvent.previousValue)) return;
        Query.RunNonQuery($"UPDATE {_userDetailsTableName} SET {("Travel_Distance")} = '{changeEvent.newValue}'");
        
        
        SetDictionaryValue("Changed", true);
        SetDictionaryValue("Travel Distance Set", true);
        
        CheckForApplicability();
    }
    
    private void OnAcceptingJobsValueChanged(ChangeEvent<string> changeEvent)
    {
        if (changeEvent.newValue.Equals(changeEvent.previousValue)) return;
        Query.RunNonQuery($"UPDATE {_userDetailsTableName} SET {("Job_Availability")} = '{changeEvent.newValue}'");
        
        
        SetDictionaryValue("Changed", true);
        SetDictionaryValue("Accepting Jobs Set", true);
        
        CheckForApplicability();
    }



    private void CheckForApplicability() => _updateButton.SetEnabled(_setValueDictionary.Values.All(set => set));
    
    private void OnUpdateButtonPressed(ClickEvent clickEvent)
    {
        Query.ApplyUserLoginDetailsChanges(UserData.Department, UserData.Username);
        SetDictionaryValue("Changed", false);
        
        CheckForApplicability();
    }
}
