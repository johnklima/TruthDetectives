using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIState : MonoBehaviour
{

    //basically same as GameStateObject.cs, so some redundancy but 
    //GUI will certainly hold much more data



    public bool firstRun = true;
    private string path = Application.streamingAssetsPath;
    protected virtual void Update()
    {



        //for designers only
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown( KeyCode.X))
        {
            writeFirstJSON();
        }
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Y))
        {
            readFirstJSON();
        }
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Z))
        {
            //write the comparison data for "correctness"
            writeCorrectJSON();
        }

    }
    public void writeCorrectJSON()
    {
      
        firstRun = false;
        string json = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(path + "/Data/CorrectGUIJson" + name + ".txt", json);

    }
    public void readFirstJSON()
    {
        if (System.IO.File.Exists(path + "/Data/FirstPlayGUIJson" + name + ".txt"))
        {
            //read the JSON level init
            string json = System.IO.File.ReadAllText(path + "/Data/FirstPlayGUIJson" + name + ".txt");


            JsonUtility.FromJsonOverwrite(json, this);

            firstRun = false;

            //next load we should now be local
            writeJSON();
        }
    }
    public void readJSON()
    {
        //first check if player saved state
        if (System.IO.File.Exists(path + "/Data/PlayerGUIJson" + name + ".txt"))
        {

            //read player's last state
            string json = System.IO.File.ReadAllText(path + "/Data/PlayerGUIJson" + name + ".txt");


            JsonUtility.FromJsonOverwrite(json, this);      



        }

        //however if it is firstRun for the designer
        if (!System.IO.File.Exists(path + "/Data/FirstPlayGUIJson" + name + ".txt"))
        {
            //temporary helper for level design at runtime
            writeFirstJSON();
        }

        if (System.IO.File.Exists(path + "/Data/FirstPlayGUIJson" + name + ".txt") && firstRun)
        {
            //read the JSON level init
            string json = System.IO.File.ReadAllText(path + "/Data/FirstPlayGUIJson" + name + ".txt");


            JsonUtility.FromJsonOverwrite(json, this);

     
            firstRun = false;
            //next load we should now be local
            writeJSON();
        }

    }

    public void writeJSON()
    {
             

        string json = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(path + "/Data/PlayerGUIJson" + name + ".txt", json);


    }
    public void writeFirstJSON()
    {
  
        firstRun = false;
        string json = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(path + "/Data/FirstPlayGUIJson" + name + ".txt", json);

    }

    private void OnApplicationQuit()
    {
        
        writeJSON();
    }
    private void Awake()
    {
       
        readJSON();
    }
}
