using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushSound : MonoBehaviour
{
    public Animator Sound;
    public AudioClip Clip;
    public AudioSource Source;
    void Start()
    {
        Source = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        
    }
    public void PushOn()
    {
        Source.PlayOneShot(Clip);
    }
}
