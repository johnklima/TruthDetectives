using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LightSlider : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform light;
    public CameraController cam;
    public Slider slider;
    
    public float timeOfDay;

    GlobalData glo;
    void Start()
    {
        light.localRotation = Quaternion.Euler(timeOfDay, 45, 14);
        glo = Camera.main.GetComponent<GlobalData>();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.M))
        {
            //cam.enabled = !cam.enabled;
            Debug.Log(cam.gameObject.transform.rotation.ToEulerAngles().ToString());
            Debug.Log(cam.gameObject.transform.position.ToString());

            cam.transitionImageTarget();

        }

    }
    public void OnSliderSlide()
    {
        //cam.enabled = false;
        timeOfDay = slider.value; //invert for left right
        Quaternion rot = transform.localRotation;
        //light.localRotation = Quaternion.Euler(timeOfDay, rot.eulerAngles.y, rot.eulerAngles.z);
        light.localRotation = Quaternion.Euler(timeOfDay, 45, 14);
        Debug.Log(slider.value);
        if(slider.value > 55.0f && slider.value < 58.0f)
        {
            //glo.dialogTree.releaseFrustrate(0);
        }



    }
    public void OnUp()
    {

       // cam.enabled = true;
    }
 
}
