using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckCollects : MonoBehaviour
{
    public RawImage blockCollect1;
    public RawImage blockCollect2;
    public RawImage blockCollect3;

    public static bool haveCollect1 = false;
    public static bool haveCollect2 = false;
    public static bool haveCollect3 = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckCollect();
    }

    public void CheckCollect()
    {
        if (!haveCollect1)
        {
            blockCollect1.enabled = true;
        }
        else if (haveCollect1)
        {
            blockCollect1.enabled = false;
        }

        if (!haveCollect2)
        {
            blockCollect2.enabled = true;
        }
        else if (haveCollect2)
        {
            blockCollect2.enabled = false;
        }

        if (!haveCollect3)
        {
            blockCollect3.enabled = true;
        }
        else if (haveCollect3)
        {
            blockCollect3.enabled = false;
        }
    }
}
