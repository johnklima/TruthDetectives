using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRotator : GameStateObject
{
    public GlobalData global;

    private void Start()
    {
        global = Camera.main.GetComponent<GlobalData>();
        writeJSON();

    }
    private void Update()
    {
        

        //am I the current object of interest?
        if(global.selected == transform)
        {

            Transform rotator = transform;

            float Z = Input.GetAxis("Horizontal");
            float X = Input.GetAxis("Vertical");
            bool shift = Input.GetKey(KeyCode.LeftShift);

            if (Z != 0)
            {
                if(!shift)
                    rotator.Rotate(0, 0, -Z * Time.deltaTime);
                else
                    rotator.Rotate(0, Z * Time.deltaTime * 20, 0);
            }

            if (X != 0)
            {
                if(!shift)
                    rotator.Rotate(X * Time.deltaTime, 0, 0);
            }

            

        }

        writeJSON();
    }

}
