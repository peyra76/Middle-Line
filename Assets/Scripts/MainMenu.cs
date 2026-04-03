using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Middle Line");
    }

    public void QuitGame()
    {
        Debug.Log("Выход из игры..."); 
        Application.Quit(); 
    }
}