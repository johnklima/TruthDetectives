using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class SkipIntro : MonoBehaviour
{
    //TODO: skip intro is a bad name, should just be a part of a video control widget

    private VideoPlayer video;
    public GameObject VideoCube;
    public DialogTree tree;
    public CorrectLocationTrigger trig;


    //TODO: not needed refactor to use global state;
    public enum STATES
    {        
        INTRO,
        CHAP1,
        CHAP2,
        CHAP3
    }
    public STATES state = STATES.INTRO;
    private void Start()
    {
        video = VideoCube.GetComponent<VideoPlayer>();
    }
    public void onClick()
    {

        transform.GetChild(0).GetComponent<Text>().text = "Next Step";

        GlobalData glo = Camera.main.GetComponent<GlobalData>();

        if (state == STATES.INTRO)
        {
            //start first chat            
            video.Stop();
            VideoCube.SetActive(false);
            transform.gameObject.SetActive(false);

            //intro is special, chat only starts after canceling the intro video
            tree.iterateChat();

            //set up for next video
            state += 1;
            
            if (glo.state == GlobalData.STATES.INTRO)
            {
                glo.nextState();
                return;
            }


        }


       
        //we move to chap 2
        if (glo.state == GlobalData.STATES.CHAP2)
        {
            video.Stop();
            VideoCube.SetActive(false);
            transform.gameObject.SetActive(false);

            Debug.Log("Video is Skipped");            
            //load chapter 2
            glo.loadScene("chapter2");
                


        }

        if (glo.state == GlobalData.STATES.CHAP3)
        {
            //TODO: bad, but it keeps it encapsulated to the location event
            if(trig)
                trig.next = true;
            
        }

    }

    public void iterateVideo()
    {
        transform.gameObject.SetActive(true);
        transform.GetChild(0).GetComponent<Text>().text = "Next Step";
        

    }
}
