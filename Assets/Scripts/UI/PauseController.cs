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
    private GameManager _gameManager;
    
    void Start()
    {
        pauseMenu.SetActive(false);
        _gameManager = FindObjectOfType<GameManager>();
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
        _gameManager.IsPlayerControlled = true;
        Time.timeScale = 1f;
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        _gameManager.IsPlayerControlled = false;
        Time.timeScale = 0f;
    }

    public void OnExitPress()
    {
        Time.timeScale = 1f;
        sceneChanger.ChangeScene("Menu");
    }
}
