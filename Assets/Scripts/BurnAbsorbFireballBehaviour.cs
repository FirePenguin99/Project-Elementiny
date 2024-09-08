using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnAbsorbFireballBehaviour : MonoBehaviour
{
    public GameObject megaFireballPrefab;
    public Camera playerCam;
    public LayerMask projectileLayerMask;

    public Vector3 shootOffset = new Vector3(0, 10, 0);
    public float shootForce = 50;
    
    public static List<GameObject> burningEnemies = new List<GameObject>();
    public int totalBurnAbsorbed = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            print("Absorbing Burn!");

            AbsorbAllBurningEnemies();
            FireMegaball();
        }
    }

    public void AbsorbAllBurningEnemies() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<BurnDebuffBehaviour>() != null) {
                burningEnemies.Add(enemy);
                totalBurnAbsorbed += enemy.GetComponent<BurnDebuffBehaviour>().burnStackCount;
                
                // extinguish burning
                enemy.GetComponent<BurnDebuffBehaviour>().ExtinguishBurn();
            }
        }
    }

    public void FireMegaball() {
        Vector3 shootPoint = transform.position + shootOffset; 
        Vector3 aimDirection = CalculateAimDirection() - shootPoint;

        GameObject currentBullet = Instantiate(megaFireballPrefab, shootPoint, Quaternion.identity);

        currentBullet.transform.forward = aimDirection.normalized; // point the projectile at the Aim Position
        currentBullet.GetComponent<Rigidbody>().AddForce(aimDirection.normalized * shootForce, ForceMode.Impulse);
        // Currently on hit gives enemy's in blast a new Burn with a starting stack value of all the Burn stack values that were absorbed. OP as fuck and then would spread and deal massive Crowd damage. Not the intention.
        // Fix by changing the vast majority of damage in Base Damage, rather than debuff
        currentBullet.GetComponent<FireballBulletBehaviour>().addStackAmount = totalBurnAbsorbed;
    }

    private Vector3 CalculateAimDirection() {
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // spawns a ray in the middle of the screen

        Vector3 aimPosition;
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, projectileLayerMask)) {
            aimPosition = hit.point;
        } else {
            aimPosition = ray.GetPoint(100); // if the ray hasnt hit anything, just point if far away from the player
        }

        return aimPosition;
    }
}
