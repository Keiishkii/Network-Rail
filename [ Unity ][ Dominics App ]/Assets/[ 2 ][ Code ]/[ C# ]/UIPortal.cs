using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIPortal : MonoBehaviour
{
    #region Components
    // - - -
        private UIDocument _UIDocument;
    // - - -
    #endregion
    
    #region References
    // - - -
        private VisualElement _rootVisualElement;
    // - - -
    #endregion
    
    
    
    private void Awake()
    {
        _UIDocument = GetComponent<UIDocument>();
        _rootVisualElement = _UIDocument.rootVisualElement;
    }
}
