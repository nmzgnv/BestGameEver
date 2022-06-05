using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private string startSceneName = "";

    [SerializeField]
    private SceneChanger sceneChanger;

    [SerializeField]
    private GameObject continueButton;

    private void Start()
    {
        continueButton.SetActive(HasProgress);
    }

    public void ChangeSettingsVisibility()
    {
        settingsPanel.SetActive(!settingsPanel.activeInHierarchy);
    }

    public bool HasProgress
    {
        get => PlayerPrefs.HasKey(ProgressConfig.LastLevelName);
    }

    public void OnContinuePress()
    {
        var sceneName = HasProgress
            ? PlayerPrefs.GetString(ProgressConfig.LastLevelName)
            : startSceneName;
        sceneChanger.ChangeScene(sceneName);
    }

    public void OnNewGamePress()
    {
        ProgressSaver.Reset();
        sceneChanger.ChangeScene(startSceneName);
    }

    public void OnExitPress()
    {
        Application.Quit();
    }
}