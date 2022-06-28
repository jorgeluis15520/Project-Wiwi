using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BrokenObj : MonoBehaviour
{
    //public MeshFilter vase;
    //public MeshFilter vaseBroken;
    public GameObject Urna1;
    public GameObject key;
    public GameObject[] PieceBroken;
    public AudioSource audioSource;
    public AudioClip BrokenSound;
    private BoxCollider colliderObj;
    private Rigidbody RBD;
    public PlayableDirector Cm;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        colliderObj = GetComponent<BoxCollider>();
        RBD = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Floor"))
        {
            audioSource.PlayOneShot(BrokenSound);
            PieceBroken[0].SetActive(true);
            PieceBroken[1].SetActive(true);
            PieceBroken[2].SetActive(true);
            PieceBroken[3].SetActive(true);
            key.SetActive(true);
            colliderObj.isTrigger = true;
            Urna1.SetActive(false);
            Cm.Play();
        }
    }
    public void KeyCreate()
    {
        GameObject KeyDrop;
        KeyDrop = Instantiate(key, transform.position, Quaternion.identity);

    }
   
}
