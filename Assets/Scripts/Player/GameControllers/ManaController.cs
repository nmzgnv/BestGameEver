using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaController : MonoBehaviour
{
    [SerializeField] private int manaPoints;
    [SerializeField] private int manaByFixedUpdate;

    private int _currentManaPoints;
    
    void Awake()
    {
        _currentManaPoints = manaPoints;
    }

    void FixedUpdate()
    {
        _currentManaPoints = Math.Min(_currentManaPoints + manaByFixedUpdate, manaPoints);
        Debug.Log("Mana: " + _currentManaPoints);
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