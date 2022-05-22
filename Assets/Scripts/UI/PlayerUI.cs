using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth playerHealth;

    [SerializeField]
    private ManaController manaController;

    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private Slider manaSlider;

    private void RefreshHealth()
    {
        healthSlider.value = playerHealth.Health;
    }

    private void RefreshMana()
    {
        manaSlider.maxValue = manaController.MaxManaPoint;
        manaSlider.value = manaController.Mana;
    }

    private void Awake()
    {
        playerHealth.OnPlayerApplyHeal += RefreshHealth;
        playerHealth.OnPlayerTakesDamage += RefreshHealth;
        manaController.OnManaPointsChanged += RefreshMana;
    }

    private void Start()
    {
        healthSlider.maxValue = playerHealth.MaxPossibleHealth;
        RefreshHealth();
        RefreshMana();
    }
}