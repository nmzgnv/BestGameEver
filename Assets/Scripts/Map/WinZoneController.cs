using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZoneController : MonoBehaviour
{
    [SerializeField]
    private WinWindowController winWindow;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 10)
        {
            winWindow.ShowWindow();
        }
    }
}
