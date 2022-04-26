using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;

    public float speed;
    public float jumpForce;
    public bool canJump;

    //public float rotation;
    public float hor;
    public float ver;

    public float gravitMod = 2;

    // Start is called before the first frame update
    void Start()
    {
        canJump = false;
        rb = GetComponent<Rigidbody>();

        Physics.gravity *= gravitMod; //Modificador de la gravedad
    }




    void FixedUpdate()
    {



    }

    private void Update()
    {
        hor = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");

        Movement();
        //Rotate();


    }

    void Movement()
    {

        if (hor != 0.0f || ver != 0.0f) //Si el valor de ver y hor no es igual a cero, el player se movera de acuerdo al vector y velocidad; 
        {
            Vector3 dir = transform.forward * ver + transform.right * hor;

            rb.MovePosition(transform.position + dir * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) //Si se mantiene presionado Shift, la velocidad sera de 40
        {
            speed = 35f;

        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) //Al soltar el boton Shift, la velocidad volvera a ser 20
        {
            speed = 20f;

        }

        if (canJump)
        {
            if (Input.GetKeyDown(KeyCode.Space)) //Al presionar espacio, el personaje saltara de acuerdo al jumpForce
            {
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            }
        }
    }



    //void Rotate()
    //{

    //    {
    //        if (Input.GetKey(KeyCode.A))
    //        {
    //            transform.Rotate(new Vector3(0f, -rotation, 0f) * Time.deltaTime);
    //        }

    //        if (Input.GetKey(KeyCode.D))
    //        {
    //            transform.Rotate(new Vector3(0f, rotation, 0f) * Time.deltaTime);
    //        }
    //    }
    //}
}
