using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OverlayFader : MonoBehaviour
{

   
    public Material normalMat;

    private Material mat;
    public GameObject pinnedImage;
    public GameObject goButton;
    public Transform image1;
    public Transform image2;
    public Transform image3;
    //public Transform image4;

    public Transform targetBox;

    CameraController cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraController>();
        mat = GetComponent<MeshRenderer>().material;
        mat.mainTexture = image1.GetComponent<MeshRenderer>().material.mainTexture;
        //GetComponent<MeshRenderer>().material = mat;

        Color color = mat.GetColor("_Color");
        color.a = 0.75f;

        mat.SetColor("_Color", color);
        GetComponent<MeshRenderer>().material = mat;

        //set the target position for start image
        if (image1)
            cam.imageTarget = image1.GetComponent<Evidence>().imageTarget;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 scroll = Input.mouseScrollDelta;

        if(scroll.y != 0 || scroll.x != 0)
        {
            Color color = mat.GetColor("_Color");
            color.a = color.a + scroll.y * Time.deltaTime * 10;
            if (color.a > 1)
                color.a = 1;
            if (color.a < 0)
                color.a = 0;

            mat.SetColor("_Color", color);
            GetComponent<MeshRenderer>().material = mat;
        }


        if (Input.GetKeyUp(KeyCode.Space ))
        {
            Debug.Log("FIRE");
            if (!targetBox) //chap 5 function
                return;

            if (image1)
            {
                //put image at my location and rotation 
                //using target box to fix rotations
                image1.position = targetBox.position;
                image1.rotation = targetBox.rotation;
                image1.gameObject.SetActive(true);
                //swap next material
                mat.mainTexture = image2.GetComponent<MeshRenderer>().material.mainTexture;
                GetComponent<MeshRenderer>().material = mat;

                //end the image
                image1 = null;
                if (image2)
                    cam.imageTarget = image2.GetComponent<Evidence>().imageTarget;
            }
            else if (image2)
            {

                image2.position = targetBox.position;
                image2.rotation = targetBox.rotation;
                image2.gameObject.SetActive(true);
                //image2.rotation = transform.rotation;

                mat.mainTexture = image3.GetComponent<MeshRenderer>().material.mainTexture;
                GetComponent<MeshRenderer>().material = mat;
                image2 = null;
                if (image3)
                    cam.imageTarget = image3.GetComponent<Evidence>().imageTarget;
            }
            else if (image3)
            {
                image3.position = targetBox.position;
                image3.rotation = targetBox.rotation;
                image3.gameObject.SetActive(true);
                //image3.rotation = transform.rotation;
                gameObject.SetActive(false);
      
                GetComponent<MeshRenderer>().material = mat;
                image3 = null;
                Debug.Log("DONE");
                enableNext();


            }
            


            Color color = mat.GetColor("_Color");
            color.a = 0.75f;

            mat.SetColor("_Color", color);
            GetComponent<MeshRenderer>().material = mat;

        }

    }
    public void enableNext()
    {
        goButton.SetActive(true);
    }
}
