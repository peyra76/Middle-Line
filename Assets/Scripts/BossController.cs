using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [Header("Настройки босса")]
    public string bossName = "Рыцарь Бездны";

    private HealthSystem healthSystem;

    private GameObject bossUIContainer;
    private Slider healthSlider;
    private Text nameText;

    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();

        GameObject uiAnchor = GameObject.FindGameObjectWithTag("BossUI");

        if (uiAnchor != null)
        {
            bossUIContainer = uiAnchor.transform.GetChild(0).gameObject;
            bossUIContainer.SetActive(true);

            healthSlider = bossUIContainer.GetComponentInChildren<Slider>();
            nameText = bossUIContainer.GetComponentInChildren<Text>();

            if (nameText != null) nameText.text = bossName;
            if (healthSlider != null && healthSystem != null)
            {
                healthSlider.maxValue = healthSystem.maxHealth;
                healthSlider.value = healthSystem.currentHealth;
            }
        }
        else
        {
            Debug.LogWarning("Якорь UI Босса не найден на сцене! Проверь тег BossUI.");
        }
    }

    void Update()
    {
        if (healthSlider != null && healthSystem != null)
        {
            healthSlider.value = healthSystem.currentHealth;
        }
    }

    void OnDestroy()
    {
        if (bossUIContainer != null)
        {
            bossUIContainer.SetActive(false);
        }
    }
}