using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour
{
    public GameObject actionFrame;
    public AudioManager AudioM;
    public BoxCollider collider;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collider.enabled = !collider.enabled;
            actionFrame.GetComponent<Rigidbody>().useGravity = true;
            actionFrame.GetComponent<Rigidbody>().isKinematic = false;
            StartCoroutine("Sound");
           


        }
    }
    private IEnumerator Sound()
    {

        yield return new WaitForSeconds(0.4f);
        AudioM.SeleccionAudioObjects(4, 0.2f);
        yield return new WaitForSeconds(1.2f);
        AudioM.SeleccionAudioObjects(4, 0.1f);
        

    }


}
