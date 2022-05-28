using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushandPullObjects : MonoBehaviour
{
    public GameObject handPoint; //punto desde donde se empujara

    public GameObject pickedObject = null; //referencia para verificar si se esta empujando un objeto
    

    void Update()
    {
        if (pickedObject != null)
        {
            if (Input.GetKeyUp(KeyCode.E)) //al soltar la tecla E el personaje dejara de empujar y jalar objetos
            {
                pickedObject.GetComponent<Rigidbody>().useGravity = true;
                pickedObject.GetComponent<Rigidbody>().isKinematic = false;

                pickedObject.gameObject.transform.SetParent(null);
                pickedObject = null;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            if (Input.GetKey(KeyCode.E) && pickedObject == null) //al mantener presionado la tecla E el personaje dejara de empujar y jalar objetos
            {
                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().isKinematic = true;

                other.transform.position = handPoint.transform.position;
                other.gameObject.transform.SetParent(handPoint.gameObject.transform);
                pickedObject = other.gameObject;
            }


        }
    }
}
