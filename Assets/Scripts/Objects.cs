using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class Objects : MonoBehaviour
{
    public List<GameObject> waterParts = new List<GameObject>();
    public GameObject efectSound;
    public bool active;

    public AudioClip clip;
    private bool inArea = false;
    private bool end = false;
    public float timer;
    private int currentPart = 0;

  
    public PlayableDirector Cm;

    private void Update()
    {
        if (inArea && Input.GetKeyDown(KeyCode.E) && !active)
        {
            active = true;
            
            StartCoroutine(WaterActive());
            Cm.Play();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            inArea = true;
        }
    }

    private IEnumerator WaterActive()
    {
        if (end)
        {
            yield break;
        }

        for (int i = 0; i < waterParts.Count; i++)
        {
            currentPart = i;
            waterParts[currentPart].SetActive(true);
            yield return new WaitForSeconds(timer);
        }

        if (currentPart >= waterParts.Count)
        {
            end = true;
        }
    }
}
