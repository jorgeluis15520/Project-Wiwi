using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    public PlayerController playerController;
    public List<GameObject> objects = new List<GameObject>();
    public GameObject inv;
    public bool activeInv;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activeInv)
        {
            inv.SetActive(true);
        }

        else
        {
            inv.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            activeInv = !activeInv;
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Item"))
        {
            for(int i = 0; i < objects.Count; i++)
            {
                if(objects[i].GetComponent<Image>().enabled == false)
                {
                    playerController.haveKey = true;
                    objects[i].GetComponent<Image>().enabled = true;
                    objects[i].GetComponent<Image>().sprite = coll.GetComponent<Image>().sprite;
                    Destroy(coll.gameObject);
                    break;
                    
                }
            }
        }
    }

    
}
