using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float health = 3;
    [SerializeField] GameObject hitVFX;

    [Header("Combat")]
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float aggroRange = 4f;

    [Header("Rewards")]
    [SerializeField] GameObject healthGainPopupPrefab;   // UI floating text prefab
    [SerializeField] Transform popupSpawnPoint;          // empty transform above head
    [SerializeField] float healthReward = 10f;           // how much HP to give player
    [SerializeField] float swordBuffAmount = 1f;         // how much to buff sword

    [Header("Audio")]
    [SerializeField] AudioClip spawnSound;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioSource audioSource;

    GameObject player;
    NavMeshAgent agent;
    Animator animator;
    float timePassed;
    float newDestinationCD = 0.5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (spawnSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(spawnSound);
        }
    }

    void Update()
    {
        if (player == null) return;
        if (agent == null || !agent.enabled) return;

        animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);

        if (timePassed >= attackCD && Vector3.Distance(player.transform.position, transform.position) <= attackRange)
        {
            animator.SetTrigger("attack");
            timePassed = 0;
        }

        timePassed += Time.deltaTime;

        if (newDestinationCD <= 0 && Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
        {
            newDestinationCD = 0.5f;
            agent.SetDestination(player.transform.position);
        }

        newDestinationCD -= Time.deltaTime;
        transform.LookAt(player.transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
        }
    }

    public void TakeDamage(float damageAmount, Vector3 hitPosition)
    {
        health -= damageAmount;
        animator.SetTrigger("damage");
        HitVFX(hitPosition);

        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damageAmount)
    {
        TakeDamage(damageAmount, transform.position);
    }

    void HitVFX(Vector3 hitPosition)
    {
        if (hitVFX != null)
        {
            GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
            Destroy(hit, 3f);
        }
    }

    void Die()
    {
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }

        // Heal the player
        if (player != null)
        {
            HealthSystem playerHealth = player.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.GainHealth(healthReward);
                ShowFloatingText("+" + healthReward + " HP");
            }

            SwordStrength sword = player.GetComponentInChildren<SwordStrength>();
            if (sword != null)
            {
                sword.IncreaseStrength(swordBuffAmount);
            }

            ScoreManager.Instance.AddScore(1);
        }

        // Disable enemy logic
        if (agent != null) agent.enabled = false;
        if (animator != null) animator.enabled = false;
        if (TryGetComponent<Collider>(out var col)) col.enabled = false;

        Destroy(gameObject, 3f);
    }

    void ShowFloatingText(string text)
    {
        if (healthGainPopupPrefab != null && popupSpawnPoint != null)
        {
            GameObject popup = Instantiate(healthGainPopupPrefab, popupSpawnPoint.position, Quaternion.identity);
            popup.transform.SetParent(GameObject.Find("Canvas").transform, true);

            var floating = popup.GetComponent<FloatingGainText>();
            if (floating != null)
            {
                floating.SetText(text);
            }
        }
    }

    public void StartDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>()?.StartDealDamage();
    }

    public void EndDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>()?.EndDealDamage();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}