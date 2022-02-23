using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{

    GlobalData glo;

    public ImageItem imageItem;
    public int index;
    
    // Start is called before the first frame update
    void Start()
    {
        glo = Camera.main.GetComponent<GlobalData>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        Debug.Log("CLICKED " + name + " " + glo.state.ToString());

        if(glo.state == GlobalData.STATES.CHAP1)
        {
            if(name == "B0")
            {
                imageItem = glo.imageItems[index];
                if (imageItem)
                {
                    imageItem.gameObject.SetActive(true);
                    imageItem.OnClick();
                }
            }
            if (name == "B1")
            {
                imageItem = glo.imageItems[index];
                if (imageItem)
                {
                    imageItem.gameObject.SetActive(true);
                    imageItem.OnClick(false); //pause it
                }
            }

        }
    }
}
