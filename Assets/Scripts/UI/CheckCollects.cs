using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckCollects : MonoBehaviour
{
    public GameObject blockCollect1;
    public GameObject blockCollect2;
    public GameObject blockCollect3;

    public GameObject unblockCollect1;
    public GameObject unblockCollect2;
    public GameObject unblockCollect3;

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
            blockCollect1.SetActive(true);
        }
        if (haveCollect1)
        {
            blockCollect1.SetActive(false);
            unblockCollect1.SetActive(true);
        }

        if (!haveCollect2)
        {
            blockCollect2.SetActive(true);
        }
        if (haveCollect2)
        {
            blockCollect2.SetActive(false);
            unblockCollect2.SetActive(true);
        }

        if (!haveCollect3)
        {
            blockCollect3.SetActive(true);
        }
        if (haveCollect3)
        {
            blockCollect3.SetActive(false);
            unblockCollect3.SetActive(true);
        }
    }
}
