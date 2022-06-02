using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;

    //Movement
    public float speed;
    /*public float jumpForce;
      public bool groundCheck;
      public bool canJumpcheck;*/
    public float jumpspeed;
    public bool DetectFloor = false;
    public float RaycastDetect;
    private bool canJump;
    private bool canRun;
    public GameObject groundCheck;
    public LayerMask Mask;



    //camera

    public Camera cam;
    public CameraOrbital co;
    private Vector3 camFwd;

    public float rotation_speed;

    //Rotate
    private float hor;
    private float ver;
    private float horRot;
    private float verRot;
    public GameObject playerObject;
    float angle;
    Quaternion targetRotation;

    //Agacharse;
    public float headRay;
    public float headCheck;
    public bool isCrouch;
    public GameObject head;


    //animation
    public Animator anim;

    public float velocidadInicial;
    public float velocidadAgachado;
    public float velocidadCorrer;

    //public float gravitMod = 2;

    private CapsuleCollider cap;
    private float startHeigh;
    private float starPosY;

    private float heighCollider = 1.47f;
    private float positionY = 0.75f;

    //inventario

    public bool haveKey;


    public LayerMask layerMask;
    public bool wallChek;
    public float climbSpeed;
    public float rayDistance;
    private bool isClimbing = false;


    public Transform headTop;
    public Transform spine;
    public bool grabBorder;

    private Transform grabTransform;

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
        velocidadCorrer = speed * 2f; ;
        
        


        /*Physics.gravity *= gravitMod;*/ //Modificador de la gravedad
    }


    private void Update()
    {


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
        CheckGround();
        Jump();
        CheckWall();
        CheckLedge();
        Climb();

        anim.SetBool("Climbing", isClimbing);
        anim.SetBool("UpLedge", grabBorder);
        //if (!Input.GetKey("e")) //Si no se esta presionando la tecla E, el personaje podra rotar
        //{
        //    CalculateDirection();
        //    Rotate();
        //}
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

        

        if (Physics.Raycast(groundCheck.transform.position,dwn, out hit, RaycastDetect) && hit.collider.CompareTag("Floor"))
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
        if (Input.GetKey(KeyCode.LeftShift) && canRun && headCheck <= 0) //Si se mantiene presionado Shift, la velocidad cambia
        {
            speed = velocidadCorrer;
            anim.SetBool("isRunning", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) //Al soltar el boton Shift
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

        //(Physics.Raycast(head.transform.position, head.transform.up * headRay))


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


        if (Physics.Raycast(spine.position, spine.forward, out hit, rayDistance, layerMask))
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
        RaycastHit hit;

        if (Physics.Raycast(headTop.position, headTop.forward, out hit, rayDistance, layerMask))
        {
            grabBorder = false;
        }
        else
        {
            if (wallChek)
            {
                grabBorder = true;
            }
        }
    }

    void Climb()
    {

        if (Input.GetKey(KeyCode.W) && wallChek && !grabBorder)
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
    //void CalculateDirection()
    //{
    //    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))) //guarda la ultima rotación hecha
    //    {
    //        verRot = ver;
    //        horRot = hor;
    //    }
    //    angle = Mathf.Atan2(horRot, verRot);
    //    angle = Mathf.Rad2Deg * angle;

    //}

    //void Rotate()
    //{
    //    targetRotation = Quaternion.Euler(0, angle, 0);
    //    playerObject.transform.rotation = Quaternion.Slerp(playerObject.transform.rotation, targetRotation, speed * Time.deltaTime);
    //}
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(head.transform.position, head.transform.up * headRay);
        Gizmos.DrawRay(transform.position, Vector3.down * RaycastDetect);
        Gizmos.DrawRay(spine.position, spine.forward * rayDistance);
        Gizmos.DrawRay(headTop.position, headTop.forward * rayDistance);
    }
}


