using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
	public bool lockCursor = true;
	public float mouseSensitivity = 2;
	public Transform target;
	public float dstFromTarget = 0;
	public Vector2 pitchMinMax = new Vector2(-35, 90);

    public Transform imageTarget;

	public float rotationSmoothTime = 0;
	Vector3 rotationSmoothVelocity;
	Vector3 currentRotation;

	float yaw;
	float pitch;

    bool detatch = false;

    public Transform camTarget;

	private float cameraPitch;

    //for to override gui elements click through into scene
    private bool isPointerOverUIObject()
    {

        return false;
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 1; //ignore panel which is always full screen alpha 0
    }

    void Start()
	{
		if (lockCursor)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
    public void transitionCamera()
    {
        
    }

    private void Update()
    {

      


        if (!detatch)
            return;

        if(imageTarget)
        {

            Vector3 a = transform.position;
            Vector3 b = imageTarget.position;

            transform.position = Vector3.Lerp(a, b, Time.deltaTime);

            Quaternion q1 = transform.rotation;
            Quaternion q2 = imageTarget.rotation;

            transform.rotation = Quaternion.Slerp(q1, q2, Time.deltaTime);


        }


    }

    void LateUpdate()
	{

        if(detatch)
        {
            //animated camera
            return;
        }


        if (isPointerOverUIObject())
            return;

        //check distance and angle to targetImage (if i have one)
        if(imageTarget)
        {
            float dist = Vector3.Distance(imageTarget.position, transform.position);
            float angle = Mathf.Abs(Quaternion.Angle(imageTarget.rotation, transform.rotation));
            //Debug.Log(dist + " dist " + angle + "angle");
            if (angle < 20 && dist < 10)
            {
                transitionImageTarget();
                imageTarget.GetComponent<ImageTarget>().onCloseEnough();
            }

        }


		yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
		pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

		currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

		Quaternion camRotation = Quaternion.Euler(currentRotation.x,currentRotation.y,0);
		Quaternion camTargetRotation = Quaternion.Euler(0,currentRotation.y,0);

		target.transform.rotation = camTargetRotation;
		transform.rotation = camRotation;

	}
    public void transitionImageTarget()
    {
        if(imageTarget)
        {

            detatch = true;
        }

    }
    
}