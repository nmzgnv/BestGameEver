using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinWindowController : MonoBehaviour
{
    [SerializeField] private GameObject winWindow;
    [SerializeField] private SceneChanger sceneChanger;
    [SerializeField] private string nextSceneName;
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void ShowWindow()
    {
        _gameManager.IsPlayerControlled = false;
        winWindow.SetActive(true);
        var animator = winWindow.GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("PlayerWin");
    }

    public void NextLevelButtonPress()
    {
        sceneChanger.ChangeScene(nextSceneName);
    }

    public void ExitButtonPress()
    {
        sceneChanger.ChangeScene("Menu");
    }
}
