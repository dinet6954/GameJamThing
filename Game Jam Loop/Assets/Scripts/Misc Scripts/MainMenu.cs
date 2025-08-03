using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame ()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame ()
    {
        Application.Quit();
        Debug.Log("Player has quit");
    }
}
