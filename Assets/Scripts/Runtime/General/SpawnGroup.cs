﻿using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public abstract class SpawnGroup<Type, ObjectEnum> : MonoBehaviour
        where Type : Component, ISelectable
        where ObjectEnum : Enum
    {
        public Lane lane = Lane.Middle;

        [SerializeField]
        protected ObjectEnum activeObject;

        // Private Variables
        protected List<Type> objects;
        protected FloatVariable speed = null;
        protected FloatVariable lerpSpeed = null;

        public virtual void Initialize(Lane lane)
        {
            speed = Assets.Instance.Speed;
            lerpSpeed = Assets.Instance.LerpSpeed;

            this.lane = lane;

            if (objects is null || objects.Count == 0)
                objects = gameObject.GetComponentsInChildren<Type>(true).ToList();

            UnactivateAll();
            ActivateItem(Utilities.GetRandomEnum<ObjectEnum>());
        }

        protected void FixedUpdate()
        {
            if (!GameManager.CanReadInput) return;
            transform.position = Vector3.Lerp(
                a: transform.position,
                b: transform.position.With(x: transform.position.x - speed.value),
                t: Time.fixedDeltaTime * lerpSpeed.value
            );
        }

        protected void ActivateItem(ObjectEnum item)
        {
            int enumToInt = (int)Enum.Parse(typeof(ObjectEnum), item.ToString());

            objects[enumToInt].gameObject.transform.position.With(
                z: -1 * (float)lane * Consts.LANE_SEPARATION
            );

            objects[enumToInt].gameObject.SetActive(true);
            objects[enumToInt].SetLane(lane);

            activeObject = item;
        }

        public void DeactivateItem(ObjectEnum item)
        {
            int enumToInt = (int)Enum.Parse(typeof(ObjectEnum), item.ToString());
            objects[enumToInt].gameObject.SetActive(false);
        }

        protected void UnactivateAll()
        {
            foreach (Type item in objects)
                item.gameObject.SetActive(false);
        }

    }
}