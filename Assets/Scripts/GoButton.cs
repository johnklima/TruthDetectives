using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoButton : MonoBehaviour
{

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {

        //TODO: put all navigation buttons inside go
        GlobalData glo = Camera.main.GetComponent<GlobalData>();

        if (glo.state == GlobalData.STATES.CHAP3_2)
        {
            //user must return
            SceneManager.LoadScene("chapter2return");

        }
        else if (glo.state == GlobalData.STATES.CHAP3)
        {
            //user must return
            SceneManager.LoadScene("chapter2return");

        }
        else if (glo.state == GlobalData.STATES.CHAP4)
        {
            SceneManager.LoadScene("chapter5");
        }
    }
}
