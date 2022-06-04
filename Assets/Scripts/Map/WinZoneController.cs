using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZoneController : MonoBehaviour
{
    private WinWindowController _winWindow;
    private GameObject _zoneImage;
    
    private void Awake()
    {
        _winWindow = FindObjectOfType<WinWindowController>();
        _zoneImage = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_zoneImage.activeSelf && other.gameObject.layer == 10)
            _winWindow.ShowWindow();
    }

    public void ShowPortal()
    {
        _zoneImage.SetActive(true);
    }
}
