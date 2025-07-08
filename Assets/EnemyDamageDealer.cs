using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    private bool canDealDamage = false;
    private bool hasDealtDamage = false;

    [SerializeField] private float weaponLength = 1.5f;
    [SerializeField] private float weaponDamage = 1f;
    [SerializeField] private LayerMask playerLayer; // Assign in Inspector

    void Update()
    {
        if (canDealDamage && !hasDealtDamage)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, playerLayer))
            {
                HealthSystem health = hit.transform.GetComponent<HealthSystem>();
                if (health != null)
                {
                    health.TakeDamage(weaponDamage);

                    // Optional — comment this out if not implemented
                    // health.HitVFX(hit.point);

                    hasDealtDamage = true;
                }
            }
        }
    }

    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage = false;
    }

    public void EndDealDamage()
    {
        canDealDamage = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength);
    }
}
