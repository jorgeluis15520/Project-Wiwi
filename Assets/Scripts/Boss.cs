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
    private bool once3 = false;
    public PlayerController player;
    public BrokenObj obj;
    private Quaternion rotation;
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
        urnActive = obj.soul;

        if (fov.targetPlayer != null)
        {
            if (!once2)
            {
                audioSource.PlayOneShot(roar);
                once2 = true;
            }
            nav.destination = fov.targetPlayer.position;
            anim.SetBool("Run", true);
            anim.SetBool("Walk", false);
        }
        else
        {
            dis = Vector3.Distance(transform.position, wayPoints[currentPoint].position);
            nav.destination = wayPoints[currentPoint].position;

            if (computerActive && !urnActive)
            {   
                if (!followComputer)
                {
                    currentPoint = 1;
                    followComputer = true;
                    audioSource.PlayOneShot(roar);
                    anim.SetBool("Run", true);
                }

                if (followComputer && !patrolling && dis <= maxDistance && currentPoint == 1)
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
                if (!once3)
                {
                    audioSource.PlayOneShot(roar);
                    once3 = true;
                }
                currentPoint = 0;
                fov.viewRadius = 5.25f;
                anim.SetBool("Run", true);
                anim.SetBool("Walk", false);
            }

            if (exitRoom)
            {
                nav.speed = 1.5f;
                anim.SetBool("Run", true);
                anim.SetBool("Walk", false);
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

            if (dis <= maxDistance)
            {
                if (urnActive && fov.targetPlayer == null && !exitRoom)
                {
                    audioSource.PlayOneShot(roar);
                    exitRoom = true;
                }

                if (currentPoint == 4)
                {
                    fov.viewRadius = 0f;
                    audioSource.Stop();
                }
            }
           
        }

        //Rotate();

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
           
            dis = Vector3.Distance(transform.position, wayPoints[currentPoint].position);

            if (dis <= maxDistance)
            {
                if (currentPoint == 1)
                {
                    anim.SetBool("Run", false);
                    anim.SetBool("Walk", true);
                    currentPoint++;
                    dis = Vector3.Distance(transform.position, wayPoints[currentPoint].position);
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
                nav.speed = 2.5f;
                fov.viewRadius = 5.25f;
            }
        }
    }

    void Rotate()
    {
        var dir = nav.destination - transform.position;

        if (dir != new Vector3(0,0,0))
        {
            rotation = Quaternion.LookRotation(dir);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speedRotation * Time.deltaTime);
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
