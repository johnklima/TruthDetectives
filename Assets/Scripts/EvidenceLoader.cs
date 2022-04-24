using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceLoader : MonoBehaviour
{

    float timer = -1;
    public string nextSceneName = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //pause then load scene
        if(Time.time - timer > 2 && timer > 0)
        {

            GlobalData glo = Camera.main.GetComponent<GlobalData>();
            glo.loadScene(nextSceneName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "evidence")
        {
            Debug.Log("EVIDENCE LOADER ENTER " + transform.name);
            timer = Time.time;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "evidence")
        {
            Debug.Log("EVIDENCE LOADER STAY " + transform.name);
            
        }

    }
   
}
