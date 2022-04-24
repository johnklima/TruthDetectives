using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalData : MonoBehaviour
{
    public Transform selected;
    public DialogTreeController dialogTreeController;

    public ImageItem[] imageItems;
    public Locations locations;

    public enum STATES
    {
        INTRO,
        CHAP1,
        CHAP2,
        CHAP3,
        CHAP3_2,
        CHAP4,
        CHAP5,
        ENDSTATE
    }
    public STATES state = STATES.INTRO;
    private void Start()
    {
        //lets set the screen rez
        Screen.SetResolution(1366, 768, true);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();


    }

    public void loadScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void nextState()
    {
        //next chapter (to be, or not to be)
        state += 1;
        if (state == STATES.CHAP2)
        {
            loadScene("chapter2");
        }
    }

    public void videoComplete()
    {
        Debug.Log("VIDEO COMPLETE");
        dialogTreeController.dialog.pause = false;
    }
    public void transitionCamera(int target)
    {

        Debug.Log("transition camera start");
        locations.onGo();
    }
}
