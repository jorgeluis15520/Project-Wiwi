
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

    private AudioSource audioSource;
    public AudioClip detectSound;
    public AudioClip stepSound;
    private bool detect = false;
    private float timer;
    private float timer2;
    private Animator anim;
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

        targetPlayer = fov.targetPlayer;
        targetObject = fov.targetObject;
        lastPosition = fov.lastPosition;

        if (targetPlayer == null && targetObject == null && lastPosition==null)
        {
            Move();
            detect = false;
        }
        else if(targetPlayer == null && targetObject != null && lastPosition ==null)
        {
            FollowObject();
            detect = false;
        }
        else
        {
            FollowPLayer();

            if (!detect)
            {
                audioSource.PlayOneShot(detectSound);
                detect = true;
            }

            timer += Time.deltaTime;
        }

        if (detect && timer>=5f)
        {
            audioSource.PlayOneShot(detectSound);
            timer = 0;
        }
    }

    void Move()
    {
        anim.SetBool("Run", false);
        nav.speed = walkSpeed;
        nav.destination = wayPoints[currentPoint].position;

        float dist = Vector3.Distance(transform.position, wayPoints[currentPoint].position);

        if (dist <= 0.1f)
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

        if (lastPosition == null)
        {
            nav.destination = targetPlayer.position;
        }
        else
        {
            nav.destination = lastPosition.position;

            float dis = Vector3.Distance(transform.position, lastPosition.position);

            if (dis <= 0.1f)
            {
                lastPosition = null;
                fov.targetPlayer = null;
            }
        }

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

        if (dis <= 0.1f)
        {
            timer2 += Time.deltaTime; 
        }

        if (timer>=3f)
        {
            targetObject.GetComponent<Objects>().active = false;
            fov.targetObject = null;
            timer = 0;
        }
    }
    public void Step()
    {
        audioSource.PlayOneShot(stepSound);
    }
}
