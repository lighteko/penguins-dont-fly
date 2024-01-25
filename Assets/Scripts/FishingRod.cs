using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

public class FishingRod : MonoBehaviour
{
    public Transform target;
    private bool thrown = false;
    private LineRenderer lineRenderer;

    private bool attached = false;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.Find("hook");
    }
    void Update()
    {
        attached = target.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!thrown && Input.GetMouseButton(0))
        {
            Debug.Log("FishingRod Called");
            GenerateString();
            target.SendMessage("CastingHook");
            thrown = true;
        }

        if (thrown && attached && Input.GetMouseButton(1))
        {
            WindString();
        }

        if (thrown && lineRenderer != null)
        {
            lineRenderer.SetPosition(0, transform.position);
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
        float distance = (target.position - transform.position).magnitude;
        if (distance < 1f) {
            Destroy(GetComponent<HingeJoint2D>());
            Destroy(GetComponent<LineRenderer>());
            target.SendMessage("CuttingOff");
        }
        transform.parent.SendMessage("MoveToTarget");
    }
}