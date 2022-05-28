using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathWindowController : MonoBehaviour
{
    [SerializeField] private GameObject deathWindow;
    [SerializeField] private SceneChanger sceneChanger;
    private Player _player;
    private GameManager _gameManager;
    
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _gameManager = FindObjectOfType<GameManager>();
        if (_player != null)
        {
            _player.Health.OnPlayerDie += ShowWindow;
            _player.Health.OnPlayerDie += () => _gameManager.IsPlayerControlled = false;
        }
    }

    private void ShowWindow()
    {
        deathWindow.SetActive(true);
        var animator = deathWindow.GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("PlayerDeath");
    }

    public void RestartButtonPress()
    {
        sceneChanger.ChangeScene(SceneManager.GetActiveScene().name);
    }

    public void ExitButtonPress()
    {
        sceneChanger.ChangeScene("Menu");
    }
}
