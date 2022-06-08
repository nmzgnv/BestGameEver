using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private ManaController _manaController;

    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private Slider manaSlider;

    [SerializeField]
    private Slider maxVisibleHealthSlider;

    private void RefreshHealth()
    {
        healthSlider.value = _playerHealth.Health;
        maxVisibleHealthSlider.value = _playerHealth.MaxHealth;
    }

    private void RefreshMana()
    {
        manaSlider.maxValue = _manaController.MaxManaPoint;
        manaSlider.value = _manaController.Mana;
    }

    private void Awake()
    {
        var player = FindObjectOfType<Player>();
        _playerHealth = player.GetComponent<PlayerHealth>();
        _manaController = player.GetComponent<ManaController>();

        _playerHealth.OnPlayerApplyHeal += RefreshHealth;
        _playerHealth.OnPlayerTakesDamage += RefreshHealth;
        _manaController.OnManaPointsChanged += RefreshMana;
    }

    private void Start()
    {
        healthSlider.maxValue = PlayerHealth.LimitHealth;
        maxVisibleHealthSlider.maxValue = PlayerHealth.LimitHealth;
        RefreshHealth();
        RefreshMana();
    }
}