using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class OutBox : GUIState
{
    public string text = "first OutBox text";
    private InputField input;

    private bool showme = false;
    public float pos;
    private RectTransform rect;

    private Vector3 restPos;
    private Vector3 startPos;
    private StudioEventEmitter emitter;

    private Transform dismiss;
    

    private void Start()
    {
        input = GetComponent<InputField>();
        input.text = text;

        //get the fmod event
        emitter = GetComponent<StudioEventEmitter>();
      //  dismiss = transform.GetChild(0); //caution: button must be at zero

       // StartMove();


    }
    private void Update()
    {
        text = input.text;
        //UpdateMove();
        base.Update();
    }

    void StartMove()
    {
        rect = GetComponent<RectTransform>();

        //buffer design time position
        restPos = rect.transform.localPosition;
        startPos = new Vector3(200, restPos.y, restPos.z);

        //hardcode start pos for now, and maybe forever
        rect.transform.localPosition = startPos;

        //buffer the start pos for easy accounting
        pos = rect.transform.localPosition.x;
    }


    void UpdateMove()
    {
        if (showme && pos > restPos.x)
        {
            if (!emitter.IsPlaying())
                emitter.Play();

            float delta = Time.deltaTime * 500;

            pos -= delta;
            if (pos < restPos.x)
            {
                rect.transform.localPosition = restPos;
                emitter.Stop();
                dismiss.gameObject.active = true;
            }
            else
                rect.transform.Translate(-delta, 0, 0);
        }

    }

    public void show()
    {
        showme = true;

    }
    public void hide()
    {
        rect.transform.localPosition = startPos;
    }

}
