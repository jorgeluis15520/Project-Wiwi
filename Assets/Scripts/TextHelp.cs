using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextHelp: MonoBehaviour
{
    public bool Iniddle;
    public float Timer;
    public float MaxTimer;
    public TMP_Text Text;
    public GameObject TextBox;
    public string[]Help;
    int I;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

   
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Zone1"))
        {
            Timer += Time.deltaTime;
            if (Timer >= MaxTimer)
            {
              
                
                Text.text = Help[I];
                TextBox.SetActive(true);
            }
        }
        if (other.gameObject.CompareTag("Zone2"))
        {
            Timer += Time.deltaTime;
            if (Timer >= MaxTimer)
            {
                TextBox.SetActive(true);
                Text.text = "Que hay arriba de esa mesa?,Deberia Subir";
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Zone1"))
        {
            Timer = 0;
            Text.text =  "";
            TextBox.SetActive(false);
        }
        if (other.gameObject.CompareTag("Zone2"))
        {
            Timer = 0;
            Text.text = "";
            TextBox.SetActive(false);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zone1"))
        {
            I = Random.Range(0, Help.Length);
        }
    }

}
