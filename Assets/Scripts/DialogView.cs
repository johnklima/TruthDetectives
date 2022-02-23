using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogView : MonoBehaviour
{
    public RawImage dialogBox;          //wrapper container (uber widget with buttons etc..)
    private InputField inputField;       //UI element to stuff from current dialog
    public Dialog dialog;               //the current or last dialog text and config
    public Scrollbar vertslider;
    public RawImage newDialogBox;

    private bool firstDialog = true;

    //hack the scroll bar
    int refresh = 0;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (refresh > 0)
        {
            Refresh();
        }

        
    }

    public void newDialog(Dialog newDialog)
    {

        
        newDialogBox = Instantiate(dialogBox,transform); //clone the current dialogBox
        

        //set the current dialog
        dialog = newDialog;

        //configure the current from the new
        configureBox(newDialogBox);
        
        //hack to refresh the scrollbar
        refresh = 1;

    }
    void Refresh()
    {
        //HACK force the scrollbar for 10 frames so it has time to check the content changed
        vertslider.value = 0; //force the scroll
        
        refresh++;
        if (refresh > 10)
            refresh = 0;

        //also hack of the widgets

       
    }
    void configureBox(RawImage box)
    {
        inputField = box.transform.GetChild(0).GetComponent<InputField>();
        ColorBlock cb = inputField.colors;
        cb.normalColor = dialog.color;
        cb.highlightedColor = dialog.color;
        cb.pressedColor = dialog.color;
        cb.selectedColor = dialog.color;
        inputField.colors = cb;
        inputField.text = dialog.text;
        inputField.readOnly = true;

        sizeBox(box);
    }
    void sizeBox(RawImage box)
    {

       

        float L = CalculateLength(inputField);

        Debug.Log("SIZE BOX " + L);

        float H = 24; //this is just not linear :( must change with font size change ):

        //size the vert of the input field

        
        RectTransform trans = inputField.GetComponent<RectTransform>();
        L = (L / trans.rect.width) + 1;
        Debug.Log("LINES  " + L);  
        float V = (L) * H;

        DialogBox content = box.GetComponent<DialogBox>();

        //we accumulate V based on pix and buttons   
        Debug.Log("IMAGES " + dialog.images.Length);
        for (int i = 0; i < dialog.images.Length; i++)
        {
            //fixed length array warning!! 
            content.images[i].gameObject.SetActive(true);  //show it
            
            //pass the texture from the config (pool) to the view
            content.images[i].texture = dialog.images[i].texture;

            //accumulate height
            V += content.images[i].rectTransform.rect.height ;

        }
        Debug.Log("BUTTONS " + dialog.buttons.Length);
        for (int i = 0; i < dialog.buttons.Length; i++)
        {
            //fixed length array warning!! 
            content.buttons[i].gameObject.SetActive(true);  //show it           
        }
        
        //size the input field
        trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, V);

        //finally size its container (add space when buttons and images included)
        // add space for 1 row of buttons
         if (dialog.buttons.Length > 0)
                V += 30;

        trans = box.GetComponent<RectTransform>();
        trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, V );


    }

    int CalculateLength(InputField text)
    {
        int totalLength = 0;

        Debug.Log(text.text);

        Font myFont = text.textComponent.font;  
        CharacterInfo characterInfo = new CharacterInfo();

        char[] arr = text.text.ToCharArray();

        foreach (char c in arr)
        {
            myFont.GetCharacterInfo(c, out characterInfo, text.textComponent.fontSize);           
            totalLength += characterInfo.advance;
        }

        return totalLength;
    }
}
