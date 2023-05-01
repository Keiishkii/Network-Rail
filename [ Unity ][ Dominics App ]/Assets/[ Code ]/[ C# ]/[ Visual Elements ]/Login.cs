using NetworkRail.SQL;
using UnityEngine;
using UnityEngine.UIElements;

/*
public class Login : VisualElement
{
    public new class UxmlFactory : UxmlFactory<Login, UxmlTraits> { }
    public new class UxmlTraits : VisualElement.UxmlTraits { }

    #region References
    // - - -
        private DropdownField _departmentDropdown;
        private TextField _usernameField;
        private TextField _passwordField;

        private Button _loginButton;
    // - - -
    #endregion



    public Login() => RegisterCallback<GeometryChangedEvent>(OnGeometryChange);

    private void OnGeometryChange(GeometryChangedEvent geometryChangedEvent)
    {
        QueryElements();
        RegisterCallbacks();
        SetDefaults();
    }

    private void QueryElements()
    {
        _departmentDropdown = this.Q<DropdownField>("DepartmentDropdown");
        _usernameField = this.Q<TextField>("UsernameField");
        _passwordField = this.Q<TextField>("PasswordField");
        _loginButton = this.Q<Button>("LoginButton");
    }

    private void RegisterCallbacks()
    {
        _departmentDropdown.RegisterCallback<ChangeEvent<string>>(DepartmentDropdownValueChanged);
        _loginButton.RegisterCallback<ClickEvent>(OnLoginButtonPressed);
    }

    private void SetDefaults()
    {
        _departmentDropdown.choices = SQLManager.QueryListOfDepartments();

        _usernameField.SetEnabled(false);
        _passwordField.SetEnabled(false);
    }



    private void DepartmentDropdownValueChanged(ChangeEvent<string> valueChangedEvent)
    {
        _usernameField.SetEnabled(true);
        _passwordField.SetEnabled(true);
    }

    private void OnLoginButtonPressed(ClickEvent clickEvent)
    {
        (bool successfulLogin, string message) = SQLManager.QueryLogin(_departmentDropdown.value, _usernameField.text, _passwordField.text);
        if (successfulLogin)
        {

        }

        Debug.Log($"[{((successfulLogin) ? ("SUCCESS") : ("FAILURE"))}] Message: {message}");
    }
}
*/