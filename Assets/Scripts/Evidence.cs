using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evidence : MonoBehaviour
{
    
    public Transform imageTarget;
    public Transform EvidenceLocations;

    //targets are established at run-time by copying locations, 
    //stopping the game, and pasteing the duplicates
    public Transform EvidenceTarget;

    public bool placed = false;

    private Material mat;
    private bool transition = false;
    private Quaternion startQ;
    private Vector3 startV;
    private float tQ;
    private float tV;
   
    void Start()
    {
        mat = imageTarget.GetComponent<MeshRenderer>().material;
        
        Color color = mat.GetColor("_Color");
        color.a = 0.75f;

        mat.SetColor("_Color", color);
        imageTarget.GetComponent<MeshRenderer>().material = mat;

    }

    // Update is called once per frame
    void Update()
    {
        if(transition)
        {
            tQ += Time.deltaTime * 0.3f;
            tV += Time.deltaTime * 0.5f;

            transform.rotation = Quaternion.Lerp(startQ, EvidenceTarget.rotation, tQ);
            transform.position = Vector3.Lerp(startV, EvidenceTarget.position, tV);

            if(tQ > 1 && tV > 1)
            {
                transition = false;

                //pop the next off the list
                if (transform.parent.childCount > 1)
                {
                    Transform next = transform.parent.GetChild(1);
                    next.gameObject.SetActive(true);
                }

                //remove me   
                transform.SetParent(EvidenceLocations);
                placed = true;

            }
            return;
        }

        if (placed)
            return;

        Vector2 scroll = Input.mouseScrollDelta;

        if (scroll.y != 0 || scroll.x != 0)
        {
            Color color = mat.GetColor("_Color");
            color.a = color.a + scroll.y * Time.deltaTime * 10;
            if (color.a > 1)
                color.a = 1;
            if (color.a < 0)
                color.a = 0;

            mat.SetColor("_Color", color);
            imageTarget.GetComponent<MeshRenderer>().material = mat;
        }

        //manual placement
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Debug.Log("FIRE");

            //get the next if there is one
            if(transform.parent.childCount > 1)
            {
                Transform next = transform.parent.GetChild(1);
                next.gameObject.SetActive(true);
            }
            

            //remove me
            transform.SetParent(EvidenceLocations);
            placed = true;

        }
        //proximity placement
        

        if (Vector3.Distance(transform.position, EvidenceTarget.position) < 4)
        {
            if(Quaternion.Angle(transform.rotation,EvidenceTarget.rotation) < 3)
            {
                //make sure I am facing forward to the position (not convinced)
                Vector3 heading = EvidenceTarget.position - transform.position;
                heading.Normalize();

                float dot = Vector3.Dot(heading, transform.forward );
                Debug.Log("angle " + dot.ToString());
                if (dot > 0)
                {

                    transition = true;
                    startQ = transform.rotation;
                    startV = transform.position;
                    tQ = 0;
                    tV = 0;

                    Debug.Log("TRANSITION EVIDENCE");

                }


            }

        }
    }
}
