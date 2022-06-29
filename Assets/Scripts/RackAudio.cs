using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RackAudio : MonoBehaviour
{
    public AudioManager audioManager;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioManager.Rack();
            Destroy(this);
        }
    }
}
