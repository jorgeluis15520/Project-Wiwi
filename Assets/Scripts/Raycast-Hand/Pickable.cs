using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickable : Interactable
{
    public GameObject NameObjec;
    
    public BoxText HandDetect;
    
    
    
    public override void interact()
    {
        base.interact();
        if (HandDetect.Detect)
        {
            NameObjec.SetActive(true);
        }
        else
        {
            NameObjec.SetActive(false);

        }





    }
        
   }

