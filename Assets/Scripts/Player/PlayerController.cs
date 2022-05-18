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
    public bool canJump;
    public bool canRun;
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

    public float gravitMod = 2;

    private CapsuleCollider cap;
    private float startHeigh;
    private float starPosY;

    private float heighCollider = 1.47f;
    private float positionY = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
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


        Physics.gravity *= gravitMod; //Modificador de la gravedad
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

        Movement();
        Run();
        CheckGround();
        Jump();

        Crouch();

         
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

        //cam.transform.position += move * Time.deltaTime;

        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, move, rotation_speed, 0.0f));

        transform.position += move * Time.deltaTime;


        //if (Input.GetKey(KeyCode.LeftShift)) //Si se mantiene presionado Shift, la velocidad cambia
        //{
        //    speed = velocidadAgachado;

        //}
        //if (Input.GetKeyUp(KeyCode.LeftShift)) //Al soltar el boton Shift
        //{
        //    speed = velocidadInicial;

        //}

        /*if (groundCheck && canJumpcheck)
          {
              if (Input.GetKeyDown(KeyCode.Space)) //Al presionar espacio, el personaje saltara de acuerdo al jumpForce
              {
                  rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
                  anim.SetBool("Jump", true);
              }
              anim.SetBool("Inwall", true);

          }
          else
          {
              FallingDown();
          }*/
    }
    void CheckGround()
    {
        Vector3 dwn = transform.TransformDirection(Vector3.down);
        RaycastHit hit;

        //Debug.DrawRay(transform.position, Vector3.down * RaycastDetect, Color.red);

        if (Physics.Raycast(groundCheck.transform.position,dwn, out hit, RaycastDetect) && hit.collider.CompareTag("Floor"))
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }
    void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && canRun) //Si se mantiene presionado Shift, la velocidad cambia
        {
            speed = velocidadCorrer;

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) //Al soltar el boton Shift
        {
            speed = velocidadInicial;

        }
    }
    void CalculateDirection()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))) //guarda la ultima rotación hecha
        {
            verRot = ver;
            horRot = hor;
        }
        angle = Mathf.Atan2(horRot, verRot);
        angle = Mathf.Rad2Deg * angle;

    }

    /*public void FallingDown()
      {
          anim.SetBool("Inwall", false);
          anim.SetBool("Jump", false);

      }*/
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            anim.SetBool("Jump", true);
            anim.SetBool("Inwall", true);
            rb.AddForce(new Vector3(0, jumpspeed, 0), ForceMode.Impulse);

        }

    }

    public void Crouch()
    {

        if (Physics.Raycast(head.transform.position, head.transform.up * headRay))
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

        if (Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetBool("agachado", true);
            speed = velocidadAgachado;
            DetectFloor = false;
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
                DetectFloor = true;
                cap.height = startHeigh;
                cap.center = new Vector3(cap.center.x, starPosY, cap.center.z);
                canRun = true;
                //isCrouch = false;
            }

            //isCrouch = false;
        }

        //if (/*!isCrouch && */headCheck <= 0)
        //{
        //    anim.SetBool("agachado", false);
        //    speed = velocidadInicial;
        //    DetectFloor = true;
        //    isCrouch = false;
        //    cap.height = startHeigh;
        //    cap.center = new Vector3(cap.center.x, starPosY, cap.center.z);
        //}

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(head.transform.position, head.transform.up * headRay);
        Gizmos.DrawRay(transform.position, Vector3.down * RaycastDetect);
    }
}


