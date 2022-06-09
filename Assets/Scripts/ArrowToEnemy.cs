using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ArrowToEnemy : MonoBehaviour
{
    private Camera _camera;
    private EnemiesController _enemiesController;
    private GameObject _arrowImage;

    void Awake()
    {
        _camera = FindObjectOfType<Camera>();
        _enemiesController = FindObjectOfType<EnemiesController>();
        _arrowImage = transform.GetChild(0).gameObject;
    }


    private void Start()
    {
        if (_enemiesController.Enemies.Count == 0)
            gameObject.SetActive(false);
    }

    void Update()
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(_camera);

        Enemy nearestEnemy = null;
        var minimalDistance = Mathf.Infinity;

        foreach (var enemy in _enemiesController.Enemies)
        {
            float distance = (enemy.transform.position - _camera.transform.position).magnitude;
            if (distance < minimalDistance)
            {
                minimalDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy is null) return;

        var toEnemy = nearestEnemy.transform.position - _camera.transform.position;
        toEnemy.z = 0;
        var ray = new Ray(_camera.transform.position, toEnemy);
        var minDistanceToPlanes = Mathf.Infinity;
        var planeIndex = 0;

        for (int i = 0; i < planes.Length; ++i)
            if (planes[i].Raycast(ray, out float distance) && distance < minDistanceToPlanes)
            {
                minDistanceToPlanes = distance;
                planeIndex = i;
            }

        if (minDistanceToPlanes > toEnemy.magnitude)
        {
            _arrowImage.SetActive(false);
        }
        else
        {
            _arrowImage.SetActive(true);
            minDistanceToPlanes = Mathf.Clamp(minDistanceToPlanes, 0, toEnemy.magnitude);
            transform.position = _camera.WorldToScreenPoint(ray.GetPoint(minDistanceToPlanes));
            transform.rotation = RotationByPlaneIndex(planeIndex);
        }
    }

    private Quaternion RotationByPlaneIndex(int index)
    {
        if (index == 0)
            return Quaternion.Euler(0, 0, -90);
        if (index == 1)
            return Quaternion.Euler(0, 0, 90);
        if (index == 2)
            return Quaternion.Euler(0, 0, 0);
        if (index == 3)
            return Quaternion.Euler(0, 0, 180);
        return Quaternion.identity;
    }
}