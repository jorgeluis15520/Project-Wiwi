using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CloseVideo : MonoBehaviour
{
    public GameObject thisObject;
    public VideoPlayer video;
    public static bool videoPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (videoPlaying)
        {
            SkipVideo();
        }
    }

    public void PlayVideo()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += CheckOver;
        videoPlaying = true;
        
    }
    // Update is called once per frame
    void CheckOver(VideoPlayer vp)
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        videoPlaying = false;
    }

    public void SkipVideo()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            videoPlaying = false;
            Time.timeScale = 1f;
            gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
