
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] wayPoints;
    public int currentPoint;
    public float walkSpeed;
    public float runSpeed;
    public float speedRotation;
    private float speed;
    private NavMeshAgent nav;
    public Transform targetPlayer;
    public Transform targetObject;
    public Transform lastPosition;
    FieldOfView fov;
    private bool isFollowPlayer = false;
    private bool isFollowObjetc = false;
    private bool isFollowPosition = false;

    private AudioSource audioSource;
    public AudioClip detectSound;
    public AudioClip stepSound;
    private bool detect = false;
    private float timer;
    public float timer2;
    public float timer3;
    private Animator anim;
    public Vector3 pos;
    private bool once = false;
    public float dis;
    public float dis2;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        transform.LookAt(new Vector3(wayPoints[currentPoint].position.x, transform.position.y, wayPoints[currentPoint].position.z));
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        fov = GetComponent<FieldOfView>();

        if (fov.targetPlayer != null)
        {
            targetPlayer = fov.targetPlayer;
            once = false;
        }
        else
        {
            targetPlayer = null;
        }

        targetObject = fov.targetObject;

        if (targetPlayer != null && !once)
        {
            lastPosition = targetPlayer;
            pos = lastPosition.position;
            once = true;
        }

        if (targetPlayer == null && targetObject == null && lastPosition == null)
        {
            Move();
            detect = false;
        }
        else if (targetPlayer == null && targetObject != null && lastPosition == null)
        {
            FollowObject();
            detect = false;
        }
        else if (targetPlayer !=null && targetObject == null && lastPosition == null)
        {
            FollowPLayer();

            if (!detect)
            {
                audioSource.PlayOneShot(detectSound);
                detect = true;
            }

            if (targetObject != null)
            {
                targetObject.GetComponent<Objects>().active = false;
                targetObject = null;
            }

            timer += Time.deltaTime;
        }
        else
        {
            FollowLastPosition();

            if (!detect)
            {
                audioSource.PlayOneShot(detectSound);
                detect = true;
            }

            timer += Time.deltaTime;
        }

        if (detect && timer >= 5f)
        {
            audioSource.PlayOneShot(detectSound);
            timer = 0;
        }

        if (Manager.isPause)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }

    void Move()
    {
        anim.SetBool("Run", false);
        nav.speed = walkSpeed;
        nav.destination = wayPoints[currentPoint].position;

        float dist = Vector3.Distance(transform.position, wayPoints[currentPoint].position);

        if (dist <= 0.15f)
        {
            currentPoint++;

            if (currentPoint >= wayPoints.Length)
            {
                currentPoint = 0;
            }
        }

        var dir = wayPoints[currentPoint].position - transform.position;

        var rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speedRotation * Time.deltaTime);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);

    }

    void FollowPLayer()
    {
        isFollowPlayer = true;
        anim.SetBool("Run", true);
        nav.speed = runSpeed;
        nav.destination = targetPlayer.position;
        lastPosition = targetPlayer;

        var dir = targetPlayer.position - transform.position;
        var rotation = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speedRotation * Time.deltaTime);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);

        if (isFollowObjetc)
        {
            targetObject.GetComponent<Objects>().active = false;
        }
    }

    void FollowObject()
    {
        anim.SetBool("Run", true);
        nav.speed = runSpeed;
        nav.destination = targetObject.position;
        isFollowObjetc = true;

        var dir = targetObject.position - transform.position;
        var rotation = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speedRotation * Time.deltaTime);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);

        float dis = Vector3.Distance(transform.position, targetObject.position);
        dis2 = dis;

        if (dis <= 1.75f)
        {
            timer2 += Time.deltaTime; 
        }

        if (timer2 >=3f)
        {
            targetObject.GetComponent<Objects>().active = false;
            fov.targetObject = null;
            timer2 = 0;
        }
    }

    void FollowLastPosition()
    {
        isFollowPosition = true;
        anim.SetBool("Run", true);
        nav.speed = runSpeed;
        nav.destination = pos;

        var dir = lastPosition.position - transform.position;
        var rotation = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speedRotation * Time.deltaTime);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);

        float dist = Vector3.Distance(transform.position, pos);

        dis = dist;

        if (dist <= 1.5f)
        {
            timer3 += Time.deltaTime;
        }

        if (timer3 >= 3f)
        {
            lastPosition = null;
            targetPlayer = null;
            pos = Vector3.zero;
            timer3 = 0;
        }
    }
    public void Step()
    {
        audioSource.PlayOneShot(stepSound);
    }
}
