using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Locations : MonoBehaviour
{

    public Transform[] locations;
    public Transform objectTargetRotator;

    public CameraTwister camTwist;
    public StickCam cam;
    public int state = 0;

    
    
    public GameObject globe;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 2)
        {
            if(Camera.main.transform.localPosition.z - camTwist.targetZs[state] < 50)
            {

                

                //LOAD NEXT SCENE
                SceneManager.LoadScene("GroundScene", LoadSceneMode.Additive);

                
                globe.SetActive(false);

                state = 3;
            }            

        }
    }
    
    public void onGo()
    {

        //TODO: too much spagetti to get here, rethink...
        Debug.Log("On Go " + state);
        if (state == 0)
        {
            Debug.Log("moving to target map 1");
            cam.transitionCamera(camTwist.targetRotations[0], camTwist.targetZs[0]);
            state = 1;
            //move media to pin location
        }
        else if (state == 1)
        {
            //MAIN SCENE MOVE TO NEXT MAP
            Debug.Log("moving to target map 2");
            
            
            cam.transitionCamera(camTwist.targetRotations[state], camTwist.targetZs[state]);
            //position watertower

            state = 2;
        }
        else if (state == 2)
        {
            cam.transitionCamera(camTwist.targetRotations[state], camTwist.targetZs[state]);
            //move to ground scene
            state = 2;
            

        }
        

    }
}
