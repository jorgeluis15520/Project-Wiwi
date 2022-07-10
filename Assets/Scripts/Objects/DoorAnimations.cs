using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimations : MonoBehaviour
{
    public Animator anim;
    public AudioClip doorOpen;
    public AudioSource audSource;
    public string parametersName;
    public static bool endLevel = false;
    public bool grid = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   
    public void DoorAudio()
    {
        audSource.PlayOneShot(doorOpen);
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        if (other.CompareTag("Player") && Inventary.haveItem)
        {
            anim.SetBool(parametersName, true);
            endLevel = true;
            if (grid)
            {
                DoorAudio();
            }
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            anim.SetBool(parametersName, true);
            DoorAudio();
            Debug.Log("te amo");
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            anim.SetBool(parametersName, true);
            DoorAudio();
            Debug.Log("te amo");
        }
    }
}
