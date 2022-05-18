using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbital : MonoBehaviour
{
    [SerializeField]
    private Transform FollowCam;
    [SerializeField]
    private float VelCamera = 120;
    [SerializeField]
    private float sensibility = 150;
    private float mousex;
    private float mousez;
    private float rotY = 0;
    private float rotX = 0;
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerFollow();
        CamaraController();
    }
    public void PlayerFollow()
    {
        transform.position = Vector3.MoveTowards(transform.position, FollowCam.position, VelCamera * Time.deltaTime);
    } 
    public void CamaraController()
    {
        mousex = Input.GetAxis("Mouse X");
        mousez = Input.GetAxis("Mouse Y");
        rotY += mousex * sensibility * Time.deltaTime;
        rotX -= mousez * sensibility * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, -60, 60);
        transform.rotation = Quaternion.Euler(rotX, rotY, 0);
    }
}
