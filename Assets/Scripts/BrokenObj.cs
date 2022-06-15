using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            //vase.GetComponent<MeshFilter>();
            //vase.mesh = vaseBroken.mesh;}
           
            audioSource.PlayOneShot(BrokenSound);
            Debug.Log("OLA");
            
            
            /*GameObject Piece1;
            Piece1= Instantiate(PieceBroken[0], transform.position, Quaternion.identity);
            GameObject Piece2;
            Piece2 = Instantiate(PieceBroken[1], transform.position, Quaternion.identity);
            GameObject Piece3;
            Piece3 = Instantiate(PieceBroken[2], transform.position, Quaternion.identity);
            GameObject Piece4;
            Piece4 = Instantiate(PieceBroken[3], transform.position, Quaternion.identity);
            colliderObj.isTrigger = true;*/
            PieceBroken[0].SetActive(true);
            PieceBroken[1].SetActive(true);
            PieceBroken[2].SetActive(true);
            PieceBroken[3].SetActive(true);
            key.SetActive(true);
            colliderObj.isTrigger = true;
            Urna1.SetActive(false);
        }
    }
    public void KeyCreate()
    {
        GameObject KeyDrop;
        KeyDrop = Instantiate(key, transform.position, Quaternion.identity);

    }
   
}
