using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    private bool inArea;
    private Objects objects;
    private AudioSource audioSource;
    public AudioClip clip;
    public Boss boss;
    public GameObject computerEmission;
    private MeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
        objects = GetComponent<Objects>();
        audioSource = GetComponent<AudioSource>();
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inArea && Input.GetKeyDown(KeyCode.E) && !objects.active)
        {
            objects.active = true;
            boss.computerActive = true;
            computerEmission.SetActive(true);
            mesh.enabled = !mesh.enabled;
            audioSource.PlayOneShot(clip);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = false;
        }
    }
}
