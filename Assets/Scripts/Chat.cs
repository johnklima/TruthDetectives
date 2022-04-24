using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chat : MonoBehaviour
{    

    bool showstate = true;
    public GameObject chatscroll;

    public void onChatClick()
    {

        Debug.Log("HELLO CHATCLICK");
        
        //hide show chat boxes
        showstate = !showstate;         //toggle

        chatscroll.SetActive(showstate);
        
    }
   
}
