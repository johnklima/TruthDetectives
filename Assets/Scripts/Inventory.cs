using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    private bool showme = true;
    private float pos;
    
    private StudioEventEmitter emitter;
    public GameObject view;
    public InputField text;
    public RawImage[] images;
    public int curImage = 0;  //start of play

    public Button enhance;


    // Start is called before the first frame update
    void Start()
    {

        //view.SetActive(true);

        //get the fmod event
        emitter = GetComponent<StudioEventEmitter>();
   

    }

    // Update is called once per frame
    void Update()
    {
       

       


    }
    
    public void onStillsClick()
    {
        Debug.Log("STILLS CLICK");
        //HACKY!!

        curImage = 2;
        GlobalData glo = Camera.main.GetComponent<GlobalData>();
        if (glo.state == GlobalData.STATES.CHAP5)
        {
            curImage = 0;
            //hide the video, show the first still
            images[curImage].GetComponent<ImageItem>().OnClick();
            //advance the dialog
            //glo.dialogTree.releaseFrustrate(1);

        }
            

        if (curImage < images.Length)
            images[curImage].gameObject.SetActive(true);
        curImage++;
        if (curImage < images.Length)
            images[curImage].gameObject.SetActive(true);
        curImage++;
        if (curImage < images.Length)
            images[curImage].gameObject.SetActive(true);
        curImage++;
        if(curImage < images.Length)
            images[curImage].gameObject.SetActive(true);

        //enhance.gameObject.SetActive(true);
       


    }

    public void onButtonClick()
    {
        showme = !showme;

        view.SetActive(true);

        if (!emitter.IsPlaying())
            emitter.Play();

        if(text.text == "#KafrNablAttack")
        {
            Debug.Log("TEXT CLICK 1");
            curImage = 1;
            images[curImage].gameObject.SetActive(true);
            //show next stuff
        }
        if (false && text.text == "#KafrNablAttack" ) //"#AlHikmaHospital")
        {
            Debug.Log("TEXT CLICK 2");
            curImage = 2;
            images[curImage].gameObject.SetActive(true);
            curImage++;
            images[curImage].gameObject.SetActive(true);
            

            //show next stuff
        }


    }
}
