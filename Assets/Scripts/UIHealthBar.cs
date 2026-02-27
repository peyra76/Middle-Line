using UnityEngine;
using UnityEngine.UI; 

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private Image healthFill; 

    public void UpdateHealthDisplay(float currentHealth, float maxHealth)
    {
        float targetFill = currentHealth / maxHealth;
        healthFill.fillAmount = targetFill;
    }
}