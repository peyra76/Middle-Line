using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float damageAmount = 20f;
    public string targetTag = "Enemy";
    private Collider damageCollider;

    void Start()
    {
        damageCollider = GetComponent<Collider>();
        damageCollider.enabled = false;
    }

    public void EnableDamage() { damageCollider.enabled = true; }
    public void DisableDamage() { damageCollider.enabled = false; }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"<color=yellow>Меч коснулся объекта:</color> {other.gameObject.name} (Тег: {other.tag})");

        if (other.CompareTag(targetTag))
        {
            HealthSystem health = other.GetComponent<HealthSystem>();

            if (health != null)
            {
                health.TakeDamage(damageAmount);
                Debug.Log($"<color=green>УСПЕХ!</color> Нанесли {damageAmount} урона по {other.name}");
            }
            else
            {
                Debug.Log($"<color=red>ОШИБКА:</color> Тег '{targetTag}' совпал, но скрипта HealthSystem на враге {other.name} НЕТ!");
            }
        }
    }
}