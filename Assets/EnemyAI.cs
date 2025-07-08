using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent ai;
    public Transform player;
    public Animator aiAnim;

    [Header("Attack Settings")]
    public float attackRange = 2f;
    public float timeBetweenAttacks = 1.5f;
    public int attackDamage = 20;

    private bool alreadyAttacked;
    private PlayerHealth playerHealth;

    void Start()
    {
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            // Stop to attack
            ai.isStopped = true;

            aiAnim.ResetTrigger("jog");
            aiAnim.ResetTrigger("idle");
            aiAnim.SetTrigger("attack");

            AttackPlayer();
        }
        else
        {
            // Chase
            ai.isStopped = false;
            ai.SetDestination(player.position);

            aiAnim.ResetTrigger("attack");
            aiAnim.ResetTrigger("idle");
            aiAnim.SetTrigger("jog");
        }
    }

    void AttackPlayer()
    {
        if (!alreadyAttacked)
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
