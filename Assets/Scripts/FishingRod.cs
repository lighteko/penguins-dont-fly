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
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.Find("hook");
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        attached = target.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static;
        float distance = (target.position - transform.position).magnitude;
        if (distance > 7f) {
            Destroy(GetComponent<HingeJoint2D>());
            Destroy(GetComponent<LineRenderer>());
            target.SendMessage("CuttingOff");
            thrown = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!thrown && Input.GetMouseButton(0))
        {
            GenerateString();
            target.SendMessage("CastingHook");
            thrown = true;
        }

        if (thrown && attached && Input.GetMouseButton(1)) {
            anim.SetBool("isWinding", true);
            WindString();
        } else anim.SetBool("isWinding", false);


        if (thrown && !attached && Input.GetMouseButton(1)) {
            Destroy(GetComponent<HingeJoint2D>());
            Destroy(GetComponent<LineRenderer>());
            target.SendMessage("CuttingOff");
            thrown = false;
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
        springJoint.distance = 4f;
        springJoint.frequency = 5f;  // Adjust the frequency as needed
        springJoint.dampingRatio = 1f;  // Adjust the damping ratio as needed
    }

    void WindString()
    {
        float distance = (target.position - transform.position).magnitude;
        if (distance < 2f) {
            Destroy(GetComponent<HingeJoint2D>());
            Destroy(GetComponent<LineRenderer>());
            target.SendMessage("CuttingOff");
            thrown = false;
        }
        if (TryGetComponent<SpringJoint2D>(out var springJoint)) springJoint.distance -= 0.02f;
        transform.parent.SendMessage("MoveToTarget");
    }
}