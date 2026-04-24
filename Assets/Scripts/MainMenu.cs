using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Sound Settings")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Slider volumeSlider; 

    private const string VOLUME_KEY = "GameVolume";

    public void PlayGame()
    {
        SceneManager.LoadScene("Middle Line");
    }

    public void QuitGame()
    {
        Debug.Log("Выход из игры...");
        Application.Quit();
        EditorApplication.ExitPlaymode();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        LoadVolume();
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void OpenSettingsPanel()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void CloseSettingsPanel()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat(VOLUME_KEY, value);
        PlayerPrefs.Save();
    }

    private void LoadVolume()
    {
        float saved = PlayerPrefs.GetFloat(VOLUME_KEY, 1f);
        if (volumeSlider != null)
            volumeSlider.value = saved;
        AudioListener.volume = saved;
    }
}