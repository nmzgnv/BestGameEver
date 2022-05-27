using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private string startSceneName = "";

    [SerializeField] private SceneChanger sceneChanger;

    public void ChangeSettingsVisibility()
    {
        settingsPanel.SetActive(!settingsPanel.activeInHierarchy);
    }

    public void OnPlayPress()
    {
        sceneChanger.ChangeScene(startSceneName);
    }

    public void OnExitPress()
    {
        Application.Quit();
    }
}