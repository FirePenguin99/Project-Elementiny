using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tryingSinAndCos : MonoBehaviour
{
    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;

    [SerializeField] private float orbNumber;
    [SerializeField] private float totalOrbsInSystem;

    [SerializeField] private float phase;

    [SerializeField] private float speedFactor;

    [SerializeField] private Transform ReferenceThingy;
    [SerializeField] private float futhestDistance;
    [SerializeField] private Vector2 xAndY;

    [SerializeField] private Vector3 directionVector;

    [SerializeField] private Vector3 startingPosition;

    float elapsedTime = 0;

    void Start() {
        // float phaseUnit = 360/totalOrbsInSystem * Mathf.Deg2Rad;
        // phase = phaseUnit * orbNumber;
        phase = ((orbNumber * 2) / totalOrbsInSystem) * Mathf.PI;

        // Max amplitude in World Space seems to be (Amplitude / Frequency) * 10. E.g. (2 / 1) * 10 = 5, which means it will reach 5 in World Space. No idea fucking why though, I feel like I've stumbled across a Wild Rune.
    
        startingPosition = transform.position;
    }

    void FixedUpdate()
    {
        // Over the course of trying to make this dogshit, I had the orbs rotate in a circle like expected, or the orbs pulsing in and out like me and CJ Bounds.
        // I found that its never a problem with the maths, its a problem with using "transform.position =" OR "transform.position +=".
        // = causes the intended behaviour, but is very hard to change mid-flight, as the position is being SET. 
        // += causes the bullshiet, but can be changed mid-flight, as its being ADDED.
        // If only I could find the maths to alter the starting position of the += variants so that they all orbit the same middle point, resembling the intented = variant. BUT I FUCKING CAAANT DDDDDDDDDD:
        // MORE BLOOD MUST BE SHED
        // MORE BLOOD MUST BE SHED
        // MORE BLOOD MUST BE SHED
        elapsedTime += Time.deltaTime;

        Vector3 orbPosition = new Vector3
        (
            Mathf.Sin((Time.time * frequency) + phase) * amplitude, 
            Mathf.Cos((Time.time * frequency)  + phase) * amplitude,
            elapsedTime * speedFactor
            // 0
        );

        // transform.localPosition = orbPosition;
        // transform.position = Vector3.Cross(orbPosition, new Vector3(0, 1, 0));

        // transform.position = Quaternion.Euler(new Vector3(45,180,90)) * orbPosition;
        // transform.position = Quaternion.Euler(new Vector3(transform.rotation.x,transform.rotation.y,transform.rotation.z)) * orbPosition;
        transform.position = startingPosition + transform.rotation * orbPosition;
        // transform.position = orbPosition;
    }
}
