using UnityEngine;
using UnityEngine.Events; 
using System.Collections;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isInvulnerable = false;

    [Header("I-Frames Settings")]
    public float damageIFrames = 0.5f; 

    [Header("Effects")]
    public GameObject deathParticlePrefab; 

    [Header("Events")]
    public UnityEvent<float, float> OnHealthChanged;

    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        OnHealthChanged?.Invoke(currentHealth, maxHealth); 
    }

    public void TakeDamage(float amount)
    {
        if (isInvulnerable)
        {
            Debug.Log("Урон проигнорирован (Invulnerable)"); 
            return; 
        }

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log($"{name} получил урон. Осталось: {currentHealth}"); 

        OnHealthChanged?.Invoke(currentHealth, maxHealth); 

        if (currentHealth <= 0)
        {
            Die(); 
        }
        else
        {
            if (animator != null) animator.SetTrigger("GetHit");
            StartCoroutine(DamageCooldown());
        }
    }

    private IEnumerator DamageCooldown()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(damageIFrames);
        isInvulnerable = false;
    }

    void Die()
    {
        if (animator != null) animator.SetTrigger("Die");
        Debug.Log(name + " погиб!"); 

        if (deathParticlePrefab != null)
        {
            Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        }

        if (gameObject.CompareTag("Enemy")) 
        {
             Destroy(gameObject); 
        }
    }

    [ContextMenu("Test Take 20 Damage")]
    public void TestDamage()
    {
        TakeDamage(20); 
    }

    public void MultiplyMaxHealth(float multiplier)
    {
        maxHealth *= multiplier; 
        currentHealth *= multiplier;
        OnHealthChanged?.Invoke(currentHealth, maxHealth); 
    }
}