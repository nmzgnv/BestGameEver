using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBarContoller : MonoBehaviour
{
    [SerializeField] private Slider levelSlider;
    [SerializeField] private WinZoneController winZone;
    [SerializeField] private EnemiesController enemiesController;
    
    public void RefreshBar()
    {
        --levelSlider.value;
        if (levelSlider.value <= 0)
            winZone.gameObject.SetActive(true); 
    }
    
    public void RefreshBar(int value)
    {
        levelSlider.value -= value;
        if (levelSlider.value <= 0)
            winZone.gameObject.SetActive(true); 
    }

    private void Start()
    {
        levelSlider.maxValue = enemiesController.EnemiesCount;
        levelSlider.value = levelSlider.maxValue;
        
        RefreshBar(0);
    }
}