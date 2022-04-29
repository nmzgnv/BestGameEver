using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private string startSceneName = "";

    public void ChangeSettingsVisibility()
    {
        settingsPanel.SetActive(!settingsPanel.activeInHierarchy);
    }

    public void OnPlayPress()
    {
        SceneManager.LoadScene(startSceneName);
    }

    public void OnExitPress()
    {
        Application.Quit();
    }
}