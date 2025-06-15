using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBehaviour : MonoBehaviour
{
    public float health = 100;

    private PlayerMovement movement;

    void Start() {
        movement = GetComponent<PlayerMovement>();
    }
        
    // Update is called once per frame
    void Update()
    {
        if (health <= 0) {
            Death();
        }
    }

    private void Death() {
        print("you dead, dumbass");
        movement.enabled = false;
    }
}
