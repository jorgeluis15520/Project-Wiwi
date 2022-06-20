using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour
{
    public GameObject actionFrame;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            actionFrame.GetComponent<Rigidbody>().useGravity = true;
            actionFrame.GetComponent<Rigidbody>().isKinematic = false;
            Debug.Log("ga");
        }
    }

}
