using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public ItemData data;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
    }

    private void PickUp()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        InventoryManager.Instance.AddItem(data);

        switch (data.bonusType)
        {
            case ItemData.BonusType.AxBass:
                player.GetComponentInChildren<DamageDealer>().doubleDamageChance += 0.2f;
                break;

            case ItemData.BonusType.Coffee:
                player.GetComponent<SoulsPlayer>().MultiplySpeed(1.5f);
                break;

            case ItemData.BonusType.DullKnife:
                player.GetComponentInChildren<DamageDealer>().damageMultiplier *= 1.5f;
                break;

            case ItemData.BonusType.Heart:
                player.GetComponent<HealthSystem>().MultiplyMaxHealth(1.5f);
                break;

            case ItemData.BonusType.Milk:
                player.GetComponent<StaminaSystem>().MultiplyRegen(1.5f);
                break;

            case ItemData.BonusType.Protein:
                player.GetComponent<StaminaSystem>().MultiplyMaxStamina(1.5f);
                break;
        }

        UIManager.Instance.ShowItemNotification(data.itemName, data.description);
        UIManager.Instance.ToggleInteractionUI(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            UIManager.Instance.ToggleInteractionUI(true, data.itemName);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            UIManager.Instance.ToggleInteractionUI(false);
        }
    }
}