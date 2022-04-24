using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Rotator : GameStateObject
{

    public bool locked = false;
    public float angleLocked = 0.1f;
    public float angle;
    private StudioEventEmitter emitter;
    private bool play;
    private bool stop;
    public bool movable = false;


    public void setPlay(bool doIt)
    {
        play = doIt;
        if (play)
            stop = false;
        else
            stop = true;
    }
    private void Start()
    {
        emitter = GetComponent<StudioEventEmitter>();
    }
    private void Update()
    {

        if (!movable)
            return;

        if (play && emitter)
        {
            Debug.Log("play");

            emitter.Play();
            play = false;
        }
        else if (stop && emitter)
        {
            emitter.Stop();
        }

        //check if the player has the object in the correct place
        //for this we can just check angles.
        if (transform.tag == "checkcorrect" && locked == false)
        {
            angle = Quaternion.Angle(transform.rotation, correctrotation);

            if (Mathf.Abs(angle) < angleLocked)
            {
                //lock it down, score a point
               

                //locked = true;

                if (emitter)
                    emitter.SetParameter("51_param_to_end", 1);
            }
            else
            {
                locked = false;

                //increase volume of sound as it approaches the angle? or simply
                //assign an audio listener??
                if (emitter)
                {
                    //Debug.Log("param " + (10.0f - (angle * 10.0f)));
                    emitter.SetParameter("51_param_volume", 10.0f - (angle * 10.0f));
                }
            }
        }

    }
}
