using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 25;
    public float attackRange = 2f;
    public LayerMask enemyLayer;
    private Animator anim;

    /*void Start() 
    {
        anim = GetComponent<Animator>();
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            anim.SetTrigger("Attack");
        }
    }*/
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) // Left mouse button
        {
            Attack();
        }
    }

    void Attack()
    {
        // Check for enemies in range
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position + transform.forward, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }

        Debug.Log("Player attacked!");
    }

    void OnDrawGizmosSelected()
    {
        // Visualize attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward, attackRange);
    }
}
