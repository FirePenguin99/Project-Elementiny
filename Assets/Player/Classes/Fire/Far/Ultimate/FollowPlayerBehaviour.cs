using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerBehaviour : MonoBehaviour
{
    public Vector3 offsetFromPlayer;

    void Update()
    {
        transform.position = GameStateHandler.instance.player.transform.position + offsetFromPlayer;
    }
}
