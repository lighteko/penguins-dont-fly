using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public Transform target;
    private bool thrown = false;
    // Start is called before the first frame update
    void Start()
    {
        target = gameObject.transform.Find("hook");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!thrown && Input.GetMouseButton(0)) { 
            generateString();
            thrown = true;
        }
    }

    void generateString()
    {
        SpringJoint2D springJoint = gameObject.AddComponent<SpringJoint2D>();

        // Attach the SpringJoint2D to the startPoint's Rigidbody2D
        springJoint.connectedBody = target.GetComponent<Rigidbody2D>();

        // Set other SpringJoint2D properties as needed
        springJoint.autoConfigureDistance = false;
        springJoint.distance = 10f;
        springJoint.frequency = 1f;  // Adjust the frequency as needed
        springJoint.dampingRatio = 0.5f;  // Adjust the damping ratio as needed
    }
}
