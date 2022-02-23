using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageTarget : MonoBehaviour
{

    public GameObject slider;
    private bool oneshot = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onCloseEnough()
    {

        if (oneshot)
            return;

        Debug.Log("oncloseenough");

        GlobalData glo = Camera.main.GetComponent<GlobalData>();
        if (glo.state == GlobalData.STATES.CHAP4)
        { 
        
        }

        if (glo.state == GlobalData.STATES.CHAP5)
        {
            oneshot = true;
            slider.SetActive(true);

            glo.state = GlobalData.STATES.ENDSTATE;
        }


    }
}
