using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

//TODO: code duplication with out/in boxes, prolly inherit...

public class InBox : MonoBehaviour
{
    //public string text = "";
    private InputField input;

   
    public StudioEventEmitter emitter;

    void Start()
    {
        input = GetComponent<InputField>();
        //input.text = text;

    }
    void Update()
    {
        //text = input.text;
    }


    public void PlaySound()
    {
        if (!emitter.IsPlaying())
            emitter.Play();
    }
}
