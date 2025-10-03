using UnityEngine;

public class ExplodeOnCollideBehaviour : MonoBehaviour
{
    [SerializeField] private float explosionRadius;
    [SerializeField] private int addStackAmount = 25;
    [SerializeField] private LayerMask explosionLayerMask;

    void OnTriggerEnter()
    {
        Explode();
    }

    public void Explode()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, explosionRadius, explosionLayerMask);

        foreach (Collider enemy in enemiesInRange)
        {
            if (enemy.gameObject.name != this.gameObject.name)
            {
                FireElementClass.ApplyBurn(enemy.gameObject, addStackAmount);
            }
        }
    }
}
