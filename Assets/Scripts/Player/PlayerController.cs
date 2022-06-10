using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;

    [Header("Movement")]
    public float speed;
    public float jumpspeed;
    public bool DetectFloor = false;
    public float RaycastDetect;
    private bool canJump;
    private bool canRun;
    public GameObject groundCheck;
    public LayerMask Mask;
    private float hor;
    private float ver;

    [Header("Camera")]
    public Camera cam;
    private Vector3 camFwd;
    public float rotation_speed;


    [Header("Crouch")]
    public float headRay;
    public float headCheck;
    public bool isCrouch;
    public GameObject head;
    private CapsuleCollider cap;
    private float startHeigh;
    private float starPosY;
    private float heighCollider = 1.47f;
    private float positionY = 0.75f;

    [Header("Animation")]
    public Animator anim;

    public float velocidadInicial;
    public float velocidadAgachado;
    public float velocidadCorrer;

    [Header("Inventary")]
    public bool haveKey;

    [Header("Climb")]
    public Transform spine;
    public LayerMask layerMask;
    public bool wallChek;
    public float climbSpeed;
    public float checkDistance;
    private bool isClimbing = false;

    [Header("Up Ledge")]
    public Transform headTop;
    public Vector3 rayDistance;
    public bool checkBorder;
    public LayerMask borderMask;

    // Start is called before the first frame update
    void Start()
    {
        haveKey = false;
        cap = GetComponent<CapsuleCollider>();
        Vector3 pos = cap.center;
        headCheck = 0;
        startHeigh = cap.height;
        starPosY = pos.y;

        DetectFloor = false;
        rb = GetComponent<Rigidbody>();

        velocidadInicial = speed;
        velocidadAgachado = speed * 0.5f;
        velocidadCorrer = speed * 2f;
    }


    private void Update()
    {
        CheckGround();

        Vector3 Floor = transform.TransformDirection(Vector3.down);
        Debug.DrawRay(transform.position, Floor * RaycastDetect);
        hor = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");

        anim.SetFloat("speed", speed);
        anim.SetFloat("VelX", hor);
        anim.SetFloat("VelY", ver);

        if (!isClimbing)
        {
            Movement();
        }
       
        Crouch();
        if (anim.GetBool("Inwall"))
        {
            Run();
        }
        
        Jump();
        CheckWall();
        CheckLedge();
        Climb();

        anim.SetBool("Climbing", isClimbing);

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


        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, move, rotation_speed, 0.0f));

        transform.position += move * Time.deltaTime;

    }
    void CheckGround()
    {
        Vector3 dwn = transform.TransformDirection(Vector3.down);
        RaycastHit hit;

        

        if (Physics.Raycast(groundCheck.transform.position,dwn, out hit, RaycastDetect, Mask))
        {
            canJump = true;
            DetectFloor = true;
            rb.useGravity = true;
            anim.SetBool("Inwall", true);
            anim.SetBool("Jump", false);
        }
        else
        {
            canJump = false;
            DetectFloor = false;
            anim.SetBool("Inwall", false);
            anim.SetBool("isRunning", false);

        }
    }
    void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && canRun && headCheck <= 0) 
        {
            speed = velocidadCorrer;
            anim.SetBool("isRunning", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) 
        {
            speed = velocidadInicial;
            anim.SetBool("isRunning", false);
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("isRunning", false);
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            anim.SetBool("Jump", true);
            anim.SetBool("isRunning", false);
            rb.AddForce(new Vector3(0, jumpspeed, 0), ForceMode.Impulse);

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
            anim.SetBool("agachado", true);
            speed = velocidadAgachado;
            
            isCrouch = true;
            cap.height = heighCollider;
            cap.center = new Vector3(cap.center.x, positionY, cap.center.z);
            canRun = false;
        }
        else
        {

            if (headCheck <= 0)
            {
                anim.SetBool("agachado", false);
                speed = velocidadInicial;
               
                cap.height = startHeigh;
                cap.center = new Vector3(cap.center.x, starPosY, cap.center.z);
                canRun = true;
               
            }

            
        }

    }

    void CheckWall()
    {
        RaycastHit hit;

        if (Physics.Raycast(spine.position, spine.forward, out hit, checkDistance, layerMask))
        {
            wallChek = true;
        }
        else
        {
            wallChek = false;
        }
    }

    void CheckLedge()
    {
        checkBorder = Physics.CheckBox(headTop.position, rayDistance, headTop.rotation, borderMask);

        anim.SetBool("UpLedge", checkBorder);
    }

    void Climb()
    {

        if (Input.GetKey(KeyCode.W) && wallChek && !checkBorder)
        {
            rb.useGravity = false;
            isClimbing = true;
            transform.Translate(Vector3.up * climbSpeed * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.W) && isClimbing && wallChek)
        {
            rb.useGravity = true;
            isClimbing = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(head.transform.position, head.transform.up * headRay);
        Gizmos.DrawRay(transform.position, Vector3.down * RaycastDetect);
        Gizmos.DrawRay(spine.position, spine.forward * checkDistance);
        Gizmos.DrawWireCube(headTop.position, rayDistance);
    }
}


