using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchScrollView : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject view;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            view.SetActive(!view.activeSelf);
    }
}
