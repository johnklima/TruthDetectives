using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    // Start is called before the first frame update
    public string text;
    public Color color;

    //though an array, it will be fixed max size, 0-2 pics, 0-4 butts at design
    public RawImage[] images;  //from the images bank
    public Button[] buttons;   //TBD---
    public DialogView dialogView;

    private GlobalData glo;

    public bool done = false;
    public bool pause = false;

    private void Awake()
    {
        //when needed, easier to set at awake.
        glo = Camera.main.GetComponent<GlobalData>();
    }

    void Start()
    {
        //once enabled, fire off the view update
        //this will move to the dialog tree, driven by its hierarchy

        dialogView.newDialog( transform.GetComponent<Dialog>() );


    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public Transform newMessage()
    {

        //at the end of the list?
        if (transform.childCount < 1)
            return null;

        //get next dialog, given a tree, GetChild will be one of several indexes
        Dialog nextDialog = transform.GetChild(0).GetComponent<Dialog>();

        //activating will fire start above which automagically will populate
        //the dialog view with a new dialog, which then applies its configuration
        //as specified in the design time properties declared (color etc...)
        nextDialog.gameObject.SetActive(true);

        //return the child
        if(transform.childCount > 0)
            return transform.GetChild(0);
        else
            return null;

    }
}
