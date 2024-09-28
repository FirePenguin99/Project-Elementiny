using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShatterBehaviour : MonoBehaviour
{
    public Camera playerCam;
    public LayerMask projectileLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            ProcShatter();
        }
    }

    // if fart then .Shatter

    public void ProcShatter() {
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // spawns a ray in the middle of the screen

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, projectileLayerMask)) {
            ColdDebuffBehaviour debuff = hit.transform.GetComponent<ColdDebuffBehaviour>();
            if (debuff) {
                debuff.ShatterFrozen();
            } else {
                print("man wtf is wron wif you deekhead");
            }
        } else {
        }
    }
}
