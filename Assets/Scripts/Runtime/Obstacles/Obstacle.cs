﻿using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public class Obstacle : MonoBehaviour, ICollide, ISelectable
    {
        [SerializeField]
        protected Lane lane = Lane.Middle;

        [SerializeField]
        private TweenPreset a = null;

        // Private Variables
        private List<Renderer> renderers;
        private bool IsSelected = false;

        private void Start()
        {
            renderers = GetComponentsInChildren<Renderer>().ToList();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (GameManager.GodMode) return;
            GameManager.Events.OnRunOver.Raise();
        }

        public void SetLane(Lane lane)
        {
            this.lane = lane;
        }

        public void OnClick()
        {
            if (!IsSelected)
               Select();
            else
                Deselect();
        }

        public void Select()
        {
            LeanTween.cancel(gameObject);
            if (IsSelected) return;
            LeanTween.moveY(gameObject, a.to, a.time).setEase(a.ease);
            IsSelected = true;
        }

        public void Deselect()
        {
            LeanTween.cancel(gameObject);
            if (!IsSelected) return;
            LeanTween.moveY(gameObject, 0, a.time / 2f).setEase(a.ease);
            IsSelected = false;
        }

        public void MoveTo(Lane lane, Action onComplete)
        {
            if (!IsSelected) return;
            LeanTween.cancel(gameObject);
            LeanTween.moveZ(gameObject, -1 * Consts.LANE_SEPARATION * (int)lane, a.time)
                .setOnComplete(onComplete)
                .setEase(a.ease);
        }

    }
}