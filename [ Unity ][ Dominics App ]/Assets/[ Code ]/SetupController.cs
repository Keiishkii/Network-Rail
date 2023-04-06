using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupController : MonoBehaviour
{
    #region References
    // - - -
        [SerializeField] private GameObject _loadPanel;
    // - - -
    #endregion
    
    
    
    private void Start() => UIManager.FadePanelIn(ref _loadPanel);
}
