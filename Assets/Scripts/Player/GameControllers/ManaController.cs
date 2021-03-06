using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaController : MonoBehaviour
{
    public event Action OnManaPointsChanged;

    [SerializeField]
    private int maxManaPoints;

    [SerializeField]
    private int manaByFixedUpdate;

    private int _currentManaPoints;

    public int MaxManaPoint => maxManaPoints;
    public int Mana => _currentManaPoints;

    public void IncreaseMaxManaAndRestore(int value)
    {
        maxManaPoints += value;
        _currentManaPoints = maxManaPoints;
        OnManaPointsChanged?.Invoke();
    }

    private void Awake()
    {
        _currentManaPoints = maxManaPoints;
    }

    private void FixedUpdate()
    {
        _currentManaPoints = Math.Min(_currentManaPoints + manaByFixedUpdate, maxManaPoints);
        OnManaPointsChanged?.Invoke();
    }

    public void AddMana(int value)
    {
        _currentManaPoints = Mathf.Min(maxManaPoints, _currentManaPoints + value);
        OnManaPointsChanged?.Invoke();
    }

    /// <summary>
    /// Принимает действие, которое нужно выполнить с стоимостью, и выполняет его, если достаточно маны
    /// </summary>
    /// <param name="manaCost">Стоимость действия</param>
    /// <param name="func">Действие</param>
    /// <returns>True - если действие удалось выполнить, false - иначе</returns>
    public bool TryDoAction(int manaCost, Action func)
    {
        if (_currentManaPoints >= manaCost)
        {
            _currentManaPoints -= manaCost;
            func();
            Debug.Log("Used " + manaCost + " mana");
            return true;
        }

        return false;
    }
}