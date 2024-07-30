using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : EffectState
{
    private static readonly int Property = Shader.PropertyToID("_CurrentRadius");


    public override void Main(Material ShaderMat)
    {
       
        var temp =ShaderMat.GetFloat(Property);
        ShaderMat.SetFloat(Property,(temp + Time.deltaTime) %1);
    }
}
