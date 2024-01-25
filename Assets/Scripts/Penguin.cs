using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Penguin : MonoBehaviour
{

    public GameObject target;
    public float speed = 10f;

    void Start() {
        target = transform.Find("fishing rod").transform.Find("hook").gameObject;
    }

    void MoveToTarget() {
        Vector2 targetPosition = target.transform.position;
        Vector2 currentPosition = transform.position;
        Vector2 direction = targetPosition - currentPosition;

        // Move the projectile
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * (speed *= 0.9f), ForceMode2D.Impulse);
    }
}
