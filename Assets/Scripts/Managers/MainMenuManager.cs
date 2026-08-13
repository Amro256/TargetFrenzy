using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    //General Functions
    public void LoadScene(int buildIndex) //Function that will load the level
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(buildIndex);
        Debug.Log("Loading Scene");
    }

    public void QuitGame() //Will quit the game
    {
        Application.Quit();
        Debug.Log("Quitting!");
    }
}
