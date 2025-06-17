using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnCallBehaviour : MonoBehaviour
{
    public void Delete()
    {
        Destroy(this.gameObject);
    }
}
