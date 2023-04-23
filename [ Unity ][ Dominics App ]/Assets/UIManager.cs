using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Instance
    // - - -
        private static UIManager _instance;
        public static UIManager Instance => _instance ??= FindObjectOfType<UIManager>();
    // - - -
    #endregion
    
    #region Instance
    // - - -
        private static readonly float TransitionDuration = 0.5f;
        [SerializeField] private AnimationCurve _transitionAnimationCurve; 
    // - - -
    #endregion
    
    
    
    
    
    public static void TransitionPanels(ref GameObject fadeInPanel, ref GameObject fadeOutPanel) => Instance.StartCoroutine(Instance.TransitionOverTime(fadeInPanel, fadeOutPanel));
    private IEnumerator TransitionOverTime(GameObject fadeInPanel, GameObject fadeOutPanel)
    {
        yield return null;
    }
}
