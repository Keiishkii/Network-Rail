using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SplashScreen : MonoBehaviour
{
    #region [ Document References ]
    // - - -
        private UIDocument _document;
        
        private VisualElement _root;
        private VisualElement _splashScreen;
        private VisualElement _userLogin;
    // - - -
    #endregion

    
    
    

    private void Awake()
    {
        _document = FindObjectOfType<UIDocument>();
        _root = _document.rootVisualElement;

        _splashScreen = _root.Q<VisualElement>("SplashScreen");
        _userLogin = _root.Q<VisualElement>("UserLogin");
    }

    private void Start() => StartCoroutine(Startup());

    
    
    private IEnumerator Startup()
    {
        UIManager.FadeIn(_splashScreen, 2f);
        
        yield return new WaitForSeconds(4f);
        
        UIManager.FadeTransition(_splashScreen, _userLogin, true);
    }
}
