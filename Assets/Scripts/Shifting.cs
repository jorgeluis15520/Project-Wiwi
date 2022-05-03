using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shifting : MonoBehaviour
{
    Vector3 ScaleNormal;
    Vector3 ScaleShifting;
    bool Shift;

    void Start()
    {
        ScaleNormal = transform.localScale;
        ScaleShifting = ScaleNormal;
        ScaleShifting.y = ScaleNormal.y/2;

    }

    // Update is called once per frame
    void Update()
    {
        ducking();
        if (Input.GetKey(KeyCode.T))
        {
            Shift = true;
        }
        else
        {
            Shift = false;
        }
    }
    void ducking()
    {
        

            if (Shift)
            {
            transform.localScale = Vector3.Lerp(transform.localScale, ScaleShifting, Time.deltaTime * 8f);
            }
            else
            {
            transform.localScale = ScaleNormal;
            }


    }
}
