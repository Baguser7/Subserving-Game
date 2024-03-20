using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; //continue growing over time
        const float tau = Mathf.PI * 2; // tau = 2pi
        float rawSinWave = Mathf.Sin(cycles * tau); // oscillating back and forth from 1 to -1

        movementFactor = (rawSinWave + 1f) / 2f; // recalculate to oscillate from 1 to 2

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
