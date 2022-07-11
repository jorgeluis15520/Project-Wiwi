using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;

    [Header("Movement")]
    public float speed;
    public float jumpForce;
    public bool detectFloor = false;
    public float raycastDetect;
    public bool canJump;
    public bool canRun;
    public GameObject groundCheck;
    public LayerMask mask;
    private float hor;
    private float ver;

    [Header("Pushing")]
    public GameObject hand;
   // public GameObject HandCollider;
    public float handRay;
    private GameObject pickedObject = null;
    public GameObject handPush;
    public GameObject handPush2;
    public GameObject handPush3;
    public bool isPushing;
    //public float forcePush;
    



    [Header("Camera")]
    public Camera cam;
    private Vector3 camFwd;
    public float rotationSpeed;


    [Header("Crouch")]
    public float headRay;
    public float headCheck;
    public bool isCrouch;
    public GameObject head;
    private CapsuleCollider cap;
    private float startHeigh;
    private float starPosY;
    public float heighCollider;
    public float positionY;

    [Header("Animation")]
    public Animator anim;
    public AudioManager audioManager;
    public float speedInitial;
    public float speedCrouch;
    public float speedRun;
    public float speedPushing;

    //[Header("Inventary")]
    //public bool haveKey;
    [Header("DetectWalls")]
    public bool wallCheck;

    [Header("Climb")]
    public Transform body;
    public LayerMask layerMask;
    public LayerMask layerMask2;
    public float checkDistance;
    public bool climbWallCheck;
    public float climbSpeed;
    public float climbRopeSpeed;
    public bool isClimbing = false;
    private RaycastHit HIT;

    [Header("Up Ledge")]
    public Transform headTop;
    public float ledgeRayDistance;
    public bool checkBorder;
    public LayerMask borderMask;
    public bool isClimbLedge;
    public Transform toUp;
    private Vector3 pos;
    private bool once = false;
    private bool isDying = false;
    public static bool isDeath;

    private AudioSource audioSource;
    public AudioClip fallSound;

    private void Awake()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //haveKey = false;
        cap = GetComponent<CapsuleCollider>();
        Vector3 pos = cap.center;
        headCheck = 0;
        startHeigh = cap.height;
        starPosY = pos.y;

        detectFloor = false;
        rb = GetComponent<Rigidbody>();

        speedInitial = speed;
        speedCrouch = speed * 0.5f;
        speedRun = speed * 2f;
        speedPushing = speed * 0.5f;

        if(audioManager == null)
        {
            return;
        }
    }


    private void Update()
    {
        if (Manager.isPause == false && !isDying && Manager.isMainMenu==false)
        {
            CheckGround();

            Vector3 Floor = transform.TransformDirection(Vector3.down);
            Debug.DrawRay(transform.position, Floor * raycastDetect);
            hor = Input.GetAxisRaw("Horizontal");
            ver = Input.GetAxisRaw("Vertical");

            anim.SetFloat("VelX", hor, 0.2f, Time.deltaTime);
            anim.SetFloat("VelY", ver, 0.2f, Time.deltaTime);

            DetectWalls();

            if (!isClimbing && !isClimbLedge)
            {
                Movement();
            }

            Crouch();

            if (anim.GetBool("Grounded"))
            {
                //Run();
            }

            Push();
            Push2();
            push3();

            if (!isClimbing)
            {
                Jump();
            }

            Climb();
            UpLedge();

            if (isPushing)
            {
                speed = speedPushing;
            }
            else if (!isPushing && !canRun && !isCrouch)
            {
                speed = speedInitial;
            }
        }


    }

    void Movement()
    {
        camFwd = Vector3.Scale(cam.transform.forward, new Vector3(1, 1, 1)).normalized;
        Vector3 camFlatFwd = Vector3.Scale(cam.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 flatRight = new Vector3(cam.transform.right.x, 0, cam.transform.right.z);

        Vector3 m_CharForward = Vector3.Scale(camFlatFwd, new Vector3(1, 0, 1)).normalized;
        Vector3 m_CharRight = Vector3.Scale(flatRight, new Vector3(1, 0, 1)).normalized;

        float w_speed;

        Vector3 move = Vector3.zero;

        w_speed = speed;

        move = ver * m_CharForward * w_speed + hor * m_CharRight * speed;

        if (isPushing && Input.GetKey(KeyCode.W))
        {
            move = 1 * m_CharForward * w_speed * speed;
        }

        if (!isPushing)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, move, rotationSpeed, 0.0f));
        }


        transform.position += move * Time.deltaTime;

    }
    void CheckGround()
    {
        Vector3 dwn = transform.TransformDirection(Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(groundCheck.transform.position, dwn, out hit, raycastDetect, mask))
        {
            canJump = true;
            detectFloor = true;
            rb.useGravity = true;
            anim.SetBool("Grounded", true);
            anim.SetBool("Jump", false);
            anim.SetBool("Climbing", false);

        }
        else
        {
            canJump = false;
            detectFloor = false;
            anim.SetBool("Grounded", false);
            anim.SetBool("isRunning", false);

        }
    }

    void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && canRun && headCheck <= 0 && !isPushing)
        {
            speed = speedRun;
            anim.SetBool("isRunning", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && !isPushing)
        {
            speed = speedInitial;
            anim.SetBool("isRunning", false);
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("isRunning", false);
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump && !isPushing && !isClimbLedge) 
        {
            anim.SetBool("Jump", true);
            anim.SetBool("isRunning", false);
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }
    public void Push()
    {
      
        if (pickedObject != null)
        {
            if (Input.GetKeyUp(KeyCode.E)) //al soltar la tecla E el personaje dejara de empujar y jalar objetos
            {
                speed = speedInitial;
                //pickedObject.GetComponent<Rigidbody>().useGravity = true;
                //pickedObject.GetComponent<Rigidbody>().isKinematic = false;

                pickedObject.gameObject.transform.SetParent(null);
                pickedObject = null;
                anim.SetBool("isPushing", false);
                
                canRun = true;
                canJump = true;
                isCrouch = true;
                isPushing = false;
                
            }
        }
        RaycastHit hit;
        if (Physics.Raycast(hand.transform.position, hand.transform.forward, out hit, handRay))
        {
            if (hit.transform.gameObject.CompareTag("Object"))
            {
                if (Input.GetKey(KeyCode.E) && pickedObject == null && canJump)
                {
                    isPushing = true;
                    speed = speedPushing;
                    
                    anim.SetBool("isPushing", true);
                    //HandCollider.SetActive(true);
                //  hit.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
                   //hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    
                    hit.transform.position = handPush.transform.position;
                    hit.transform.SetParent(handPush.gameObject.transform);
                    pickedObject = hit.transform.gameObject;
                    canRun = false;
                    canJump = false;
                    isCrouch = false;
                  
                    
                   
                }
            }
        }
    }
    public void Push2()
    {

        if (pickedObject != null)
        {
            if (Input.GetKeyUp(KeyCode.E)) //al soltar la tecla E el personaje dejara de empujar y jalar objetos
            {
                speed = speedInitial;
              // pickedObject.GetComponent<Rigidbody>().useGravity = true;
                //pickedObject.GetComponent<Rigidbody>().isKinematic = false;

                pickedObject.gameObject.transform.SetParent(null);
                pickedObject = null;
                anim.SetBool("isPushing", false);

                canRun = true;
                canJump = true;
                isCrouch = true;
                isPushing = false;

            }
        }
        RaycastHit hit;
        if (Physics.Raycast(hand.transform.position, hand.transform.forward, out hit, handRay))
        {
            if (hit.transform.gameObject.CompareTag("Object2"))
            {
                if (Input.GetKey(KeyCode.E) && pickedObject == null && canJump)
                {
                    isPushing = true;
                    speed = speedPushing;

                    anim.SetBool("isPushing", true);
                    //HandCollider.SetActive(true);
                  // hit.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
                   // hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;

                    hit.transform.position = handPush2.transform.position;
                    hit.transform.SetParent(handPush2.gameObject.transform);
                    pickedObject = hit.transform.gameObject;
                    canRun = false;
                    canJump = false;
                    isCrouch = false;



                }
            }
        }
    }
    public void push3()
    {
        if (pickedObject != null)
        {
            if (Input.GetKeyUp(KeyCode.E)) //al soltar la tecla E el personaje dejara de empujar y jalar objetos
            {
                speed = speedInitial;
                // pickedObject.GetComponent<Rigidbody>().useGravity = true;
                //pickedObject.GetComponent<Rigidbody>().isKinematic = false;

                pickedObject.gameObject.transform.SetParent(null);
                pickedObject = null;
                anim.SetBool("isPushing", false);

                canRun = true;
                canJump = true;
                isCrouch = true;
                isPushing = false;

            }
        }
        RaycastHit hit;
        if (Physics.Raycast(hand.transform.position, hand.transform.forward, out hit, handRay))
        {
            if (hit.transform.gameObject.CompareTag("Object3"))
            {
                if (Input.GetKey(KeyCode.E) && pickedObject == null && canJump)
                {
                    isPushing = true;
                    speed = speedPushing;

                    anim.SetBool("isPushing", true);
                    //HandCollider.SetActive(true);
                    // hit.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    // hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;

                    hit.transform.position = handPush3.transform.position;
                    hit.transform.SetParent(handPush3.gameObject.transform);
                    pickedObject = hit.transform.gameObject;
                    canRun = false;
                    canJump = false;
                    isCrouch = false;



                }
            }
        }
    }

    public void Crouch()
    {
        RaycastHit hit;

        if (Physics.Raycast(head.transform.position, head.transform.up, out hit, headRay))
        {
            headCheck++;
            isCrouch = true;
        }
        else
        {
            headCheck = 0;
            headCheck--;
            isCrouch = false;
        }

        if (Input.GetKey(KeyCode.LeftControl) && canJump)
        {
            anim.SetBool("isCrouching", true);
            speed = speedCrouch;

            isCrouch = true;
            cap.height = heighCollider;
            cap.center = new Vector3(cap.center.x, positionY, cap.center.z);
            canRun = false;
        }
        else
        {
            if (headCheck <= 0)
            {
                anim.SetBool("isCrouching", false);
                speed = speedInitial;
                cap.height = startHeigh;
                cap.center = new Vector3(cap.center.x, starPosY, cap.center.z);
                canRun = true;
            }
        }
    }



    void DetectWalls()
    {
        RaycastHit[] ray = Physics.BoxCastAll(body.position, body.lossyScale / 2, body.forward, body.rotation, checkDistance,layerMask);
        RaycastHit[] ray2 = Physics.BoxCastAll(headTop.position, headTop.lossyScale / 2, headTop.forward,headTop.rotation, ledgeRayDistance,borderMask);

        wallCheck = false;
        checkBorder = false;

        for (int i = 0; i < ray.Length; i++)
        {
            if (ray[i].collider.gameObject !=null)
            {
                wallCheck = true;
            }
        }

        if (wallCheck)
        {
            for (int i = 0; i < ray2.Length; i++)
            {
                if (ray2[i].collider.gameObject != null)
                {
                    checkBorder = true;
                }
            }
        }
    }

    void UpLedge()
    {
        if (checkBorder && wallCheck)
        {
            anim.SetBool("UpLedge", true);
            isClimbLedge = true;
            if (!once)
            {
                pos = toUp.position;
                once = true;
            }

            StartCoroutine(ClimbLedge(transform.position, pos));
        }

        if (transform.position == pos)
        {
            anim.SetBool("UpLedge", false);
            rb.useGravity = true;
        }
    }

    IEnumerator ClimbLedge(Vector3 start, Vector3 target)
    {
        float timer = 0;
        while (transform.position != target)
        {
            rb.useGravity = false;
            transform.position = Vector3.Lerp(start, target, timer / 1f);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = target;
        if (transform.position == target)
        {
            isClimbing = false;
            isClimbLedge = false;
            once = false;
            anim.SetBool("UpLedge", false);
        }
    }

    void Climb()
    {
        RaycastHit[] ray = Physics.BoxCastAll(body.position, body.lossyScale / 2, body.forward, body.rotation, checkDistance, layerMask2);
        
        climbWallCheck = false;

        for (int i = 0; i < ray.Length; i++)
        {
            if (ray[i].collider.gameObject != null)
            {
                climbWallCheck = true;

                if (isClimbing)
                {
                    if (ray[i].collider.gameObject.CompareTag("Rope"))
                    {
                        anim.SetBool("ClimbRope", true);
                        transform.Translate(Vector3.up * climbRopeSpeed * Time.deltaTime);
                    }
                    else
                    {
                        anim.SetBool("Climbing", true);
                        transform.Translate(Vector3.up * climbSpeed * Time.deltaTime);
                    }
                }
            }
        }

        if (Input.GetKey(KeyCode.W) && climbWallCheck && !checkBorder)
        {
            rb.useGravity = false;
            isClimbing = true;
        }

        if (Input.GetKeyUp(KeyCode.W) && isClimbing && !checkBorder)
        {
            rb.useGravity = true;
            isClimbing = false;
            anim.SetBool("ClimbRope", false);
            anim.SetBool("Climbing", false);
        }
    }

    private IEnumerator DeathAnim()
    {
        rb.isKinematic = true;
        isDying = true;
        if (audioManager != null)
        {
            //audioManager.Death();
        }
        anim.SetBool("isDeath", true);
        yield return new WaitForSeconds(3f);
        isDeath = true;
        if (audioManager != null)
        {
            audioManager.ScreenDeath();
        }
    }

    public void Death()
    {
        StartCoroutine("DeathAnim");
    }

    public void FallSound()
    {
        audioSource.PlayOneShot(fallSound);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(head.transform.position, head.transform.up * headRay);
        Gizmos.DrawRay(transform.position, Vector3.down * raycastDetect);
        Gizmos.DrawRay(hand.transform.position, hand.transform.forward * handRay);
        Gizmos.color = Color.blue;
        if (wallCheck)
        {
            Gizmos.DrawWireCube(body.position + body.forward * HIT.distance, body.lossyScale);
        }

        if (checkBorder)
        {
            Gizmos.DrawWireCube(headTop.position + headTop.forward * HIT.distance, headTop.lossyScale);
        }
        Gizmos.DrawWireCube(body.position + body.forward * HIT.distance, body.lossyScale);
        Gizmos.DrawWireCube(headTop.position + headTop.forward * HIT.distance, headTop.lossyScale);
    }

    

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Death();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Death();
        }
    }
}



