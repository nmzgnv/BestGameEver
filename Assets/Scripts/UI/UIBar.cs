using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    public Slider slider;
    
    public void SetMaxValue(float value){
        slider.maxValue = value;
        SetValue(value);
    }

    public void SetValue(float value)
    {
        slider.value = value;
    }
}
