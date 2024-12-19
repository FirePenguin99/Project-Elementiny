using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixMovementBehaviour : MonoBehaviour
{
    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;
    private float currentAmplitude = 0;

    [SerializeField] private float orbNumber;
    [SerializeField] private float totalOrbsInSystem;

    [SerializeField] private float phase;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float amplitudeIncreaseRate;

    [SerializeField] private Vector3 startingPosition;

    float elapsedTime = 0;

    void Start() {
        phase = ((orbNumber * 2) / totalOrbsInSystem) * Mathf.PI;

        startingPosition = transform.position;
    }

    void FixedUpdate()
    {
        print(currentAmplitude);
        if (currentAmplitude < amplitude) {
            currentAmplitude += amplitudeIncreaseRate;
        }

        elapsedTime += Time.deltaTime;

        Vector3 orbPosition = new Vector3
        (
            Mathf.Sin((Time.time * frequency) + phase) * currentAmplitude, 
            Mathf.Cos((Time.time * frequency)  + phase) * currentAmplitude,
            elapsedTime * movementSpeed
        );

        transform.position = startingPosition + transform.rotation * orbPosition;
    }
}
