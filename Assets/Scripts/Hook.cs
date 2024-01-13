using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public float speed = 10f;
    private bool thrown = false;
    public HingeJoint2D joint;

    void Start()
    {
        joint = GetComponent<HingeJoint2D>();
    }
    void FixedUpdate()
    {
        // Move the projectile in the direction of the mouse
        if (!thrown && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Hook Called");
            thrown = true;
            MoveTowardsMouse();
            if (joint != null) joint.enabled = false;
        }
    }

    void MoveTowardsMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 direction = (worldMousePosition - transform.position).normalized;

        // Move the projectile
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
