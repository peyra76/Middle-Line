using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public bool isInvulnerable = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (isInvulnerable)
        {
            Debug.Log("Игрок получил удар, но он БЕССМЕРТЕН (isInvulnerable = true). Урон проигнорирован.");
            return;
        }

        if (isInvulnerable) return;

        currentHealth -= amount;
        Debug.Log(name + " получил урон. Осталось: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }

        else
        {
            
        }
    }

    void Die()
    {
        Debug.Log(name + " погиб!");
        if (gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
