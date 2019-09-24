﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, ICollide {

    public void OnTriggerEnter(Collider other) {
        GameManager.instance.RunOver();
    }

}