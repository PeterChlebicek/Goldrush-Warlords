using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void BackButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void CreateButton()
    {
        SceneManager.LoadScene("CreateGame");
    }
    public void SettingsButton()
    {
        SceneManager.LoadScene("Settings");
    }
    public void ReturnButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void KeyButton()
    {
        SceneManager.LoadScene("Keybinds");
    }
    public void IronGraveyard() 
    {
        SceneManager.LoadScene("MapIG");
    }
}
