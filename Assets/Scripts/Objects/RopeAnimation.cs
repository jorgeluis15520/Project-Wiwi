using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RopeAnimation : MonoBehaviour
{
    
    public GameObject Rope;
    public GameObject RopeB1;
    public GameObject RopeB2;
    public PlayableDirector cm;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Grill.isActive)
        {

            
                StartCoroutine("Activate");
                cm.Play();
            
        }
    }
  private IEnumerator Activate()
    {
        yield return new WaitForSeconds(3f);
        RopeB1.SetActive(true);
        RopeB2.SetActive(true);
        Rope.SetActive(false);
    }
    
    

}
