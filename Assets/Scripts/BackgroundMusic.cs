using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic s_instance = null;

    [SerializeField]
    private List<string> mutedSceneNamesList = new List<string>();

    private HashSet<string> _mutedSceneNames = new HashSet<string>();

    private void Start()
    {
        _mutedSceneNames = new HashSet<string>(mutedSceneNamesList);
        if (s_instance == null)
            s_instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameObject.SetActive(!_mutedSceneNames.Contains(scene.name));
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}