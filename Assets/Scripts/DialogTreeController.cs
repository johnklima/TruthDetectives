using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTreeController : MonoBehaviour
{

    public GlobalData glo;
    public float nextTimer = -1;
    public Transform currentDialog;
    public GameObject currentVideo;

    public Dialog dialog;

    // Start is called before the first frame update
    void Start()
    {
        glo = Camera.main.GetComponent<GlobalData>();
    }

    // Update is called once per frame
    void Update()
    {
        //for now, 2 seconds
        if(Time.time - nextTimer > 2 && nextTimer > 0 && dialog.pause == false)
        {
            //iterate next            
            if (currentDialog) 
            {

                currentDialog = currentDialog.GetComponent<Dialog>().newMessage();
                if(currentDialog)
                    dialog = currentDialog.GetComponent<Dialog>();

                nextTimer = Time.time;
            }
            else
            {
                nextTimer = -1;

            }

        }
    }

    public void OnNextClick()
    {

        Debug.Log("OnNextClick");

        if(glo.state == GlobalData.STATES.INTRO)
        {
            //Get the dialog ball rolling after the video is stopped
            transform.GetChild(0).gameObject.SetActive(true);
            currentDialog = transform.GetChild(0);
            dialog = currentDialog.GetComponent<Dialog>();
            glo.nextState();
            nextTimer = Time.time;

            if (currentVideo)
                currentVideo.SetActive(false);


        }
        else if (glo.state == GlobalData.STATES.CHAP1)
        {
            
            //start transition to AOI
            glo.transitionCamera(0);

        }
        else if (glo.state == GlobalData.STATES.CHAP2)
        {
            glo.nextState();
        }
    }

}
