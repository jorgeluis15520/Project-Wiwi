using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFall : MonoBehaviour
{
    private Rigidbody rgbd;
    public float force;
    public Boss boss;
    private bool once = false;
    private AudioSource audioSource;
    public AudioClip fall;

    private void Start()
    {
        rgbd = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (boss.boxFall && !once)
        {
            Move();
            boss.boxFall = false;
            once = true;
        }
    }



    public void Move()
    {
        rgbd.AddForce(new Vector3(0, 0, -force), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            audioSource.PlayOneShot(fall);
        }
    }
}
