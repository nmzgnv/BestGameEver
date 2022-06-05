using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBarContoller : MonoBehaviour
{
    private Slider _levelSlider;
    private WinZoneController _winZone;
    private EnemiesController _enemiesController;

    private void Awake()
    {
        _levelSlider = GetComponent<Slider>();
        _winZone = FindObjectOfType<WinZoneController>();
        _enemiesController = FindObjectOfType<EnemiesController>();
        if (_winZone is null)
            Debug.LogWarning("HELLO FROM ME");
    }

    public void RefreshBar()
    {
        RefreshBar(1);
    }

    private void RefreshBar(int value)
    {
        _levelSlider.value -= value;
        if (_levelSlider.value <= 0)
            _winZone.ShowPortal();
    }

    private void Start()
    {
        foreach (var enemy in _enemiesController.Enemies)
            enemy.Health.OnPlayerDie += RefreshBar;

        _levelSlider.maxValue = _enemiesController.Enemies.Count;
        _levelSlider.value = _levelSlider.maxValue;

        RefreshBar(0);
    }
}