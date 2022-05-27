using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    PlayerController player;

    public bool active;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            active = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //active = false;
        }
    }
}
