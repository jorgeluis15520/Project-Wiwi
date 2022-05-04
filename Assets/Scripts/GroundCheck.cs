using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerController playerController;
  

    private void OnTriggerStay(Collider other) //Si el trigger entra contacto con un collider, el personaje puede saltar
    {

        if (other.CompareTag("Floor"))
        {
            playerController.canJump = true;
        }

    }

    private void OnTriggerExit(Collider other)//Al no hacer contacto, se desactiva el salto
    {
        playerController.canJump = false;


    }
}
