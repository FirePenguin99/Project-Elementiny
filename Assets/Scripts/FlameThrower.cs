
using UnityEngine;

public class FlameThrower : MonoBehaviour
{
    private ParticleSystem FlamePS;
    // Start is called before the first frame update
    void Start()
    {
        FlamePS = FlamePS != null ? FlamePS : new ParticleSystem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
