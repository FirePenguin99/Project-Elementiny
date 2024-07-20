using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameParticleBehaviour : MonoBehaviour
{
    public ParticleSystem burningParticleSystem;
    
    void Start()
    {
        // flameParticleSystem = GetComponent<ParticleSystem>();
        // flameParticleSystem.Play();
    }

    public void EndFlame() {
        var em = burningParticleSystem.emission;  //wtf
        em.rateOverTime = 0;

        InvokeRepeating(nameof(ParticleCheck), 0f, 2f);
    }

    private void ParticleCheck() {
        if (burningParticleSystem.particleCount == 0) {
            Destroy(this.gameObject);
        }
    }
}
