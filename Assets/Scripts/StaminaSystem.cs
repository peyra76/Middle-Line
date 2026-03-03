using UnityEngine;
using UnityEngine.Events;

public class StaminaSystem : MonoBehaviour
{
    [Header("Settings")]
    public float maxStamina = 100f;
    public float currentStamina;
    public float regenRate = 15f; 
    public float regenDelay = 1.0f; 

    [Header("Events")]
    public UnityEvent<float, float> OnStaminaChanged;

    private float _regenTimer;

    void Start()
    {
        currentStamina = maxStamina;
        OnStaminaChanged?.Invoke(currentStamina, maxStamina);
    }

    void Update()
    {
        RegenerateStamina();
    }

    private void RegenerateStamina()
    {
        if (_regenTimer > 0)
        {
            _regenTimer -= Time.deltaTime;
            return;
        }

        if (currentStamina < maxStamina)
        {
            currentStamina += regenRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            OnStaminaChanged?.Invoke(currentStamina, maxStamina);
        }
    }

    public bool UseStamina(float amount)
    {
        if (currentStamina >= amount)
        {
            currentStamina -= amount;
            _regenTimer = regenDelay; 
            OnStaminaChanged?.Invoke(currentStamina, maxStamina);
            return true; 
        }

        Debug.Log("Недостаточно стамины!");
        return false; 
    }

    public void MultiplyMaxStamina(float multiplier)
    {
        maxStamina *= multiplier;
        currentStamina *= multiplier;
        OnStaminaChanged?.Invoke(currentStamina, maxStamina);
    }

    public void MultiplyRegen(float multiplier)
    {
        regenRate *= multiplier;
    }
}