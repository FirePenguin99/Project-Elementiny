using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableBehaviour : MonoBehaviour
{
    public UnityEvent interactMethods;

    public void onInteract()
    {
        interactMethods?.Invoke(); // need to find a way to pass the player reference into an event, but also optionally? As some of the functions in the event wont take in a GO arguement
    }
}
