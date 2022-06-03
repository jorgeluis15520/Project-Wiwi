using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogText : MonoBehaviour
{
    public GameObject Text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Text.SetActive(true);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Text.SetActive(false);
        }
    }
}
