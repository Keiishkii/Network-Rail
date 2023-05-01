using UnityEngine;
using UnityEngine.UIElements;

/*
public class SplashScreen : VisualElement
{
    public new class UxmlFactory : UxmlFactory<SplashScreen, UxmlTraits> { }
    public new class UxmlTraits : VisualElement.UxmlTraits { }

    private UIDocument _document;
    private UIRootElement _rootElement;

    public SplashScreen() => RegisterCallback<GeometryChangedEvent>(OnGeometryChange);

    private void OnGeometryChange(GeometryChangedEvent geometryChangedEvent)
    {
        _document = Object.FindObjectOfType<UIDocument>();
        _rootElement = _document.rootVisualElement.Q<UIRootElement>();
        
        UIManager.TransitionPanels(_rootElement.SplashScreen, _rootElement.Login);
    }
}
*/