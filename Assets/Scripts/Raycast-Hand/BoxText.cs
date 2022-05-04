using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxText : MonoBehaviour
{
    public GameObject Hand;
    public float HandDistance;
    public bool Detect;
    public TextMesh objText;
    public 
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandRaycast();
        
    }
    void HandRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(Hand.transform.position, Hand.transform.forward, out hit, HandDistance, LayerMask.GetMask("Interactable")))
        {
            
                hit.transform.GetComponent<Interactable>().interact();
            

        }
       
      
           
        

       


    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Hand.transform.position, Hand.transform.forward * HandDistance);
    }
}
