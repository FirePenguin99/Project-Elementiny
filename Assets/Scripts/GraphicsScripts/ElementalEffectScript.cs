
using UnityEngine;

public class ElementalEffectScript : MonoBehaviour
{
    public EffectState CurrentEffectState;
   
    void Update()
    {
        CurrentEffectState.Main();
    }
}
