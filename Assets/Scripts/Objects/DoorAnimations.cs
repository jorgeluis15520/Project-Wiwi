using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimations : MonoBehaviour
{
    public Animator anim;
    public AudioClip doorOpen;
    public AudioSource audSource;
    public string parametersName;
    public static bool endLevel = false;
    public bool grid = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   
    public void DoorAudio()
    {
        audSource.PlayOneShot(doorOpen);
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && Inventary.haveItem)
        {
            anim.SetBool(parametersName, true);
            endLevel = true;
            if (grid)
            {
                DoorAudio();
            }
        }
    }
}
