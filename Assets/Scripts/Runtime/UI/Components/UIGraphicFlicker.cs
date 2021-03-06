﻿using UnityEngine;

namespace GMTK
{
    [RequireComponent(typeof(RectTransform))]
    public class UIGraphicFlicker : MonoBehaviour
    {
        [SerializeField]
        private float duration = 0.5f;

        [SerializeField]
        private LeanTweenType tweenType = LeanTweenType.easeInOutBack;

        [SerializeField]
        private float delay = 0.0f;

        // Private Variables
        private RectTransform rect = null;

        private void OnEnable()
        {
            if (rect == null) rect = GetComponent<RectTransform>();
            LeanTween.cancel(rect);
            LeanTween.textAlpha(rect, 0, duration)
                .setDelay(delay)
                .setLoopPingPong()
                .setEase(tweenType);
        }
    }
}