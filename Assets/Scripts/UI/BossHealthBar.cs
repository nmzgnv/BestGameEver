using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth playerHealth;

    [SerializeField]
    private Slider healthSlider;

    private void RefreshHealth()
    {
        healthSlider.value = playerHealth.Health;
    }

    private void Awake()
    {
        playerHealth.OnPlayerApplyHeal += RefreshHealth;
        playerHealth.OnPlayerTakesDamage += RefreshHealth;
    }

    private void Start()
    {
        healthSlider.maxValue = playerHealth.MaxPossibleHealth;
        RefreshHealth();
    }
}
