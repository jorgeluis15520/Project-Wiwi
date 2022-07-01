using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionOptions : MonoBehaviour
{
    private int widthCurrent;

    public GameObject button1920;
    public GameObject button1600;
    public GameObject button1366;

    private void Start()
    {
        widthCurrent = Screen.currentResolution.width;

        switch (widthCurrent)
        {
            case 1920:

                button1920.SetActive(true);
                break;

            case 1600:

                button1600.SetActive(true);
                break;

            case 1366:

                button1366.SetActive(true);
                break;

            default:
                button1920.SetActive(true);
                break;
        }

    }

    public void RightArrow()
    {
        if (button1920.activeSelf)
        {
            button1920.SetActive(false);
            button1366.SetActive(true);
        }

        else if (button1600.activeSelf)
        {
            button1600.SetActive(false);
            button1920.SetActive(true);
        }

        else if (button1366.activeSelf)
        {
            button1366.SetActive(false);
            button1600.SetActive(true);
        }
    }

    public void LeftArrow()
    {
        if (button1920.activeSelf)
        {
            button1920.SetActive(false);
            button1600.SetActive(true);
        }

        else if (button1600.activeSelf)
        {
            button1600.SetActive(false);
            button1366.SetActive(true);
        }

        else if (button1366.activeSelf)
        {
            button1366.SetActive(false);
            button1920.SetActive(true);
        }
    }
}
