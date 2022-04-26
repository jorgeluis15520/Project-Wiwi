using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other) //Si el trigger entra contacto con un collider, el personaje puede saltar
    {
        playerController.canJump = true;

    }

    private void OnTriggerExit(Collider other)//Al no hacer contacto, se desactiva el salto
    {
        playerController.canJump = false;


    }
}
