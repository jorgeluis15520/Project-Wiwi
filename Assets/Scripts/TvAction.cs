using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
using UnityEngine.Playables;


public class TvAction : MonoBehaviour
{
    public GameObject Video;
    public bool InArea;
    public PlayableDirector Cm;
    public AudioManager AudioM;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (InArea)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                Video.SetActive(true);
                Cm.Play();
                AudioM.Tv();
                
            }
        }
    }
   
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InArea = false;
        }
    }

}
