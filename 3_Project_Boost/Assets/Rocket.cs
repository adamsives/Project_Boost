using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidbody;
    Transform transform;
    [SerializeField] float rcsThrust = 250f;
    [SerializeField] float rcsMainThrust = 50f;

    AudioSource audio;
    // Use this for initialization
    void Start()
    {
        //print("started!");
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Thrust();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":

                print("collided");
                break;
            case "Fuel":
                print("Fuel");
                break;
            default:
                print("DEAD!");
                break;
        }
    }

    private void Rotate()
    {
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * rotationThisFrame);
        }

    }

    private void Thrust()
    {

        rigidbody.freezeRotation = true;

        float forwardThrustThisFrame = rcsMainThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * forwardThrustThisFrame);
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }
        else
        {
            audio.Stop();
        }
        rigidbody.freezeRotation = false;
    }
}
