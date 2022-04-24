using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CorrectLocationTrigger : MonoBehaviour
{

    public GameObject radiotower;
    public GameObject hospital;
    public GameObject sidebuilding;
    public GameObject mosque;

    public bool next = false;
    public GameObject nextButton;

    int state = 0;
    float timer = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        

        if(timer > 0 && Time.time - timer > 2 && state == 0)
        {

            //quick and dirty error trap...
            if (radiotower)
            {
                radiotower.SetActive(true);
                hospital.SetActive(true);
                sidebuilding.SetActive(true);
                mosque.SetActive(true);
                state = 1;
                timer = Time.time;
                if (nextButton)
                    nextButton.SetActive(true);
            }
        }
        if (state == 1  && next )
        {
            SceneManager.LoadScene("chapter4");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("object entered trigger");
        GlobalData glo = Camera.main.GetComponent<GlobalData>();
        
        
        //glo.dialogTree.frustrate = false;      

        timer = Time.time;

        if (nextButton)
            nextButton.SetActive(true);

    }
}
