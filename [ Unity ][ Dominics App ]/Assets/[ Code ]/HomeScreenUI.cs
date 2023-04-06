using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreenUI : MonoBehaviour
{
    #region References
    // - - -
        [SerializeField] private GameObject _viewActiveRequestsPanel;
        [SerializeField] private GameObject _createRequestPanel;
        [SerializeField] private GameObject _viewRequestHistoryPanel;
        private GameObject _gameObject;
    
        [SerializeField] private Button _viewActiveRequestsButton;
        [SerializeField] private Button _createRequestButton;
        [SerializeField] private Button _viewRequestHistoryButton;
    // - - -
    #endregion


    
    
    
    private void Awake() => _gameObject = gameObject;
    
    private void OnEnable()
    {
        _viewActiveRequestsButton.onClick.AddListener(OnViewActiveRequestButtonPressed);
        _createRequestButton.onClick.AddListener(OnCreateRequestButtonPressed);
        _viewRequestHistoryButton.onClick.AddListener(OnViewRequestHistoryButtonPressed);
    }

    private void OnDisable()
    {
        _viewActiveRequestsButton.onClick.RemoveListener(OnViewActiveRequestButtonPressed);
        _createRequestButton.onClick.RemoveListener(OnCreateRequestButtonPressed);
        _viewRequestHistoryButton.onClick.RemoveListener(OnViewRequestHistoryButtonPressed);
    }

    

    private void OnViewActiveRequestButtonPressed()
    {
        UIManager.TransitionPanels(ref _viewActiveRequestsPanel, ref _gameObject);
    }
    
    private void OnCreateRequestButtonPressed()
    {
        UIManager.TransitionPanels(ref _createRequestPanel, ref _gameObject);
    }
    
    private void OnViewRequestHistoryButtonPressed()
    {
        UIManager.TransitionPanels(ref _viewRequestHistoryPanel, ref _gameObject);
    }
}
