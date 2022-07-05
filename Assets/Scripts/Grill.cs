using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grill : MonoBehaviour
{
    public GameObject Fire;
    public static bool isActive;

    // Update is called once per frame
    void Update()
    {
        GrillActive();
    }

    public void GrillActive()
    {
        if (isActive)
        {
            Fire.SetActive(true);
        }
    } 
}
