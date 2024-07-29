using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : EffectState
{
    private Material ShaderMat;
    private void Awake()
    {
        ShaderMat = GetComponent<Material>();
    }

    public override void Main()
    {
        
    }
}
