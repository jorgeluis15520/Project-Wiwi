using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class Valve : MonoBehaviour
{
    public List<GameObject> waterParts = new List<GameObject>();
    public GameObject efectSound;
    public bool active;

    public AudioSource audioSource;
    public AudioClip clip;
    private bool inArea = false;
    private bool end = false;
    public float timer;
    private int currentPart = 0;

    private Objects objects;
    public PlayableDirector Cm;

    private void Start()
    {
        objects = GetComponent<Objects>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (inArea && Input.GetKeyDown(KeyCode.E) && !active)
        {
            active = true;
            objects.active = true;
            audioSource.PlayOneShot(clip);
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

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player") && inArea)
        {
            inArea = false;
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
