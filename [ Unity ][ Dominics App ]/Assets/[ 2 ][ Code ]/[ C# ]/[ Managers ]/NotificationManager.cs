using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NotificationManager : MonoBehaviour
{
    #region [ Instance ]
    // - - -
        private static NotificationManager _instance;
        public static NotificationManager Instance => _instance ? _instance : _instance = FindObjectOfType<NotificationManager>();
    // - - -
    #endregion
    
    #region [ Document References ]
    // - - -
        private UIDocument _document;
        
        private VisualElement _root;
        private VisualElement _notification;
    // - - -
    #endregion

    #region [ References ]
    // - - -
        private Button _closeButton;
        
        private Label _notificationTitle;
        private Label _notificationText;
    // - - -
    #endregion


    
    
    
    private void Awake()
    {
        _document = FindObjectOfType<UIDocument>();
        _root = _document.rootVisualElement;

        _notification = _root.Q<VisualElement>("Notification");
		
        _closeButton = _notification.Q<Button>("CloseButton");
        _notificationTitle = _notification.Q<Label>("NotificationTitle");
        _notificationText = _notification.Q<Label>("NotificationText");
    }

    private void OnEnable()
    {
        _closeButton.RegisterCallback<ClickEvent>(OnNotificationCloseButtonPressed);
    }

    private void OnDisable()
    {
        _closeButton.UnregisterCallback<ClickEvent>(OnNotificationCloseButtonPressed);
    }

    
    
    public void ShowNotification(string title, string text)
    {
        UIManager.Enable(_notification);
        
        _notificationTitle.text = title;
        _notificationText.text = text;
    }

    private void OnNotificationCloseButtonPressed(ClickEvent clickEvent)
    {
        UIManager.Disable(_notification);
    }
}
