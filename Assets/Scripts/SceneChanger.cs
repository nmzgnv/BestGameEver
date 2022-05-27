using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private Animator _animator;
    private string sceneToChange;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void ChangeScene(string sceneName)
    {
        sceneToChange = sceneName;
        _animator.SetTrigger("FadeOn");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToChange);
    }
}
