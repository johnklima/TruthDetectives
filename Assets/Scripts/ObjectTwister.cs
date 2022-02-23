using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class ObjectTwister : GameStateObject
{

    public bool isSelected = false;
    
    //slideEmitter for click slide move 
    public StudioEventEmitter slideEmitter;

    //set at design time for ease
    public StudioEventEmitter twistEmitter;
    public GlobalData global;
    public Vector3 mousePosition;

    private void Start()
    {
        StudioEventEmitter[] e = GetComponents<StudioEventEmitter>();
  
        slideEmitter = e[0];
        twistEmitter = e[1]; 
        global = Camera.main.GetComponent<GlobalData>();
    }

    // Update is called once per frame
    void Update()
    {
        //frustrate my selected status
        isSelected = false;
        if (global.selected == transform)
            isSelected = true;

        if (isSelected)
        {

            float horz = Input.GetAxis("Horizontal");
            float vert = Input.GetAxis("Vertical");
            transform.RotateAroundLocal(Vector3.forward, Time.deltaTime * 3 * horz);
            if(horz != 0)
                twistEmitter.Play();

            transform.RotateAroundLocal(Vector3.right, Time.deltaTime * 3 * vert);
            if (vert != 0)
                twistEmitter.Play();

            /*if(Input.GetKey(KeyCode.LeftArrow))
            {
                transform.RotateAroundLocal(Vector3.forward, Time.deltaTime * 3);
                twistEmitter.Play();

                
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.RotateAroundLocal(Vector3.forward, -Time.deltaTime * 3);
                twistEmitter.Play();


            }*/




        }

        if( slideEmitter.IsPlaying() && Input.GetMouseButton(0) )
        {
            
            if (Mathf.Abs(Vector3.Distance(mousePosition, Input.mousePosition)) > 0.0001f )
            {
                slideEmitter.SetParameter("52_param_volume", 1);
            }
            else
            {
                slideEmitter.SetParameter("52_param_volume", 0);
            }
            mousePosition = Input.mousePosition;
        }
        else
        {
            slideEmitter.SetParameter("52_param_volume", 0);
        }
        
    }
    private void OnMouseDown()
    {
        //inform the UI
        global.selected = transform;

        slideEmitter.SetParameter("52_param_to_end", 0);
        slideEmitter.SetParameter("52_param_to_slide", 0);
        slideEmitter.Play();
        mousePosition = Input.mousePosition;
    
    }
    private void OnMouseUp()
    {
        slideEmitter.SetParameter("52_param_to_end", 1);
        slideEmitter.SetParameter("52_param_to_slide", 0);
        
    }
    private void OnMouseDrag()
    {
        slideEmitter.SetParameter("52_param_to_slide", 1);
    }

}
