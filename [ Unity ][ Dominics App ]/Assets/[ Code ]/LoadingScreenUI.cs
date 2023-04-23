using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenUI : MonoBehaviour
{
    #region References
    // - - -   
        [SerializeField] private GameObject _homeScreenPanel;
        private GameObject _gameObject;
        
        [SerializeField] private ProgressBar _progressBar;
    // - - -
    #endregion
    
    #region Behaviour
    // - - -   
        private static readonly float LoadDuration = 3f; 
        [SerializeField] private AnimationCurve _loadingAnimationCurve; 
    // - - -
    #endregion


    private void Awake() => _gameObject = gameObject;
    private void Start() => StartCoroutine(SimulateLoad());
    
    private IEnumerator SimulateLoad()
    {
        for (float timeElapsed = 0; timeElapsed < LoadDuration; timeElapsed += Time.deltaTime)
        {
            _progressBar.FillAmount = _loadingAnimationCurve.Evaluate(Mathf.InverseLerp(0, LoadDuration, timeElapsed));
            yield return null;
        }
        
        UIManagerOld.TransitionPanels(ref _homeScreenPanel, ref _gameObject);
    }
}
