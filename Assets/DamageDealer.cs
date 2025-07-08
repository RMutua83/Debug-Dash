using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private bool canDealDamage = false;
    private List<GameObject> hasDealtDamage = new List<GameObject>();

    [SerializeField] private float weaponLength = 1.5f;
    [SerializeField] private float weaponDamage = 1f;
    [SerializeField] private LayerMask enemyLayer; // Set this from Inspector

    void Update()
    {
        if (!canDealDamage) return;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, enemyLayer))
        {
            GameObject target = hit.transform.gameObject;

            if (!hasDealtDamage.Contains(target))
            {
                Enemy enemy = target.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(weaponDamage);

                    // OPTIONAL: Only call if implemented
                    // enemy.HitVFX(hit.point);

                    hasDealtDamage.Add(target);
                }
            }
        }
    }

    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage.Clear();
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
