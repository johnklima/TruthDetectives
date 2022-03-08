using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class ImageItem : MonoBehaviour
{

    public Transform VideoCube;
    public Transform ImageCube;
    public VideoClip clip;
    public Texture EnhancedImage;
    public Texture NormalImage;    
    public GameObject evidenceRotator;
    public RectTransform highlighter;
    // Start is called before the first frame update
    void Start()
    {
        //if we dont have a normal image, replace it with this raw image
        if(!NormalImage)
            NormalImage = GetComponent<RawImage>().texture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick(bool on = true)
    {

        RectTransform rect = GetComponent<RectTransform>();
        GlobalData glo = Camera.main.GetComponent<GlobalData>();
        Debug.Log("IMAGE CLICK " + glo.state.ToString() + " on=" +on);

        //move the highlighter and be sure it is visible
        highlighter.gameObject.SetActive(true);
        highlighter.SetPositionAndRotation(rect.position, rect.rotation);

        //special case hide video in chap 5
        if (glo.state == GlobalData.STATES.CHAP5)
        {
            VideoCube.gameObject.SetActive(!VideoCube.gameObject.activeSelf);
            if(VideoCube.gameObject.activeSelf == true)
                VideoCube.GetComponent<PlayVideo>().playVideo();
            else
                VideoCube.GetComponent<PlayVideo>().stopVideo();            

        }

        
        if (clip)
        {
            //TODO: find where I actually load the next video???

            //HACK FIX:

            if (!VideoCube)
                return;

            //IF NOT CHAP5
            //if (glo.state != GlobalData.STATES.CHAP5)
            {
                VideoCube.GetComponent<PlayVideo>().scaleMe(1);
                VideoCube.gameObject.SetActive(true);
                Texture tex = GetComponent<RawImage>().texture;



                VideoCube.GetComponent<MeshRenderer>().material.mainTexture = tex;
                VideoCube.GetComponent<VideoPlayer>().clip = clip;

                if (on)
                    VideoCube.GetComponent<VideoPlayer>().Play();
                else
                    VideoCube.GetComponent<VideoPlayer>().Pause();

                //ImageCube.gameObject.SetActive(false);

            }


            

            
        }
        else if (NormalImage && ImageCube)
        {
            ImageCube.gameObject.SetActive(true); //tricky tricky
            ImageCube.GetComponent<ImageCube>().normalMat = NormalImage;
            
            if(EnhancedImage)
            {
                ImageCube.GetComponent<ImageCube>().enhancedMat = EnhancedImage;
                //once enhanced always enhanced - bug fix
                if(ImageCube.GetComponent<ImageCube>().enhanced)
                    ImageCube.GetComponent<ImageCube>().normalMat = EnhancedImage;
            }
                
            
            if(VideoCube)
                VideoCube.gameObject.SetActive(false);
            
            if(ImageCube)
                ImageCube.GetComponent<ImageCube>().reloadImages();

            if (evidenceRotator)
                evidenceRotator.SetActive(true);

            if(glo.state == GlobalData.STATES.CHAP3)
                ImageCube.gameObject.SetActive(false); //hide the cube in obj placement mode



        }

    }
    
    
}
