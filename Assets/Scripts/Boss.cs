using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public Transform[] wayPoints;
    public int currentPoint = 0;
    private NavMeshAgent nav;
    public float speedRotation;
    public bool computerActive;
    public bool urnActive;
    public bool exitRoom;
    private Animator anim;
    public AudioClip idle;
    public AudioClip roar;
    private AudioSource audioSource;
    public float dis;
    [SerializeField] private float maxDistance;
    private bool followComputer = false;
    private bool patrolling = false;
    public float timerSearch;
    public float maxTimer;
    private bool once = false;
    public bool boxFall;
    private FieldOfView fov;
    private bool once2 = false;
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        fov = GetComponent<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        dis = Vector3.Distance(transform.position, wayPoints[currentPoint].position);
        

        if (fov.targetPlayer != null)
        {
            if (!once2)
            {
                audioSource.PlayOneShot(roar);
                once2 = true;
            }
            nav.destination = fov.targetPlayer.position;
        }
        else
        {
            nav.destination = wayPoints[currentPoint].position;

            if (computerActive && !urnActive)
            {   
                if (!followComputer)
                {
                    currentPoint = 1;
                    followComputer = true;
                    audioSource.PlayOneShot(roar);
                }

                if (followComputer && !patrolling && dis <= maxDistance)
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

            if (once2)
            {
                once2 = false;
            }
        }

        Rotate();

        if (Manager.isPause)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
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
            audioSource.PlayOneShot(roar);
            if (!urnActive)
            {
                player.Death();
            }
        }
    }

    void Rotate()
    {
        var dir = nav.destination - transform.position;

        if (dir != new Vector3(0,0,0))
        {
            var rotation = Quaternion.LookRotation(dir);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speedRotation * Time.deltaTime);
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            if (exitRoom)
            {
                boxFall = true;
            }
        }
    }
}
