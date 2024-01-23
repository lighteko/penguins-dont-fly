using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public Transform target;
    public GameObject prefab;
    private bool thrown = false;
    
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

        if (Input.GetAxis("Mouse ScrollWheel") < 0) WindString();
        if (Input.GetAxis("Mouse ScrollWheel") > 0) LoosenString();
    }

    void GenerateString()
    {
        GameObject cursor = gameObject;
        for (int i = 0; i < 20; i++) {
            GameObject stringTip = Instantiate(prefab, transform);
            if (i == 0) stringTip.GetComponent<HingeJoint2D>().connectedBody = gameObject.GetComponent<Rigidbody2D>();
            else stringTip.GetComponent<HingeJoint2D>().connectedBody = cursor.GetComponent<Rigidbody2D>();
            cursor = stringTip;
            if (i == 19) target.GetComponent<HingeJoint2D>().connectedBody = cursor.GetComponent<Rigidbody2D>();
        }
    }

    void WindString()
    {

    }

    void LoosenString()
    {

    }
}
