using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineManager : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;
    [SerializeField]
    private string gameScene;

    [SerializeField] private SceneChanger sceneChanger;
    
    private PlayableDirector _director;
    void Start()
    {
        _director = GetComponent<PlayableDirector>();
    }

    void Update()
    {
        if (_director.state == PlayState.Playing) return;
        SceneManager.LoadScene(nextSceneName);
    }
    
    public void ChangeToGameScene(){
        sceneChanger.ChangeScene(gameScene);
    }
}
