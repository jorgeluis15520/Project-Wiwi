using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;

    //Movement
    public float speed;
    public float jumpForce;
    public bool groundCheck;
    public bool canJumpcheck;

    //Rotate
    private float hor;
    private float ver;
    private float horRot;
    private float verRot;
    public GameObject playerObject;
    float angle;
    Quaternion targetRotation;

    //Agacharse;
    public CapsuleCollider colUp;
    public CapsuleCollider colDown;
    public GameObject head;
    public HeadCheck headCheck;
    public bool isCrouch;


    //animation
    public Animator anim;

    public float velocidadInicial;
    public float velocidadAgachado;
    public float velocidadCorrer;

    public float gravitMod = 2;

    // Start is called before the first frame update
    void Start()
    {


        groundCheck = false;
        rb = GetComponent<Rigidbody>();

        velocidadInicial = speed;
        velocidadAgachado = speed * 0.5f;
        velocidadCorrer = speed * 1.5f;


        Physics.gravity *= gravitMod; //Modificador de la gravedad
    }


    private void Update()
    {
        hor = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");

        anim.SetFloat("speed", speed);
        anim.SetFloat("VelX", hor);
        anim.SetFloat("VelY", ver);

        Movement();
        if (!Input.GetKey("e")) //Si no se esta presionando la tecla E, el personaje podra rotar
        {
            CalculateDirection();
            Rotate();
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetBool("agachado", true);
            speed = velocidadAgachado;
            canJumpcheck = false;

            colDown.enabled = true;
            colUp.enabled = false;

            head.SetActive(true);
            isCrouch = true;
        }

        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            if (headCheck.CountColission <= 0)
            {
                anim.SetBool("agachado", false);
                speed = velocidadInicial;
                canJumpcheck = true;

                head.SetActive(false);
                colDown.enabled = false;
                colUp.enabled = true;
                isCrouch = false;
            }

        }
    }

    void Movement()
    {

        if (hor != 0.0f || ver != 0.0f) //Si el valor de ver y hor no es igual a cero, el player se movera de acuerdo al vector y velocidad; 
        {
            Vector3 dir = transform.forward * ver + transform.right * hor;

            rb.MovePosition(transform.position + dir * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) //Si se mantiene presionado Shift, la velocidad cambia
        {
            speed = velocidadCorrer;

        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) //Al soltar el boton Shift
        {
            speed = velocidadInicial;

        }

        if (groundCheck && canJumpcheck)
        {
            if (Input.GetKeyDown(KeyCode.Space)) //Al presionar espacio, el personaje saltara de acuerdo al jumpForce
            {
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            }
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

    void Rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        playerObject.transform.rotation = Quaternion.Slerp(playerObject.transform.rotation, targetRotation, speed * Time.deltaTime);
    }

    private void OnAnimatorMove()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        playerObject.transform.rotation = Quaternion.Slerp(playerObject.transform.rotation, targetRotation, speed * Time.deltaTime);
    }

}
