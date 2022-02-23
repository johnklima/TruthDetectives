using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShow : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject obj;
    public KeyCode key = KeyCode.J;
    private bool isVisible = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            obj.SetActive(!obj.activeSelf);
        }
            
    }
}
