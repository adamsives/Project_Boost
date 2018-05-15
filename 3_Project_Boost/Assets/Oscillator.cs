using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class Oscillator : MonoBehaviour {

    [SerializeField] private Vector3 movementVector = new Vector3(10.0f, 0.0f, 0.0f);

    [Range(0, 1)] [SerializeField] float movementFactor;

    [SerializeField] float period = 2f;
    
    Vector3 startPos;

    // Use this for initialization
    void Start () {
        startPos = transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {

        if (period <= Mathf.Epsilon) { return; };

        float cycle = Time.time/period;

        const float TAU = Mathf.PI * 2f;

        float rawSinWave = Mathf.Sin(cycle * TAU);

        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movementFactor * movementVector;
        transform.position = startPos + offset;
    }
}
