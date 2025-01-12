using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixMovementBehaviour : MonoBehaviour
{
    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;
    private float currentAmplitude = 0;
    float interp = 0;

    public float orbNumber;
    public float totalOrbsInSystem;

    [SerializeField] private float phase;

    public float movementSpeed;
    [SerializeField] private float amplitudeIncreaseRate;

    [SerializeField] private Vector3 startingPosition;

    [SerializeField] private float elapsedTime = 0;

    void Start() {
        phase = 2 * (orbNumber / totalOrbsInSystem) * Mathf.PI;

        startingPosition = transform.position;
    }

    void FixedUpdate()
    {
        elapsedTime += Time.fixedDeltaTime;

        interp += amplitudeIncreaseRate;
        currentAmplitude = Mathf.SmoothStep(0, amplitude, interp);

        Vector3 orbPosition = new Vector3
        (
            Mathf.Cos((elapsedTime * frequency)  + phase) * currentAmplitude,
            Mathf.Sin((elapsedTime * frequency)  + phase) * currentAmplitude,
            elapsedTime * movementSpeed
        );

        // print("sin: " + Mathf.Sin((elapsedTime * frequency)  + phase) * currentAmplitude + " | cos: " + Mathf.Cos((elapsedTime * frequency)  + phase) * currentAmplitude);

        transform.position = startingPosition + transform.rotation * orbPosition;
    }
}
