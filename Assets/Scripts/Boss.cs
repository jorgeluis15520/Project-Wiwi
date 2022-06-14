using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public Transform[] wayPoints;
    public int currentPoint = 0;
    private NavMeshAgent nav;
    public bool computerActive;
    public bool urnActive;
    public bool exitRoom;
    private Animator anim;
    private AudioClip roar;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float dis = Vector3.Distance(transform.position, wayPoints[currentPoint].position);
        
        nav.destination = wayPoints[currentPoint].position;

        if (computerActive && !urnActive)
        {
            currentPoint = 1;
            anim.SetBool("Walk", true);
        }

        if (urnActive && !exitRoom)
        {
            anim.SetBool("Walk", true);
            currentPoint = 2;
        }

        if (dis <= 2f)
        {
            anim.SetBool("Walk", false);
        }

        if (exitRoom)
        {
            nav.speed = 7f;
            anim.SetBool("Run", true);
            currentPoint = 3;
        }
    }
}
