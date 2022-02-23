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
    public int state = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onGo()
    {
        Debug.Log("On Go " + state);
        if (state < 0)
        {

            //MAIN SCENE MOVE TO NEXT MAP

            int index = 0;
            float epsilon = 1000;

            Quaternion otr = objectTargetRotator.rotation;
            //find closest target object to the target object rotator, and go there
            for (int i = 0; i < locations.Length; i++)
            {
                Quaternion rotLoc = locations[i].rotation;
                float angle = Quaternion.Angle(rotLoc, otr);
                if (angle < epsilon)
                {
                    epsilon = angle;
                    index = i;
                }

            }

            //cam.transitionCamera(camTwist.targetRotations[index], camTwist.targetZs[index]);
            cam.transitionCamera(camTwist.targetRotations[index], 680);


            state = 0;
        }
        else 
        {
            //LOAD NEXT SCENE
            SceneManager.LoadScene("GroundScene", LoadSceneMode.Single);

        }
               

    }
}
