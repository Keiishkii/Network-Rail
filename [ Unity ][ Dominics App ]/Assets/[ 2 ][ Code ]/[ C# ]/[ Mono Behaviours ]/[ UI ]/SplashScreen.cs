using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SplashScreen : _UILayoutInterface
{
    #region [ Document References ]
    // - - -
        private UIDocument _document;
        private VisualElement _root;
        
        private VisualElement _splashScreenLayout;
        private VisualElement _userLoginLayout;
    // - - -
    #endregion

    #region [ References ]
    // - - -
        private UserLogin _userLogin;
    // - - -
    #endregion

    
    
    

    private void Awake()
    {
        _document = FindObjectOfType<UIDocument>();
        _root = _document.rootVisualElement;

        _splashScreenLayout = _root.Q<VisualElement>("_SplashScreenLayout");
        _userLoginLayout    = _root.Q<VisualElement>("_UserLoginLayout");

        _userLogin = FindObjectOfType<UserLogin>();
    }

    private void Start() => StartCoroutine(Startup());

    
    
    private IEnumerator Startup()
    {
        UIManager.FadeIn(_splashScreenLayout, 2f);
        
        yield return new WaitForSeconds(4f);
        _userLogin.ResetUIDefaults();
        
        UIManager.FadeTransition(_splashScreenLayout, _userLoginLayout, true);
    }
}
