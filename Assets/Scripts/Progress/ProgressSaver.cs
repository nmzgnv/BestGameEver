using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressSaver : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetString(ProgressConfig.LastLevelName, SceneManager.GetActiveScene().name);
    }

    public static void Reset()
    {
        PlayerPrefs.DeleteKey(ProgressConfig.LastLevelName);
    }
}