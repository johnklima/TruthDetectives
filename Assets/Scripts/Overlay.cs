using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlay : MonoBehaviour
{

    private Material OverlayMat;
    public Transform OverlayObj;
    public string loadSceneName = "";

    // Start is called before the first frame update
    void Start()
    {
        OverlayMat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //TODO: what was I thinking here??
    private void OnMouseUpAsButton()
    {
        Debug.Log(transform.name + " clicked up, not sure what I want to do here???");        
        
        if(OverlayObj)
            OverlayObj.GetComponent<MeshRenderer>().material.mainTexture = OverlayMat.mainTexture;
    
        //do i load a new scene?? how do i go back?
        if(loadSceneName.Length > 0)
        {
            Camera.main.GetComponent<GlobalData>().loadScene(loadSceneName);
        }
    }
    
}
