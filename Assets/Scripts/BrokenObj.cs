using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenObj : MonoBehaviour
{
    public MeshFilter vase;
    public MeshFilter vaseBroken;
    public bool Broken;
    public GameObject key;
    public float timer;
    public float MaxTimer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      /*  if (Broken)
        {
            timer += Time.deltaTime;
            if (timer > MaxTimer)
            {
                KeyCreate();
                Broken
            }
            
        }*/
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Floor"))
        {
            vase.GetComponent<MeshFilter>();
            vase.mesh = vaseBroken.mesh;
            Broken = true;
            KeyCreate();
        }
    }
    public void KeyCreate()
    {
        GameObject KeyDrop;
        KeyDrop = Instantiate(key, transform.position, Quaternion.identity);

    }
}
