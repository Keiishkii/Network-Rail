using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveRequestsUI : MonoBehaviour
{
    #region References
    // - - -
        [SerializeField] private GameObject _homeScreenPanel;
        private GameObject _gameObject;
    
        [SerializeField] private Button _returnButton;
    // - - -
    #endregion


    
    
    
    private void Awake() => _gameObject = gameObject;
    
    private void OnEnable()
    {
        _returnButton.onClick.AddListener(OnReturnButtonPressed);
    }

    private void OnDisable()
    {
        _returnButton.onClick.RemoveListener(OnReturnButtonPressed);
    }

    

    private void OnReturnButtonPressed()
    {
        UIManagerOld.TransitionPanels(ref _homeScreenPanel, ref _gameObject);
    }
}
