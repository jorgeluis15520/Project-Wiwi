using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventaryCanvas : MonoBehaviour
{
    public  List<GameObject> objects = new List<GameObject>();
    public GameObject inv;
    public Image invImage;
    public bool activeInv;
    // Start is called before the first frame update
    void Start()
    {
        var item = GameObject.Find("Items");
        objects.Add(item);
    }

    // Update is called once per frame
    void Update()
    {
        activeInv = true;
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
    IEnumerator FadeInItem()
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
    public void starfadingItem()
    {
        StartCoroutine("FadeInItem");
    }
}
