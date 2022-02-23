using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Message : GUIState
{

    private bool showme = false;
    public float pos;
    private RectTransform rect;
    private StudioEventEmitter emitter;

    private Transform inbox;
    private Transform outbox;
    private Transform window;

    private Vector3 startPos;
    private float speed = 1000;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();

        //move relative to screen anchor
        rect.transform.localPosition = new Vector3(startPos.x + 200, startPos.y, startPos.z);

        //buffer the start pos for easy accounting
        pos = rect.transform.localPosition.x;
        startPos = rect.transform.localPosition;
        
        //get the fmod event
        emitter = GetComponent<StudioEventEmitter>();

        //get the textbox elements
        inbox = transform.GetChild(0);
        outbox = transform.GetChild(1);
        //get the parent view window
        window = transform.parent.parent.parent; //CAUTION: hierarchy dependent!!!
    }

    // Update is called once per frame
    void Update()
    {
        if(showme && pos > 0)
        {
            if (!emitter.IsPlaying())
                emitter.Play();

            pos-= Time.deltaTime * speed;

            if (pos < 0)
            {
                emitter.Stop();
                rect.transform.localPosition = Vector3.zero;
                pos = 0;
            }
            else
            {
                rect.transform.Translate(-Time.deltaTime * speed, 0, 0);
            }
        }
        else if (!showme && pos == 0)
        {
            rect.transform.Translate(Time.deltaTime * speed, 0, 0);
            if(rect.transform.localPosition.x > startPos.x)
            {
                //restore to initial state
                pos = startPos.x;
                rect.transform.localPosition = startPos;

              
            }
        }

        
    }
    public void show()
    {
        showme = true;

    }
    public void onDismissClick()
    {
        Debug.Log("Dismiss Click");
        window.GetComponent<MessageScrollView>().hide();
        showme = false;

    }
    public void onMessageClick()
    {
        showme = !showme;
        if(!showme)
        {
            onDismissClick();
        }
        else
        {
            
        }
    }

}
