using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public Transform target;
    private bool thrown = false;
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.Find("hook");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!thrown && Input.GetMouseButton(0))
        {
            Debug.Log("FishingRod Called");
            GenerateString();
            target.gameObject.SendMessage("CastingHook");
            thrown = true;
        }

        if (thrown)
        {
            lineRenderer.SetPosition(0, gameObject.transform.position);
            lineRenderer.SetPosition(1, target.position);
        }
    }

    void GenerateString()
    {
        SpringJoint2D springJoint = gameObject.AddComponent<SpringJoint2D>();
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.025f;
        lineRenderer.endWidth = 0.025f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;

        // Attach the SpringJoint2D to the startPoint's Rigidbody2D
        springJoint.connectedBody = target.GetComponent<Rigidbody2D>();

        // Set other SpringJoint2D properties as needed
        springJoint.autoConfigureDistance = false;
        springJoint.distance = 3f;
        springJoint.frequency = 1f;  // Adjust the frequency as needed
        springJoint.dampingRatio = 0.5f;  // Adjust the damping ratio as needed
    }

    void WindString()
    {
        SpringJoint2D springJoint = GetComponent<SpringJoint2D>();
        float originalDistance = springJoint.distance;
        if (originalDistance / 5 >= 3f / 4f) Destroy(springJoint);
        else springJoint.distance = originalDistance - 0.5f;
    }

    void LoosenString()
    {

    }
}
