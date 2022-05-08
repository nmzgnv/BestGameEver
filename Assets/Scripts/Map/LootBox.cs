using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootBox : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> lootList = new List<GameObject>();

    [SerializeField]
    private float thingDropProbability;

    private void OnDestroy()
    {
        float value = Random.Range(0f, 1f);
        if (value <= thingDropProbability && lootList.Count > 0)
        {
            var randomIndex = Random.Range(0, lootList.Count - 1);
            Instantiate(lootList[randomIndex], transform.position, Quaternion.identity);
        }
    }
}