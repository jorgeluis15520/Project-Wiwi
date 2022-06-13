using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform[] wayPoints;
    public int currentPoint;
    public float walkSpeed;
    public float runSpeed;
    public float speedRotation;
    private float speed;
    
    public Transform targetPlayer;
    public Transform targetObject;
    FieldOfView fov;

    private AudioSource audioSource;
    public AudioClip detectSound;
    public AudioClip stepSound;
    private bool detect = false;
    private float timer;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        transform.LookAt(new Vector3(wayPoints[currentPoint].position.x, transform.position.y, wayPoints[currentPoint].position.z));
    }

    // Update is called once per frame
    void Update()
    {
        fov = GetComponent<FieldOfView>();

        targetPlayer = fov.targetPlayer;
        targetObject = fov.targetObject;

        if (targetPlayer == null && targetObject == null)
        {
            Move();
            detect = false;
        }
        else if(targetPlayer == null && targetObject != null)
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
        speed = walkSpeed;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(wayPoints[currentPoint].position.x, transform.position.y, wayPoints[currentPoint].position.z), speed * Time.deltaTime);

        float dist = Vector3.Distance(transform.position, wayPoints[currentPoint].position);

        if (dist <= 1f)
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
        anim.SetBool("Run", true);
        speed = runSpeed;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPlayer.position.x, transform.position.y, targetPlayer.position.z), speed * Time.deltaTime);

        var dir = targetPlayer.position - transform.position;
        var rotation = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speedRotation * Time.deltaTime);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }

    void FollowObject()
    {
        anim.SetBool("Run", true);
        speed = runSpeed;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetObject.position.x, transform.position.y, targetObject.position.z), speed * Time.deltaTime);

        var dir = targetObject.position - transform.position;
        var rotation = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speedRotation * Time.deltaTime);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);

        float dis = Vector3.Distance(transform.position, targetObject.position);

        if (dis <= 2f)
        {
            targetObject.GetComponent<Objects>().active = false;
        }
    }
    public void Step()
    {
        audioSource.PlayOneShot(stepSound);
    }
}
