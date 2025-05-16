using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private PlayerSettings playerSettings;
    [SerializeField]
    private GameObject winScreen;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (playerSettings.win)
        {
            playerSettings.win = false;
            winScreen.SetActive(true);
            StartCoroutine(HideWinScreenAfterDelay());
            
        }
    }

    IEnumerator HideWinScreenAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        winScreen.SetActive(false);
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
