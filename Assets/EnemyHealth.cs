using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Animator enemyAnim;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Enemy took damage! Current health: " + currentHealth);

        // Optional: play hit animation
        if (enemyAnim != null)
        {
            enemyAnim.SetTrigger("hit");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        // You can disable AI and play death animation
        if (enemyAnim != null)
        {
            enemyAnim.SetTrigger("die");
        }
        // Disable movement or destroy enemy after some delay
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        this.enabled = false;
        // Optionally destroy: Destroy(gameObject, 2f);
    }
}
