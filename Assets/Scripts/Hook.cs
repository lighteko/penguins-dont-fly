using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hook : MonoBehaviour
{
    float speed = 20f;
    public HingeJoint2D joint;

    void Start()
    {
        joint = GetComponent<HingeJoint2D>();
    }

    public void CastingHook() {
        MoveTowardsMouse();
        joint.enabled = false;
    }

    void CuttingOff() {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        transform.localPosition = new Vector3(0f,0f,0f);
        joint.enabled = true;
    }

    void MoveTowardsMouse()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = (worldMousePosition - transform.position.ConvertTo<Vector2>()).normalized;

        GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Terrain") && !joint.enabled)
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}
