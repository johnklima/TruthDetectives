using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : GUIState
{
    // Start is called before the first frame update
    public float slideValue;
    private Slider slider;
    void Start()
    {
       slider = GetComponent<Slider>();
       slider.value = slideValue;
    }

    // Update is called once per frame
    void Update()
    {
        slideValue = slider.value;
        base.Update();
    }
}
