using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimations : MonoBehaviour
{
    public Animator anim;
    public AudioClip door_Open;
    public AudioSource audSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   
    public void DoorAudio()
    {
        audSource.PlayOneShot(door_Open);
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && Inventary.haveItem)
        {
            anim.SetBool("doorOpen", true);
        }
    }
}
