using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [Header("Настройки босса")]
    public string bossName = "Рыцарь Бездны";

    // Ссылка на твой скрипт здоровья (предполагаю, что он называется HealthSystem)
    private HealthSystem healthSystem;

    // Ссылки на элементы UI
    private GameObject bossUIContainer;
    private Slider healthSlider;
    private Text nameText;

    void Start()
    {
        // Получаем компонент здоровья с этого же объекта
        healthSystem = GetComponent<HealthSystem>();

        // Ищем включенный якорь UI босса на сцене по тегу
        // Внимание: FindGameObjectWithTag ищет только ВКЛЮЧЕННЫЕ объекты, 
        // поэтому мы ищем включенный пустой якорь.
        GameObject uiAnchor = GameObject.FindGameObjectWithTag("BossUI");

        if (uiAnchor != null)
        {
            // Берем первого ребенка (нашу выключенную панель) и включаем ее
            bossUIContainer = uiAnchor.transform.GetChild(0).gameObject;
            bossUIContainer.SetActive(true);

            // Ищем внутри слайдер и текст
            healthSlider = bossUIContainer.GetComponentInChildren<Slider>();
            nameText = bossUIContainer.GetComponentInChildren<Text>();

            // Настраиваем UI
            if (nameText != null) nameText.text = bossName;
            if (healthSlider != null && healthSystem != null)
            {
                // Задаем максимальное и текущее здоровье слайдеру
                // Убедись, что переменные maxHealth и currentHealth публичные в HealthSystem
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
        // Синхронизируем здоровье каждый кадр
        if (healthSlider != null && healthSystem != null)
        {
            healthSlider.value = healthSystem.currentHealth;
        }
    }

    void OnDestroy()
    {
        // Когда босс умирает (или его объект удаляется со сцены), выключаем босс-бар
        if (bossUIContainer != null)
        {
            bossUIContainer.SetActive(false);
        }
    }
}