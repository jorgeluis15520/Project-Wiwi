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
    public float dis;
    [SerializeField] private float maxDistance;
    private bool followComputer = false;
    private bool patrolling = false;
    public float timerSearch;
    public float maxTimer;
    private bool once = false;
    public bool boxFall;
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
        dis = Vector3.Distance(transform.position, wayPoints[currentPoint].position);
        
        nav.destination = wayPoints[currentPoint].position;

        if (computerActive && !urnActive)
        {
            if (!followComputer)
            {
                currentPoint = 1;
                followComputer = true;
            }

            if (followComputer && !patrolling && dis<= maxDistance)
            {

                patrolling = true;
            }

            if (patrolling)
            {
                Search();
            }
        }

        if (urnActive && !exitRoom)
        {
            //anim.SetBool("Walk", true);
            currentPoint = 0;
        }

        if (dis <= 2f)
        {
            //anim.SetBool("Walk", false);
        }

        if (exitRoom)
        {
            nav.speed = 1.5f;
            //anim.SetBool("Run", true);
            if (!once)
            {
                currentPoint = 3;
                once = true;
            }

            if (dis <= maxDistance && currentPoint < wayPoints.Length - 1)
            {
                currentPoint++;
            }
        }
    }

    void Search()
    {
        timerSearch += Time.deltaTime;

        if (timerSearch <= maxTimer)
        {
            if (dis <= maxDistance)
            {
                if (currentPoint == 1)
                {
                    currentPoint++;
                }
                else
                {
                    currentPoint--;
                }
            }
        }
        else
        {
            timerSearch = 0;
            currentPoint = 0;
            patrolling = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            boxFall = true;
        }
    }
}
