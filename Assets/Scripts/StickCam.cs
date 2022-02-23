using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StickCam : MonoBehaviour
{

    //public Transform target;
    public Transform cameraTarget;
    public Transform cameraTargetRotator;
    public Transform objectTarget;
    public Transform objectTargetRotator;
    public Camera stickCam;
    public Vector3 clickStartPos;
    private bool doItCamera;
    private bool doItObject;
    
    private bool doItTransition = false;
    private float transitionIncrement = 0;
    private Quaternion transitionStartRot;
    private float transitionZ;
    private float transitionStartZ;

    public ObjectTwister objTwister;
    public Transform selected;
    private float interpSpeed = 1.0f;
    private GlobalData global;

    private float initialTilt;
    private float tilt = 0;
    private CameraTwister camTwister;

    private Vector3 lastMousePos;

    private int selType = 0;
    // Start is called before the first frame update
    void Start()
    {
        //our camera on the stick
        stickCam = Camera.main;
        //convenient place for global data, more or less a singleton
        global = stickCam.GetComponent<GlobalData>();
        //look up/down value
        initialTilt = stickCam.transform.localEulerAngles.x;
        //rotation around the z on the stick
        camTwister = GetComponent<CameraTwister>();
    }

    //for to override gui elements click through into scene
    private bool isPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 1; //ignore panel which is always full screen alpha 0
    }

    // Update is called once per frame
    void Update()
    {
        if(doItTransition)
        {
            UpdateTransition();
        }        
        else if (!isPointerOverUIObject())
        {

            //have the image cube of the camera overide object manipulation
            //and camera position
            if (!InputUpdateImageCube())
            {
                InputUpdateCamera();
                InputUpdateObjects();
            }

        }
        //buffer for delta mouse drags
        lastMousePos = Input.mousePosition;
    }
    void UpdateTransition()
    {
        Quaternion q2 = (cameraTargetRotator.rotation);
        Quaternion q1 = transitionStartRot;

        transitionIncrement += Time.deltaTime;
        transform.rotation = Quaternion.Lerp(q1, q2, transitionIncrement);

        //transition the Z
        float z = Mathf.Lerp(transitionStartZ, transitionZ, transitionIncrement);
        Vector3 pos = stickCam.transform.localPosition;
        pos.z = z;
        stickCam.transform.localPosition = pos;
        


        if (transitionIncrement > 1.0f)
        {
            doItTransition = false;
        }
        
    }
    
    bool InputUpdateImageCube()
    {

        return false;

        //to scroll and zoom the camera's child object image cube, which
        //an unlit cube behaving as a 2d GUI element
        if (Input.GetMouseButton(0)) //left mouse
        {
            RaycastHit hit;

            LayerMask mask = 1 << 12; //IMAGECUBE layer


            //get the ray
            Ray ray = stickCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 10000.0f, mask, QueryTriggerInteraction.Ignore))
            {
                
                //get the image cube's material and twiddle the position
                //WARNING: Hierarchy dependant
                Transform cube = stickCam.transform.GetChild(0);
                //cube.GetComponent<ImageCube>().setMatPos(Input.mousePosition - lastMousePos);

                
                MeshRenderer mr = cube.GetComponent<MeshRenderer>();

                Vector2 pos = mr.material.mainTextureOffset;
               
                pos.x -= (Input.mousePosition.x - lastMousePos.x) * Time.deltaTime *0.1f;
                pos.y -= (Input.mousePosition.y - lastMousePos.y) * Time.deltaTime *0.1f;
                                
               
                Debug.Log("pos " + pos.ToString());

                
                mr.material.mainTextureOffset = pos;

                cube.GetComponent<ImageCube>().setMatPos(pos);

                return true;
            }

        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if(scroll != 0)
        {

            //TODO: roll this all into one function

            RaycastHit hit;

            LayerMask mask = 1 << 12; //IMAGECUBE layer
            
            //get the ray
            Ray ray = stickCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 10000.0f, mask, QueryTriggerInteraction.Ignore))
            {

                //get the image cube's material and twiddle the tiling
                //WARNING: Hierarchy dependant

                Transform cube = stickCam.transform.GetChild(0);
                MeshRenderer mr = cube.GetComponent<MeshRenderer>();

                Vector2 scale = mr.material.mainTextureScale;
                scale.x += scroll * Time.deltaTime;
                scale.y += scroll * Time.deltaTime;

                Debug.Log("scale " + scale.ToString() );
                if (scale.x < 1.0f)
                {
                    mr.material.mainTextureScale = scale;
                    cube.GetComponent<ImageCube>().setMatScale(scale);
                }
                return true;
            }
        }

        return false;
    }
    void InputUpdateCamera() 
    {
               

        Vector3 pos = stickCam.transform.localPosition;
        Quaternion rot = stickCam.transform.localRotation;

        

        if (Input.GetMouseButtonDown(1)) //right mouse
        {
            
            RaycastHit hit;

            LayerMask mask = 1 << 9; //EARTH layer            

            //get the ray
            Ray ray = stickCam.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit, 10000.0f, mask, QueryTriggerInteraction.Ignore))
            {

                Debug.Log(hit.collider.name );

                GameObject obj = hit.collider.gameObject;
                Transform which = hit.collider.transform;

                
                Debug.Log(hit.point);

                //move the target to the click position
                cameraTarget.position = hit.point;

            }
        }
        

        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;

            LayerMask mask = 1 << 9; //EARTH layer

            /*
            //buffer current cam rotation
            Quaternion Q = stickCam.transform.localRotation;

            //set it top down square to the sphere
            stickCam.transform.localRotation = Quaternion.Euler(0, 180, 0);

            //get the ray
            Ray ray = stickCam.ScreenPointToRay(Input.mousePosition);

            //reset the camera
            Camera.main.transform.localRotation = Q;
            */

            //move the camera up, and look straight to the center of the earth
            Vector3 clickpos = stickCam.transform.localPosition;
            //lift it if needed
            if(clickpos.z < 1000)
                clickpos.z = 1000;

            stickCam.transform.localPosition = clickpos;
            stickCam.transform.LookAt(Vector3.zero);

            //get the ray
            Ray ray = stickCam.ScreenPointToRay(Input.mousePosition);

            //restore
            stickCam.transform.localPosition = pos;
            stickCam.transform.localRotation = rot;

            if (Physics.Raycast(ray, out hit, 10000.0f, mask, QueryTriggerInteraction.Ignore))
            {

                Debug.Log(hit.collider.name);

                GameObject obj = hit.collider.gameObject;
                Transform which = hit.collider.transform;

                //Debug.Log(hit.point);

                cameraTarget.position = hit.point;
                doItCamera = true;

                cameraTargetRotator.LookAt(cameraTarget.position);


            }

        }


        float zoom = Input.GetAxis("Mouse ScrollWheel");
        bool shift = Input.GetKey(KeyCode.LeftShift);
        if (!shift && zoom !=0)
        {
            pos.z += zoom * Time.deltaTime * 300;
            stickCam.transform.localPosition = pos;
            doItCamera = false; //stop camera motion
        }
        else if (zoom != 0)
        {
            tilt += zoom * Time.deltaTime * 300;
            Vector3 tiltRot = stickCam.transform.localEulerAngles;
            tiltRot.x = initialTilt + tilt;
            stickCam.transform.localEulerAngles = tiltRot;
            doItCamera = false; //stop camera motion
        }
        if (Input.GetMouseButtonUp(1))
        {
            doItCamera = false;
        }

        if (doItCamera)
        {
            Quaternion q2 = (cameraTargetRotator.rotation);
            Quaternion q1 = transform.rotation;

            //interp speed some function of height?
            float t = Time.deltaTime * Mathf.Sqrt(pos.z) * 0.01f * interpSpeed;
            if (t > 0.0008f)
                t = 0.0008f;

            transform.rotation = Quaternion.Lerp(q1, q2, t);
            //if (Quaternion.Angle(q1, q2) > 0.1f)
            //    interpSpeed = 1.0f;
        }

        //frustrate the z pos update based on twist value
        Quaternion rot2 = transform.rotation;
        Vector3 euler = rot2.eulerAngles;
        euler.z = camTwister.twist;
        rot.eulerAngles = euler;
        transform.rotation = rot;

    }

    void InputUpdateObjects()
    {      
        

        if (Input.GetMouseButtonDown(0))
        {
            global.selected = null;
            RaycastHit hit;

            LayerMask maskObjs = 1 << 10; //OBJECTS layer
            LayerMask maskMaps = 1 << 11; //MAPS layer


            //get the ray
            Ray ray = stickCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 10000.0f, maskObjs, QueryTriggerInteraction.Ignore))
            {
                selType = 1;

                //select the object
                selected = hit.collider.transform;
                global.selected = selected;

                //fire play sound if it has one
                BuildingRotator brot = selected.parent.GetComponent<BuildingRotator>();

                if (brot)
                {
                    //TODO: refactor this
                    if (!brot.movable)
                    {
                        selected = null;
                        global.selected = null;
                        return;
                    }
                        

                    brot.setPlay(true);                    
                }
                 
                Rotator rota = selected.parent.GetComponent<Rotator>();
                if (rota)
                {
                    if (!rota.movable)
                    {
                        selected = null;
                        global.selected = null;
                        return;
                    }

                    rota.setPlay(true);
                }




                //cancel/frustrate previous twister
                if (objTwister)
                    objTwister.isSelected = false;

                //select this one, so the object twister is listening to keypress
                //to change it's local rotation
                objTwister = selected.GetComponent<ObjectTwister>();
                //not all objects will have a twister (eg. VideoSurface)
                if (objTwister)
                    objTwister.isSelected = true;
            }
            else if (Physics.Raycast(ray, out hit, 10000.0f, maskMaps, QueryTriggerInteraction.Ignore))
            {
                selType = 2;
                
                //select the object
                selected = hit.collider.transform;
                //in the case of the map, we grab the parent rotator
                global.selected = selected.parent;

                //cancel/frustrate previous
                if (objTwister)
                    objTwister.isSelected = false;

            }



        }
        //kind of an ugly check, somewhere a mouseup happens with nothing selected
        if (Input.GetMouseButtonUp(0) && selected && selected.parent)
        {
            //kill play sound if it has one
            BuildingRotator brot = selected.parent.GetComponent<BuildingRotator>();
            if (brot)
                brot.setPlay(false);

            //release what I grabbed
            selected = null;
            doItObject = false;
            Cursor.visible = true;
        }

        if (selected && selType == 1) //OBJECTS
        {


            //Move what I grabbed

            //check if locked
            BuildingRotator brot = selected.parent.GetComponent<BuildingRotator>();
            if (brot)
            {               
               //if (brot.locked)
               //     return;
            }



            //get the rotator for the object, which should always be just the parent
            //if asset is well exported
            Transform rotator = selected.parent;

            RaycastHit hit;

            //now we mask on the main sphere
            LayerMask mask = 1 << 9; //EARTH layer

            //get the ray
            Ray ray = stickCam.ScreenPointToRay(Input.mousePosition);



            if (Physics.Raycast(ray, out hit, 10000.0f, mask, QueryTriggerInteraction.Ignore))
            {

                Cursor.visible = false;

                //Debug.Log(hit.collider.name);
                objectTarget.position = hit.point;
                objectTargetRotator.LookAt(objectTarget.position);

                doItObject = true;

                Quaternion q2 = (objectTargetRotator.rotation);
                Quaternion q1 = rotator.rotation;

                //interp speed some function of camera height?
                rotator.rotation = Quaternion.Lerp(q1, q2, Time.deltaTime );



                //rotate to the target position
                //rotator.LookAt(target.position);

                /*
                //correct for difference between pivot point and center point of the object
                //buffer the pivot rot
                Quaternion pivotQuat = rotator.rotation;

                
                //find the new center of the object
                Vector3 center = (selected.GetComponent<Renderer>().bounds.center);
                //look there instead
                rotator.LookAt(center);
                //buffer that rot
                Quaternion centerQuat = rotator.rotation;

                //look again at the target
                rotator.LookAt(target.position);

                //rotate by the difference
                rotator.Rotate(pivotQuat.eulerAngles - centerQuat.eulerAngles);
                */

            }


        }
        if (selected && selType == 2) //MAPS
        {
            doItObject = false;
            //Handled in the map rotator
        }
    }
    public void transitionCamera(Quaternion rot, float z)
    {

        cameraTargetRotator.rotation = rot;  //final rotation of camera
        transitionStartRot = transform.rotation; //initial rotation of camera
        transitionStartZ = stickCam.transform.localPosition.z;
        transitionZ = z;
        transitionIncrement = 0; //start, count to 1
        doItTransition = true;


    }
}
