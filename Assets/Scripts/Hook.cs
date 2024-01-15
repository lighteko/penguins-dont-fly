using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public float speed = 10f;
    public HingeJoint2D joint;

    void Start()
    {
        joint = GetComponent<HingeJoint2D>();
    }

    void Update() {
        if (Input.GetMouseButton(1)) 
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    public void CastingHook() {
        MoveTowardsMouse();
        GetComponent<HingeJoint2D>().enabled = false;
    }

    void MoveTowardsMouse()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = (worldMousePosition - transform.position.ConvertTo<Vector2>()).normalized;

        // Move the projectile
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Terrain"))
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

    }
}
