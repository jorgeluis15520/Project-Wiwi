using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogText : MonoBehaviour
{
    public GameObject Text;
    public GameObject TextDialog;
    public bool Action1;
    public bool Action2;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.isPause)
        {
            Text.SetActive(false);
            TextDialog.SetActive(false);
        }
        if (Action1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Action2 = true;
                Action1 = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Text.SetActive(true);
    
            if (Action2)
            {
                Text.SetActive(false);
                TextDialog.SetActive(true);
            }

        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Text.SetActive(false);
            TextDialog.SetActive(false);
            Action1 = true;
            Action2 = false;
        }
    }
}
