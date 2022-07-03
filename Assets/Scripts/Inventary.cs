using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventary : MonoBehaviour
{
    public InventaryCanvas inventaryCanvas;

    public static bool haveItem = false;
    public AudioManager audioManager;
    public GameObject Fire;
    private void Awake()
    {
        inventaryCanvas = GameObject.FindObjectOfType<InventaryCanvas>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    public void Start()
    {
        if(audioManager == null)
        {
            return;
        }

        if (audioManager == null)
        {
            return;
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Item"))
        {
            for (int i = 0; i < inventaryCanvas.objects.Count; i++)
            {
                if (inventaryCanvas.objects[i].GetComponent<Image>().enabled == false)
                {
                    haveItem = true;
                    inventaryCanvas.objects[i].GetComponent<Image>().enabled = true;
                    inventaryCanvas.objects[i].GetComponent<Image>().sprite = coll.gameObject.GetComponent<Image>().sprite;
                    audioManager.GrabObject();
                    inventaryCanvas.starfadingItem();
                    Destroy(coll.gameObject);
                    break;

                }
            }
        }

        if (coll.gameObject.CompareTag("Collect1"))
        {
            CheckCollects.haveCollect1 = true;
            Destroy(coll.gameObject);
        }

        if (coll.gameObject.CompareTag("Collect2"))
        {
            CheckCollects.haveCollect2 = true;
            Destroy(coll.gameObject);
        }

        if (coll.gameObject.CompareTag("Collect3"))
        {
            CheckCollects.haveCollect3 = true;
            Destroy(coll.gameObject);
        }

        if (coll.gameObject.CompareTag("Door") && haveItem)
        {
            inventaryCanvas.objects[0].GetComponent<Image>().sprite = null;
            inventaryCanvas.objects[0].GetComponent<Image>().enabled = false;
            haveItem = false;
        }

    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Grill") && haveItem)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                inventaryCanvas.objects[0].GetComponent<Image>().sprite = null;
                inventaryCanvas.objects[0].GetComponent<Image>().enabled = false;
                haveItem = false;
                Fire.SetActive(true);
            }

        }
    }
}

