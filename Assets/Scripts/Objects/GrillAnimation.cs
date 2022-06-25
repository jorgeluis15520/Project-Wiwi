using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrillAnimation : MonoBehaviour
{
    public AudioManager audioManager;


    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && Inventary.haveItem)
        {
            audioManager.Lighter();
        }
    }
}
