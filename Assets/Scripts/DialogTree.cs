using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogTree : MonoBehaviour
{

    public float timer = 0;
    public InputField inbox;
    public InputField outbox;
    public InputField thirdbox;
    public InputField toolbot;
    public PlayVideo vid;
    public string [] dialog01;
    public string [] dialog02;    
    public string [] dialog03;
    public string [] toolBot;
    private int curDialog = -1;
    public RectTransform panel;
    public bool frustrate = false;


    public float releaseTime = 0;
    public float releaseTimer = -1;

    GlobalData global;

    private enum STATES {  
                    MESSAGE,
                    IDLE
                        }
    private STATES state = STATES.IDLE;

    // Start is called before the first frame update
    void Start()
    {
        
        
        global = Camera.main.GetComponent<GlobalData>();
        if (global.state == GlobalData.STATES.CHAP2)
        {
            frustrate = false;
            iterateChat(); // kick it off
            timer = Time.time;
        }

        if (global.state == GlobalData.STATES.CHAP3)
        {
            iterateChat(); // also kick it off            
        }
        if (global.state == GlobalData.STATES.CHAP3_2)
        {
            iterateChat(); // also kick it off            
        }

        if (global.state == GlobalData.STATES.CHAP4)
        {
            iterateChat(); // also kick it off
            timer = Time.time;
            frustrate = false;
        }
        if (global.state == GlobalData.STATES.CHAP5)
        {
            
            iterateChat(); // kick it off
            timer = Time.time;
            frustrate = true;


        }
    }
    public void hideGUI()
    {
        enableBox(inbox, false);
        enableBox(outbox, false);
        enableBox(thirdbox, false);
        enableBox(toolbot, false);
    }
    public void iterateChat()
    {

        //TODO: hacky check on index, work this out!!
        if (curDialog > dialog02.Length - 1)
        {
            curDialog = dialog02.Length - 1;
            return;
        }
            

        curDialog++;

        if (curDialog > dialog02.Length - 1)
        {
            curDialog = dialog02.Length - 1;
            return;
        }

        timer = Time.time;
        Debug.Log("Iterate Chat " + curDialog + " timer " + timer);

        //CHAP 1 FUNCTION - frustrate until user plays video
        if (curDialog == 0 
            && global.state == GlobalData.STATES.INTRO)
        {
            //wait until they play the video
            frustrate = true;
        }
        
        if (curDialog == 3 
            && global.state == GlobalData.STATES.CHAP2)
        {
            //wait until they generate stills and enhance
            frustrate = true;
        }
        if (curDialog == 2
            && global.state == GlobalData.STATES.CHAP3)
        {
            //wait until they place the watertower
            frustrate = true;
        }
        if (curDialog == 2
            && global.state == GlobalData.STATES.CHAP3_2)
        {
            //wait until they place the watertower
            frustrate = true;
        }
        if (curDialog == 1
            && global.state == GlobalData.STATES.CHAP5)
        {
            //wait until they make stills
            frustrate = true;
        }
        if (curDialog == 3
            && global.state == GlobalData.STATES.ENDSTATE)
        {
            //wait until they place the image and time slider
            frustrate = true;
        }

        TextEnable();
        
        
      


    }


    // Update is called once per frame
    void Update()
    {
        //this pauses the normal time iteration when needed
        if(frustrate)
        {
            timer = Time.time;
        }

        //to unfrustrate after set period 
        if (releaseTimer > 0 && Time.time - releaseTimer > releaseTime)
        {
            frustrate = false;
            releaseTimer = -1;
            timer = -1;
            TextEnable();
        }


        //for now just whip through on a timer
        if (Time.time - timer > 2 && curDialog >= 0 )
        {
            iterateChat();
            timer = Time.time;
        }
        
    }
    public void TextEnable() 
    {

        Debug.Log("cur dialog " + curDialog);

        outbox.text = dialog02[curDialog];
        inbox.text = dialog01[curDialog];
        thirdbox.text = dialog03[curDialog];
        toolbot.text = toolBot[curDialog];

        enableBox(inbox, dialog01[curDialog].Length > 1);
        enableBox(outbox, dialog02[curDialog].Length > 1);        
        enableBox(thirdbox, dialog03[curDialog].Length > 1);
        enableBox(toolbot, toolBot[curDialog].Length > 1);

    }
    private void enableBox(InputField which, bool enable) 
    {
        //if(which.gameObject.activeSelf == false )
        {
            if (enable)
                which.GetComponent<InBox>().PlaySound();
        }
        
        which.gameObject.SetActive(enable);

        
    }

    public void releaseFrustrate(float t)
    {

        releaseTime = t;
        releaseTimer = Time.time;

    }


}
