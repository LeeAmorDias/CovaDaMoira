using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Play(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ActivateOrDeactivate(GameObject Go)
    {
        Go.SetActive(!Go.activeInHierarchy);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
