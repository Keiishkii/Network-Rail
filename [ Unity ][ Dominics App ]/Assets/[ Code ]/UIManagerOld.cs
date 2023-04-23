using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerOld : MonoBehaviour
{
    #region Instance
    // - - -
        private static UIManagerOld _instance;
        public static UIManagerOld Instance => _instance ??= FindObjectOfType<UIManagerOld>();
    // - - -
    #endregion
    
    #region Instance
    // - - -
        private static readonly float TransitionDuration = 0.5f;
        [SerializeField] private AnimationCurve _transitionAnimationCurve; 
    // - - -
    #endregion
    
    
    
    
    
    public static void FadePanelIn(ref GameObject fadeInPanel) => Instance.StartCoroutine(Instance.FadeInOverTime(fadeInPanel));
    private IEnumerator FadeInOverTime(GameObject fadeInPanel)
    {
        fadeInPanel.SetActive(true);
        
        CanvasGroup fadeInPanelCanvasGroup = fadeInPanel.GetComponent<CanvasGroup>();
        fadeInPanelCanvasGroup.interactable = true;
        
        for (float timeElapsed = 0; timeElapsed < TransitionDuration; timeElapsed += Time.deltaTime)
        {
            float transitionProgress = Mathf.InverseLerp(0, TransitionDuration, timeElapsed);
            fadeInPanelCanvasGroup.alpha = _transitionAnimationCurve.Evaluate(transitionProgress);

            yield return null;
        }
    }
    
    public static void FadePanelOut(ref GameObject fadeOutPanel) => Instance.StartCoroutine(Instance.FadeOutOverTime(fadeOutPanel));
    private IEnumerator FadeOutOverTime(GameObject fadeOutPanel)
    {
        CanvasGroup fadeOutPanelCanvasGroup = fadeOutPanel.GetComponent<CanvasGroup>();
        fadeOutPanelCanvasGroup.interactable = false;
        
        for (float timeElapsed = 0; timeElapsed < TransitionDuration; timeElapsed += Time.deltaTime)
        {
            float transitionProgress = Mathf.InverseLerp(0, TransitionDuration, timeElapsed);
            fadeOutPanelCanvasGroup.alpha = _transitionAnimationCurve.Evaluate(1f - transitionProgress);

            yield return null;
        }
        
        fadeOutPanel.SetActive(false);
    }
    
    public static void TransitionPanels(ref GameObject fadeInPanel, ref GameObject fadeOutPanel) => Instance.StartCoroutine(Instance.TransitionOverTime(fadeInPanel, fadeOutPanel));
    private IEnumerator TransitionOverTime(GameObject fadeInPanel, GameObject fadeOutPanel)
    {
        fadeInPanel.SetActive(true);
        
        CanvasGroup fadeInPanelCanvasGroup = fadeInPanel.GetComponent<CanvasGroup>();
        CanvasGroup fadeOutPanelCanvasGroup = fadeOutPanel.GetComponent<CanvasGroup>();
        
        fadeInPanelCanvasGroup.interactable = true;
        fadeOutPanelCanvasGroup.interactable = false;
        
        for (float timeElapsed = 0; timeElapsed < TransitionDuration; timeElapsed += Time.deltaTime)
        {
            float transitionProgress = Mathf.InverseLerp(0, TransitionDuration, timeElapsed);
            
            fadeInPanelCanvasGroup.alpha = _transitionAnimationCurve.Evaluate(transitionProgress);
            fadeOutPanelCanvasGroup.alpha = _transitionAnimationCurve.Evaluate(1f - transitionProgress);

            yield return null;
        }
        
        fadeOutPanel.SetActive(false);
    }
}
