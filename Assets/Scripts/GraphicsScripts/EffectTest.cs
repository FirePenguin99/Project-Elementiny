using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTest : MonoBehaviour
{
    private Material TestMaterial;
    public EffectState TestState;
    void Start()
    {
        TestState = new HitEffect();
        TestMaterial = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        TestState.Main(TestMaterial);
    }
}
