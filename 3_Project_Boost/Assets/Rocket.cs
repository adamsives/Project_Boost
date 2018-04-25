using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidbody;
    Transform transform;

    // Use this for initialization
    void Start()
    {
        //print("started!");
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        //print("Start:" + transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //print("ProcessInput.space():" + transform.position.y);
            rigidbody.AddRelativeForce(Vector3.up);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back);
        }

    }
}
