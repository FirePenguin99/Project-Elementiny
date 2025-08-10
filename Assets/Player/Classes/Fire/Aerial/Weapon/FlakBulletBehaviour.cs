using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakBulletBehaviour : MonoBehaviour
{
    [SerializeField] private SphereCollider proximityCollider;

    [SerializeField] private float minRadius = 0;
    [SerializeField] private float maxRadius = 4;
    [SerializeField] private float timeToMaxRadius = 2;
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        if (timer > timeToMaxRadius) return;

        timer += Time.deltaTime;
        float lerpValue = (timer / timeToMaxRadius) * (maxRadius - minRadius) + minRadius;
        proximityCollider.radius = lerpValue;
    }
}
