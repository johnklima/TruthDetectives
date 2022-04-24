using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTwister : GameStateObject
{
    public float twist = 0;
    public Quaternion[] targetRotations;
    public float[] targetZs;
    
    // Start is called before the first frame update
    void Start()
    {
        
        Quaternion rot = transform.rotation;

        Vector3 euler = rot.eulerAngles;
        euler.z = twist;
        rot.eulerAngles = euler;
        transform.rotation = rot;

    }

    // Update is called once per frame
    void Update()
    {
        

        if(Input.GetKey(KeyCode.Q))
        {
            twist += Time.deltaTime * 10;
        }
        if (Input.GetKey(KeyCode.E))
        {
            twist -= Time.deltaTime * 10;
        }

        //frustrate the z pos update based on twist value
        
        Quaternion rot = transform.rotation;
        Vector3 euler = rot.eulerAngles;
        euler.z = twist;
        rot.eulerAngles = euler;
        transform.rotation = rot;
        

        base.Update();
    }
}
