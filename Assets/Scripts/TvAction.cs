using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvAction : MonoBehaviour
{
    public GameObject Video;
    public bool InArea;
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
}
