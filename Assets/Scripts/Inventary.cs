using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventary : MonoBehaviour
{

    public PlayerController playerController;
    public List<GameObject> objects = new List<GameObject>();
    public GameObject inv;
    public Image invImage;
    public bool activeInv;

    //coleccionables
    public RawImage blockCollect1;
    public RawImage blockCollect2;
    public RawImage blockCollect3;
    public static bool haveCollect1 = false;
    public static bool haveCollect2 = false;
    public static bool haveCollect3 = false;


    // Start is called before the first frame update
    void Start()
    {
        Color c = invImage.color;
        c.a = 0f;
        invImage.color = c;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            activeInv = !activeInv;
        }
        if (Input.GetKeyDown(KeyCode.Return) && activeInv)
        {
            startFading();
        }
        if (Input.GetKeyDown(KeyCode.Return) && !activeInv)
        {
            startFadeOut();
        }

        CheckCollects();
    }

    public void CheckCollects()
    {
        if (!haveCollect1)
        {
            blockCollect1.enabled = true;
        }
        else if(haveCollect1)
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

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Item"))
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].GetComponent<Image>().enabled == false)
                {
                    playerController.haveKey = true;
                    objects[i].GetComponent<Image>().enabled = true;
                    objects[i].GetComponent<Image>().sprite = coll.GetComponent<Image>().sprite;

                    if (activeInv)
                    {
                        starfadingkey();
                    }
                    if (!activeInv)
                    {
                        Color c = objects[i].GetComponent<Image>().color;
                        c.a = 0f;
                        objects[i].GetComponent<Image>().color = c;
                    }

                    Destroy(coll.gameObject);
                    break;

                }
            }
        }

        if (coll.CompareTag("Collect1"))
        {
            haveCollect1 = true;
            Destroy(coll.gameObject);
        }

        if (coll.CompareTag("Collect2"))
        {
            haveCollect2 = true;
            Destroy(coll.gameObject);
        }

        if (coll.CompareTag("Collect3"))
        {
            haveCollect3 = true;
            Destroy(coll.gameObject);
        }

        if (coll.CompareTag("Door") && playerController.haveKey)
        {
            objects[0].GetComponent<Image>().sprite = null;
            objects[0].GetComponent<Image>().enabled = false;
        }
    }

    IEnumerator FadeIn()
    {
        for (float f = 0.05f; f <= 1; f += 0.05f)
        {
            if (objects[0].GetComponent<Image>().enabled)
            {
                Color d = objects[0].GetComponent<Image>().color;
                d.a = f;
                objects[0].GetComponent<Image>().color = d;

            }
            Color c = invImage.color;
            c.a = f;
            invImage.color = c;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator FadeOut()
    {
        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            if (objects[0].GetComponent<Image>().enabled)
            {
                Color d = objects[0].GetComponent<Image>().color;
                d.a = f;
                objects[0].GetComponent<Image>().color = d;

            }
            Color c = invImage.color;
            c.a = f;
            invImage.color = c;
            yield return new WaitForSeconds(0.03f);
        }
    }
    IEnumerator FadeInkey()
    {
        for (float f = 0.05f; f <= 1; f += 0.05f)
        {
            Color d = objects[0].GetComponent<Image>().color;
            d.a = f;
            objects[0].GetComponent<Image>().color = d;
            yield return new WaitForSeconds(0.05f);
        }


    }
    public void startFading()
    {
        StartCoroutine("FadeIn");
    }
    public void startFadeOut()
    {
        StartCoroutine("FadeOut");
    }
    public void starfadingkey()
    {
        StartCoroutine("FadeInkey");
    }
}

