using System; //for json
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;


[Serializable]
public class VideoSurface : GameStateObject
{

    //some code copy/paste here from object twister, as we dont 
    //actualy twist the video. but maybe we do eventualy so
    //consider a TODO: refactor audio/mouse into a small class

    //slideEmitter for click slide move 
    public StudioEventEmitter slideEmitter;    

    public Vector3 mousePosition;
    public float zScale;

    private Vector3 initialScale;

    public float twistZ = 0 ;
    public bool allowTwist = false;

    // Start is called before the first frame update
    void Start()
    {
        readJSON();
        slideEmitter = GetComponent<StudioEventEmitter>();
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

        //scale surface to camera zoom level
        zScale = Mathf.Abs(Camera.main.transform.position.z * 0.001f);
        zScale *= zScale;  //eponential
        zScale *= zScale;  //eponential

        //transform.localScale = initialScale * zScale;
;
        //match the camera rotation so it is flat to the camera plane
        //but override the z so we can twist it

        twistZ = transform.rotation.eulerAngles.z;
        transform.rotation = Camera.main.transform.rotation;
        
        if(allowTwist)
        {
            Vector3 euler = transform.rotation.eulerAngles;
            euler.z = twistZ;
            Quaternion q = Quaternion.Euler(euler);
            transform.rotation = q;
        }

        if (slideEmitter.IsPlaying() && Input.GetMouseButton(0))
        {
            
            if (Mathf.Abs(Vector3.Distance(mousePosition, Input.mousePosition)) > 0.0001f)
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

    public void setTwist(float twist)
    {
        transform.Rotate(new Vector3(0, 0, twist));
        twistZ = transform.rotation.eulerAngles.z;

    }

}
