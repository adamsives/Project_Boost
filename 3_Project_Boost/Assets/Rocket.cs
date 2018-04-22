using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        print("started!");
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
            print("su-be-su plessed");
        }

        if (Input.GetKey(KeyCode.A))
        {
            print("lotating reft plessed");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            print("lotating light plessed");
        }

    }
}
