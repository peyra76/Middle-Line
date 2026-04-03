using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 

    [Header("Экраны")]
    public GameObject deathScreen;
    public GameObject victoryScreen;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void OnPlayerDied()
    {
        StartCoroutine(ShowScreenAndExit(deathScreen));
    }

    public void OnBossDied()
    {
        StartCoroutine(ShowScreenAndExit(victoryScreen));
    }

    private IEnumerator ShowScreenAndExit(GameObject screenToShow)
    {
        if (screenToShow != null)
        {
            screenToShow.SetActive(true);
        }

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Menu");
    }
}