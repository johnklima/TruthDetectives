using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateObject : MonoBehaviour
{

    public Quaternion localRotation;
    public Vector3 localPosition;
    public Quaternion rotation;
    public Vector3 position;
    
    public Quaternion correctrotation;
    public Vector3 correctposition;

    public bool firstRun = true;
    public bool write = false;  //change this to public to burn the scene
    private string path = Application.streamingAssetsPath;
    private string chapter = "";



    protected virtual void Update()
    {
                
        if(Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Z) )
        {
            //write the comparison data for "correctness"
            Debug.Log("WRITE CORRECT");
            writeCorrectJSON();
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.C))
        {
            //write the comparison data for "correctness"
            Debug.Log("READ CORRECT");
            readCorrectJSON();
        }

    }
    public void writeCorrectJSON()
    {
        correctrotation = transform.rotation;
        correctposition = transform.position;
        firstRun = false;
        string json = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(path + "/Data/" + chapter + "/CorrectObjJson" + name + ".txt", json);

        readCorrectJSON();
    }
    

    public void readCorrectJSON()
    {

        if (System.IO.File.Exists(path + "/Data/" + chapter + "/CorrectObjJson" + name + ".txt"))
        {
            //read the JSON level init
            string json = System.IO.File.ReadAllText(path + "/Data/" + chapter + "/CorrectObjJson" + name + ".txt");


            JsonUtility.FromJsonOverwrite(json, this);

           
        }
    }
    public void readJSON()
    {
        //first check if player saved state
        if (System.IO.File.Exists(path + "/Data/" + chapter + "/PlayerObjJson" + name + ".txt"))
        {

            //read player's last state
            string json = System.IO.File.ReadAllText( path + "/Data/" + chapter + "/PlayerObjJson" + name + ".txt");


            JsonUtility.FromJsonOverwrite(json, this);

            transform.rotation = rotation;
            transform.position = position;
            transform.localRotation = localRotation;
            transform.localPosition = localPosition;



        }       
        
    }

    public void writeJSON()
    {

        rotation = transform.rotation;
        position = transform.position;
        localRotation = transform.localRotation;
        localPosition = transform.localPosition;        

        string json = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(path + "/Data/" + chapter + "/PlayerObjJson" + name + ".txt", json);
        

    }
    

    private void OnApplicationQuit()
    {
        if(write)
            writeJSON();
    }
    private void Awake()
    {
        chapter = SceneManager.GetActiveScene().name;
        Debug.Log(chapter);
        
        readCorrectJSON();
        readJSON();
        write = false;
    }
}
