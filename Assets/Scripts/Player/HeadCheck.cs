using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCheck : MonoBehaviour
{
    public int CountColission = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        CountColission++;
    }

    private void OnTriggerExit(Collider other)
    {
        CountColission--;
    }
}
