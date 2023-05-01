using UnityEngine;
using UnityEngine.UIElements;

/*
public class UserPortal : VisualElement
{
    public new class UxmlFactory : UxmlFactory<UserPortal, UxmlTraits> { }
    public new class UxmlTraits : VisualElement.UxmlTraits { }

    private UIDocument _document;
    private UIRootElement _rootElement;
    
    public UserPortal() => RegisterCallback<GeometryChangedEvent>(OnGeometryChange);

    private void OnGeometryChange(GeometryChangedEvent geometryChangedEvent)
    {
        _document = Object.FindObjectOfType<UIDocument>();
        _rootElement = _document.rootVisualElement as UIRootElement;
        
        UIManager.TransitionPanels(_rootElement.SplashScreen, _rootElement.Login);
    }
}
*/