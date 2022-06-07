using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextHelp: MonoBehaviour
{
    public bool action1;
    public bool action2;
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
        if (action1)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                action2 = true;
                action1 = false;
            }
        }
    }

   
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Zone1"))
        {
            Timer += Time.deltaTime;

            if (Timer >= MaxTimer)
            {
                TextBox.SetActive(true);
                if (action1)
                {
                    Text.text = "Presione X para la ayuda";
                }
                if (action2)
                {
                    Text.text = Help[I];
                }
               
                   
                
                

               
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
            action2 = false;
            action1 = false;
            
        }
       
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zone1"))
        {
            I = Random.Range(0, Help.Length);
            action1 = true;
           
            
        }

        
    }

}
