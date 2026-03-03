using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [Header("Base Stats")]
    public float weaponDamage = 10f;

    [Header("Targeting")]
    public string targetTag = "Enemy";

    [Header("Bonuses")]
    public float damageMultiplier = 1f;
    public float doubleDamageChance = 0f;

    private Collider weaponCollider;

    private void Start()
    {
        weaponCollider = GetComponent<Collider>();
        if (weaponCollider != null)
        {
            DisableDamage();
        }
    }

    public void EnableDamage()
    {
        if (weaponCollider != null) weaponCollider.enabled = true;
    }

    public void DisableDamage()
    {
        if (weaponCollider != null) weaponCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(targetTag)) return;

        HealthSystem targetHealth = other.GetComponent<HealthSystem>();
        if (targetHealth != null)
        {
            float finalDamage = weaponDamage * damageMultiplier;

            if (Random.value < doubleDamageChance)
            {
                finalDamage *= 2;
                Debug.Log("CRIT! Damage: " + finalDamage);
            }

            targetHealth.TakeDamage(finalDamage);
        }
    }
}