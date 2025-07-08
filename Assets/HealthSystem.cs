using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float health = 100f;
    [SerializeField] float maxHealth = 100f;
    [SerializeField] GameObject ragdoll;

    [Header("Visuals")]
    [SerializeField] GameObject hitVFX;
    [SerializeField] Image healthBarFillImage;
    [SerializeField] Gradient healthColorGradient;

    [Header("Audio")]
    [SerializeField] AudioClip damageSound;
    [SerializeField] AudioSource audioSource;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (healthBarFillImage != null)
        {
            float fillAmount = Mathf.Clamp01(health / maxHealth);

            RectTransform rt = healthBarFillImage.rectTransform;
            rt.localScale = new Vector3(fillAmount, 1f, 1f);

            healthBarFillImage.color = healthColorGradient.Evaluate(fillAmount);
        }
    }

    public void TakeDamage(float damageAmount, Vector3 hitPosition)
    {
        health -= damageAmount;
        animator.SetTrigger("damage");
        HitVFX(hitPosition);

        if (damageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        UpdateHealthBar();

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
        // Spawn ragdoll if assigned
        if (ragdoll != null)
        {
            Instantiate(ragdoll, transform.position, transform.rotation);
        }

        // Show Game Over screen
        GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver();
        }
        else
        {
            Debug.LogWarning("GameOverManager not found in scene!");
        }

        // Destroy this object
        Destroy(gameObject);
    }

    public void GainHealth(float amount)
    {
        health += amount;
        health = Mathf.Min(health, maxHealth);
        UpdateHealthBar();

        Debug.Log("Healed " + amount + " HP. Current health: " + health);
    }
}