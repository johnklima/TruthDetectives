using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PlayVideo : MonoBehaviour
{

    private VideoPlayer video;
    public bool play = true;
    
    // Start is called before the first frame update
    void Start()
    {

        video = transform.GetComponent<VideoPlayer>();

        scaleMe(1);

        //if a scene starts with a video, play should be true
        if(play)
            video.Play();

        video.loopPointReached += CheckOver;

    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {

        Debug.Log("Video Is Over" );
        
        GlobalData glo = Camera.main.GetComponent<GlobalData>();

        //LOAD NEXT AUTOMAGIC??
        glo.videoComplete();
        

        
  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            play = !play;
            
            if(play)
            {
                
                video.Play();   
               
            }
            else
            {
                video.Pause();
                
            }
        }

    
            
    }

    public void playVideo()
    {
       
        video = transform.GetComponent<VideoPlayer>();
        video.Play();
    }

    public void pauseVideo()
    {
        video = transform.GetComponent<VideoPlayer>();
        video.Pause();

    }
    public void stopVideo()
    {
        video = transform.GetComponent<VideoPlayer>();
        video.Stop();
    }
    public void scaleMe(int gamestate)
    {
        if (gamestate == 0)
        {
            Debug.Log("HELLO SCALE 0");
            transform.localScale = new Vector3(-5.5f, -3.15f, 0.001f);
        }
        else
        {
            Debug.Log("HELLO SCALE 1");
            transform.localScale = new Vector3(-4.25f, -2.52f, 0.001f);

        }
    }
}
