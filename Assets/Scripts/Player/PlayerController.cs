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
    public bool canJump;
    public bool canRun;
    public GameObject groundCheck;
    public LayerMask Mask;
    private float hor;
    private float ver;

    [Header("Pushing")]
    public GameObject Hand;
   // public GameObject HandCollider;
    public float HandRay;
    private GameObject pickedObject = null;
    public GameObject handPush;
    public GameObject handPush2;
    public bool isPushing;
 //   public float forcePush;
    



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
    public float VelocityPushing;

    [Header("Inventary")]
    public bool haveKey;

    [Header("Climb")]
    public Transform spine;
    public LayerMask layerMask;
    public Vector3 bodyRayDistance;
    public bool climbWallCheck;
    public float climbSpeed;
    public float checkDistance;
    private bool isClimbing = false;

    [Header("Up Ledge")]
    public Transform headTop;
    public Vector3 ledgeRayDistance;
    public bool checkBorder;
    public bool wallCheck;
    public LayerMask borderMask;
    private float upTimer;
    public float upSpeed;
    public bool isClimbLedge;
    public float upDuration;
    public Transform toUp;


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
        VelocityPushing = speed * 0.2f;
    }


    private void Update()
    {
        if (Manager.isPause == false)
        {
            CheckGround();

            Vector3 Floor = transform.TransformDirection(Vector3.down);
            Debug.DrawRay(transform.position, Floor * RaycastDetect);
            hor = Input.GetAxisRaw("Horizontal");
            ver = Input.GetAxisRaw("Vertical");

            anim.SetFloat("speed", speed);
            anim.SetFloat("VelX", hor);
            anim.SetFloat("VelY", ver);

            CheckLedge();

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

            Climb();
            UpLedge();
            anim.SetBool("Climbing", isClimbing);
            push();
            push2();
            
            if (isPushing)
            {
                speed = VelocityPushing;
            }
            else if (!isPushing && !canRun && !isCrouch)
            {
                speed = velocidadInicial;
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


        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, move, rotation_speed, 0.0f));

        transform.position += move * Time.deltaTime;

    }
    void CheckGround()
    {
        Vector3 dwn = transform.TransformDirection(Vector3.down);
        RaycastHit hit;



        if (Physics.Raycast(groundCheck.transform.position, dwn, out hit, RaycastDetect, Mask))
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
        if (Input.GetKey(KeyCode.LeftShift) && canRun && headCheck <= 0 && !isPushing)
        {
            speed = velocidadCorrer;
            anim.SetBool("isRunning", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && !isPushing)
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
        if (Input.GetKeyDown(KeyCode.Space) && canJump && !isPushing) 
        {
            anim.SetBool("Jump", true);
            anim.SetBool("isRunning", false);
            rb.AddForce(new Vector3(0, jumpspeed, 0), ForceMode.Impulse);

        }
    }
    public void push()
    {
      
        if (pickedObject != null)
        {
            if (Input.GetKeyUp(KeyCode.E)) //al soltar la tecla E el personaje dejara de empujar y jalar objetos
            {
                speed = velocidadInicial;
                pickedObject.GetComponent<Rigidbody>().useGravity = true;
               pickedObject.GetComponent<Rigidbody>().isKinematic = false;

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
        if (Physics.Raycast(Hand.transform.position, Hand.transform.forward, out hit, HandRay))
        {
            if (hit.transform.gameObject.CompareTag("Object"))
            {
                if (Input.GetKey(KeyCode.E) && pickedObject == null && canJump)
                {
                    isPushing = true;
                    speed = VelocityPushing;
                    
                    anim.SetBool("isPushing", true);
                    //HandCollider.SetActive(true);
                  hit.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
                   hit.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    
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
    public void push2()
    {

        if (pickedObject != null)
        {
            if (Input.GetKeyUp(KeyCode.E)) //al soltar la tecla E el personaje dejara de empujar y jalar objetos
            {
                speed = velocidadInicial;
               pickedObject.GetComponent<Rigidbody>().useGravity = true;
                pickedObject.GetComponent<Rigidbody>().isKinematic = false;

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
        if (Physics.Raycast(Hand.transform.position, Hand.transform.forward, out hit, HandRay))
        {
            if (hit.transform.gameObject.CompareTag("Object2"))
            {
                if (Input.GetKey(KeyCode.E) && pickedObject == null && canJump)
                {
                    isPushing = true;
                    speed = VelocityPushing;

                    anim.SetBool("isPushing", true);
                    //HandCollider.SetActive(true);
                   hit.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
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



    void CheckLedge()
    {
        wallCheck = Physics.CheckBox(spine.position, bodyRayDistance, spine.rotation, borderMask);
        checkBorder = Physics.CheckBox(headTop.position, ledgeRayDistance, headTop.rotation, borderMask);
    }

    void UpLedge()
    {
        if (!checkBorder && wallCheck)
        {
            anim.SetBool("UpLedge", checkBorder);
        }
    }

    void Climb()
    {
        climbWallCheck = Physics.CheckBox(spine.position, bodyRayDistance, spine.rotation, layerMask);

        if (Input.GetKey(KeyCode.W) && climbWallCheck && !checkBorder)
        {
            rb.useGravity = false;
            isClimbing = true;
            transform.Translate(Vector3.up * climbSpeed * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.W) && climbWallCheck && wallCheck)
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
        Gizmos.DrawWireCube(headTop.position, bodyRayDistance);
        Gizmos.DrawWireCube(spine.position, ledgeRayDistance);
        Gizmos.DrawRay(Hand.transform.position, Hand.transform.forward * HandRay);
    }

  
    /*public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            Debug.Log("CHALEX");
            Rigidbody rgbd = collision.collider.attachedRigidbody;
            if (rgbd != null)
            {
                Vector3 forceDirection = collision.gameObject.transform.position - transform.position;
                forceDirection.y = 0;
                forceDirection.Normalize();
                rgbd.AddForceAtPosition(forceDirection * forcePush, transform.position, ForceMode.Impulse);

            }
        }

    }*/
    

}



