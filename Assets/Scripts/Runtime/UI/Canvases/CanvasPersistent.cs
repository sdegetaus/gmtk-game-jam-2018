﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public class CanvasPersistent : CanvasLogic
    {
        [Header("Cinematic Effect")]
        [SerializeField]
        private UICinematicEffect cinematicEffect = null;

        private void Start()
        {
            GameManager.Events.OnRunStarted.RegisterListener(OnRunStarted);
            GameManager.Events.OnRunOver.RegisterListener(OnRunOver);
        }

        private void OnRunStarted()
        {
            cinematicEffect.FadeOut();
        }

        private void OnRunOver()
        {
            cinematicEffect.FadeIn();
        }
    }
}