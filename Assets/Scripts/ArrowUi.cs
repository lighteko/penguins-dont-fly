using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Rendering;

public class ArrowUi : MonoBehaviour
{
    private int direction = 0; // right is 0
    // Start is called before the first frame update
    void Start()
    {
        Hide();
        gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) Show();
        Vector3 vector = gameObject.transform.eulerAngles;
        if (Input.GetKey(KeyCode.S)) {
            if (direction == 0) gameObject.transform.eulerAngles = new Vector3(0, 0, vector.z - 0.1f);
            else gameObject.transform.eulerAngles = new Vector3(0, 0, vector.z + 0.1f);
            if (vector.z-180 >= -80 && vector.z-180 < 0) direction = 0;
            if (vector.z-180 > 0 && vector.z-180 <= 80) direction = 1;
        }
    }

    void FixedUpdate() {
    }

    // Example method to hide the object
    public void Hide() {
        Transform arrow = transform.Find("arrow");
        Renderer renderer = arrow.GetComponent<Renderer>();

        // Create a new material instance
        Material material = new Material(renderer.material);

        // Set the material's alpha value to 0 to make it fully transparent
        Color color = material.color;
        color.a = 0f;
        material.color = color;

        // Apply the new material to the object
        renderer.material = material;
    }

    // Example method to show the object
    public void Show() {
        Transform arrow = transform.Find("arrow");
        Renderer renderer = arrow.GetComponent<Renderer>();

        // Create a new material instance
        Material material = new Material(renderer.material);

        // Set the material's alpha value to 1 to make it fully opaque
        Color color = material.color;
        color.a = 1f;
        material.color = color;

        // Apply the new material to the object
        renderer.material = material;
    }
}
