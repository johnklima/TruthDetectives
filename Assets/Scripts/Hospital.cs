using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hospital : MonoBehaviour
{

    public GameObject[] evidence;
    private int cur = 0; 
    // Start is called before the first frame update    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            evidence[cur].SetActive(true);
            cur++;
            if (cur > evidence.Length - 1)
                cur = 0;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            for(int i = 0; i < evidence.Length; i++)
                evidence[i].SetActive(false);
        }

    }
}
