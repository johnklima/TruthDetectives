using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItem : MonoBehaviour
{
    // Start is called before the first frame update

    public Quaternion location;
    public StickCam stickCam;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log("MAP ITEM CLICK");
        stickCam.transitionCamera(location, 800);
    }
}
