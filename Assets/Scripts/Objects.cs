using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    PlayerController player;

    public bool active;

    public AudioClip clip;
    public AudioSource audioSource;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            active = true;

            audioSource.PlayOneShot(clip);
        }
    }
}
