using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//this is for the "wrong" location trigger, which sends us back to sat image
public class LocationTrigger : MonoBehaviour
{

    float timer = -1;
    float scenetimer = -1;
    public DialogTree dialog;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //iterate the chat
        if(timer > 0 && Time.time - timer > 0.01f)
        {
            dialog.frustrate = false;
            scenetimer = Time.time;
            Debug.Log("scenetimer set");
            timer = -1;

        }
        if (scenetimer > 0 && Time.time - scenetimer > 4.0f)
        {
            Debug.Log("scenetimer load");
            SceneManager.LoadScene("chapter2return");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("object enter");
        timer = Time.time;
    }
}
