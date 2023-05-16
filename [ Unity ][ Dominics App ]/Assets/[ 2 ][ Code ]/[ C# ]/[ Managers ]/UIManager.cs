using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    #region [ Instance ]
    // - - -
        private static UIManager _instance;
        private static UIManager Instance => _instance ??= FindObjectOfType<UIManager>();
    // - - -
    #endregion
    
    #region [ Behaviour ]
    // - - -
        private const float BaseTransitionDuration = 1f;
        [SerializeField] private AnimationCurve _transitionAnimationCurve;

        public static VisualElement ActiveUIPanel { get; private set; }
    // - - -
    #endregion


    
    
    
    public static void Enable(VisualElement visualElement, bool setAsActiveElement = false)
    {
        if (setAsActiveElement) ActiveUIPanel = visualElement;
        
        visualElement.style.display = DisplayStyle.Flex;
        visualElement.style.opacity = 1f;
        
        visualElement.SetEnabled(true);
    }
    
    public static void Disable(VisualElement visualElement)
    {
        visualElement.style.display = DisplayStyle.None;
        visualElement.style.opacity = 0f;
        
        visualElement.SetEnabled(false);
    }

    public static void Swap(VisualElement oldVisualElement, VisualElement newVisualElement, bool setAsActiveElement = false)
    {
        if (oldVisualElement.name == newVisualElement.name) return;
        if (setAsActiveElement) ActiveUIPanel = newVisualElement;
        
        newVisualElement.style.display = DisplayStyle.Flex;
        oldVisualElement.style.display = DisplayStyle.None;
        
        newVisualElement.style.opacity = 1f;
        oldVisualElement.style.opacity = 0f;
        
        newVisualElement.SetEnabled(true);
        oldVisualElement.SetEnabled(false);
    }
    
    
    
    public static void FadeIn(VisualElement visualElement, float transitionDuration = BaseTransitionDuration, bool setAsActiveElement = false) => Instance.StartCoroutine(Instance.FadeInCoroutine(visualElement, transitionDuration, setAsActiveElement));
    private IEnumerator FadeInCoroutine(VisualElement visualElement, float transitionDuration, bool setAsActiveElement)
    {
        if (setAsActiveElement) ActiveUIPanel = visualElement;
        
        visualElement.style.display = DisplayStyle.Flex;
        visualElement.SetEnabled(true);
        
        for (float timeElapsed = 0; timeElapsed < transitionDuration; timeElapsed += Time.deltaTime)
        {
            visualElement.style.opacity = _transitionAnimationCurve.Evaluate(Mathf.InverseLerp(0, transitionDuration, timeElapsed));
            yield return null;
        }
    }
    
    public static void FadeOut(VisualElement visualElement, float transitionDuration = BaseTransitionDuration) => Instance.StartCoroutine(Instance.FadeOutCoroutine(visualElement, transitionDuration));
    private IEnumerator FadeOutCoroutine(VisualElement visualElement, float transitionDuration)
    {
        visualElement.SetEnabled(false);
        
        for (float timeElapsed = 0; timeElapsed < transitionDuration; timeElapsed += Time.deltaTime)
        {
            visualElement.style.opacity = _transitionAnimationCurve.Evaluate(1f - Mathf.InverseLerp(0, transitionDuration, timeElapsed));
            yield return null;
        }
        
        visualElement.style.display = DisplayStyle.None;
    }
    
    public static void FadeTransition(VisualElement oldVisualElement, VisualElement newVisualElement, bool setAsActiveElement = false, float transitionDuration = BaseTransitionDuration) => Instance.StartCoroutine(Instance.FadeTransitionCoroutine(oldVisualElement, newVisualElement, setAsActiveElement, transitionDuration));
    private IEnumerator FadeTransitionCoroutine(VisualElement oldVisualElement, VisualElement newVisualElement, bool setAsActiveElement, float transitionDuration)
    {
        if (oldVisualElement.name == newVisualElement.name) yield break;
        if (setAsActiveElement) ActiveUIPanel = newVisualElement;
        
        newVisualElement.style.display = DisplayStyle.Flex;
        
        oldVisualElement.SetEnabled(false);
        newVisualElement.SetEnabled(true);

        for (float timeElapsed = 0; timeElapsed < transitionDuration; timeElapsed += Time.deltaTime)
        {
            float transitionProgress = Mathf.InverseLerp(0, transitionDuration, timeElapsed);
            
            newVisualElement.style.opacity = _transitionAnimationCurve.Evaluate(transitionProgress);
            oldVisualElement.style.opacity = _transitionAnimationCurve.Evaluate(1f - transitionProgress);

            yield return null;
        }
        
        oldVisualElement.style.display = DisplayStyle.None;
    }
}
