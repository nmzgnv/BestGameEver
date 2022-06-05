using UnityEngine;

public class ProgressLoader : MonoBehaviour
{
    public string GetLastSceneName()
    {
        return PlayerPrefs.HasKey(ProgressConfig.LastLevelName)
            ? PlayerPrefs.GetString(ProgressConfig.LastLevelName)
            : null;
    }
}