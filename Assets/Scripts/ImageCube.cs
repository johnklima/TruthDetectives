using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageCube : MonoBehaviour
{

    bool show = false;
    public bool enhanced = false;
    public bool flattened = false;
    public Texture enhancedMat;
    public Texture normalMat;
    public Texture flatMat;

    public GameObject video;

    //radio blue water redblu hospital green building redred

    private Vector2 matScale = new Vector2(1,1);
    private Vector2 matPos;
    private  Material mat;
    public GameObject pinnedImage;
    GlobalData glo;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        glo = Camera.main.GetComponent<GlobalData>();
        if (glo.state == GlobalData.STATES.CHAP3)
        {
            //setAlpha(0.75f);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (pinnedImage == null)
            return;

        float twist = Input.GetAxis("Horizontal");
        if (twist != 0)
        {
            transform.Rotate(new Vector3(0, 0, twist ));
            Debug.Log("twist " + twist);

            pinnedImage.transform.GetComponent<VideoSurface>().setTwist(twist);
        }



    }

    public void setAlpha(float a)
    {


        Color color = mat.color;
        color.a = 0.75f;

        mat.color = color;
        GetComponent<MeshRenderer>().material = mat;
    }

    public void setMatScale(Vector2 s)
    {
        return;
        matScale = s;
        if (false)
        {
            MeshRenderer mr = pinnedImage.GetComponent<MeshRenderer>();
            mr.material.mainTextureScale = s;
        }

    }
    public void setMatPos(Vector2 p)
    {

        return;
        matPos = p;
        if (false)
        {
            MeshRenderer mr = pinnedImage.GetComponent<MeshRenderer>();
            mr.material.mainTextureOffset = p;
        }

    }

    public void onOverlayClick()
    {
        
        show = !show;
        transform.gameObject.SetActive(show);
        video.SetActive(false);


    }

    public void onFlattenClick()
    {
        if (enhanced)
        {
            flattened = !flattened;
            if (flattened)
                mat.mainTexture = flatMat;
            else
                mat.mainTexture = enhancedMat;

            mat.mainTextureScale = matScale;
            mat.mainTextureOffset = matPos;
            GetComponent<MeshRenderer>().material = mat;
            if (true)
            {
                MeshRenderer mr = pinnedImage.GetComponent<MeshRenderer>();
                mr.material = mat;
            }
        }

    }
    public void reloadImages()
     {

        Debug.Log("ImageCube.reloadImages");
        mat = GetComponent<MeshRenderer>().material;
        mat.mainTexture = normalMat;
        

     }
    public void onEnhanceClick()
    {

        //enhanced = !enhanced;
        enhanced = true;
        if (enhanced)
        {
            //TODO: hacky UI fix...
            if (mat && enhancedMat)
            {
                GlobalData glo = Camera.main.GetComponent<GlobalData>();
                mat.mainTexture = enhancedMat;
                if(glo.state== GlobalData.STATES.CHAP2)
                {
                    //glo.dialogTree.releaseFrustrate(1);
                    Debug.Log("release frustrate from image");
                }
                   
            }                
            else
                return;
         
        }
           
        else
            mat.mainTexture = normalMat;

        //mat.mainTextureScale = matScale;
        //mat.mainTextureOffset = matPos;

        //GetComponent<MeshRenderer>().material = mat;
        //MeshRenderer mr = pinnedImage.GetComponent<MeshRenderer>();
        //mr.material = mat;

        //show the 3d image surface??



    }
    private void OnApplicationQuit()
    {

    }
   
}
