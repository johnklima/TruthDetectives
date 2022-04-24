using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Rotates light according to "time of day" tied to a timeline slider
//sunrise to sunset 160 to -160 on the Y axis
public class LightRotator : GameStateObject
{
    public float TimeOfDay = 0;


    public void onSlider(float tod)
    {
        TimeOfDay = -tod; //invert for left right

        Quaternion rot = transform.rotation;
        transform.rotation = Quaternion.Euler(TimeOfDay, rot.eulerAngles.y, rot.eulerAngles.z);

    }

}
