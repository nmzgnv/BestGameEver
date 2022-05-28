using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBarContoller : MonoBehaviour
{
    [SerializeField] private Slider levelSlider;

    [SerializeField] private EnemiesController enemiesController;
    
    public void RefreshBar()
    {
        --levelSlider.value;
    }

    private void Start()
    {
        levelSlider.maxValue = enemiesController.EnemiesCount;
        levelSlider.value = levelSlider.maxValue;
    }
}