using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

//TODO: hmm more code duplication "SlidingGUI" class?
public class MessageScrollView : MonoBehaviour
{

    private bool showme = false;
    public float pos;
    private RectTransform rect;

    private Vector3 restPos;
    private Vector3 startPos;
    private float speed = 1000;

    private StudioEventEmitter emitter;
    // Start is called before the first frame update
    void Start()
    {
        //get the fmod event
         emitter = GetComponent<StudioEventEmitter>();

        //TODO: inherit?
        StartMove();

    }
    void StartMove()
    {
        rect = GetComponent<RectTransform>();

        //buffer design time position
        restPos = rect.transform.localPosition;
        startPos = new Vector3(restPos.x + 200, restPos.y, restPos.z);

        //hardcode start pos for now, and maybe forever
        rect.transform.localPosition = startPos;

        //buffer the start pos for easy accounting
        pos = rect.transform.localPosition.x;

        //panel is now "hidden" off screen
    }

    void UpdateMove()
    {
        //TODO: use restPos for final accounting
        if (showme && pos > restPos.x)
        {
            
            if (!emitter.IsPlaying())
                emitter.Play();

            float delta = Time.deltaTime * speed;

            //slide it
            pos -= delta;
            if (pos < restPos.x)
            {
                pos = 0;
                
                rect.transform.localPosition = restPos; //lock at design time position
                emitter.Stop();
            }
            else
                rect.transform.Translate(-delta, 0, 0); //otherwise, move me left
        }
        else if (!showme && pos == 0)
        {
            //hide me sliding right
            float delta = Time.deltaTime * speed;

            rect.transform.Translate(delta, 0, 0);
            if (rect.transform.localPosition.x > startPos.x)
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
    public void hide()
    {
        showme = false;
    }
    // Update is called once per frame
    void Update()
    {
        UpdateMove();
        
    }
}
