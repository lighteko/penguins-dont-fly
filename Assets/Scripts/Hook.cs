using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            Vector2 forceDirection = new Vector2(5,4);
            GetComponent<Rigidbody2D>().AddForce(forceDirection * 5f);
        }
    }
}
