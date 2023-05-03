using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    #region References
    // - - -
        [SerializeField] private RectTransform _containerRectTransform;
        [SerializeField] private RectTransform _fillRectTransform;
    // - - - 
    #endregion

    #region Behaviour
    // - - -
        private float _fillAmount;
        public float FillAmount
        {
            get => _fillAmount;
            set
            {
                _fillAmount = Mathf.Clamp01(value);
                _fillRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _containerRectTransform.rect.width * _fillAmount);
            }
        }
    // - - -
    #endregion
}
