﻿using UnityEngine;
using System.Collections;

public class LookAtAnimation : MonoBehaviour {

    void Update() {
        Vector3 diff = PlayerController.instance.GetPlayerPos() - transform.position;
        diff.Normalize();

        transform.up = diff;

        //float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
