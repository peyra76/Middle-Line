using UnityEngine;
using UnityEngine.Events; 

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public bool isInvulnerable = false;

    [Header("Events")]
    public UnityEvent<float, float> OnHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;
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
    }

    void Die()
    {
        Debug.Log(name + " погиб!");
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