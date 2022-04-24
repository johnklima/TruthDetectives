using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowChat : MonoBehaviour
{

    public InputField text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            text.gameObject.SetActive(!text.gameObject.activeInHierarchy);
            text.GetComponent<InBox>().PlaySound();
        }
    }

}
