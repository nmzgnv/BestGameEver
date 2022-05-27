using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

public class PauseController : MonoBehaviour
{
    [HideInInspector] public bool gameIsPaused = false;
    public GameObject pauseMenu;
    public SceneChanger sceneChanger;
    
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
                Resume();
            else
                Pause();
            gameIsPaused = !gameIsPaused;
        }
    }
    
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnExitPress()
    {
        Time.timeScale = 1f;
        sceneChanger.ChangeScene("Menu");
    }
}
