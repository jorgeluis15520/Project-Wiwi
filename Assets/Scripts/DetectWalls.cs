using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectWalls : MonoBehaviour
{
    private float minDistance = 1;
    private float Maxdistance = 5;
    private float SlowCam = 10;
    private float distance;
    Vector3 Direction;
    void Start()
    {
        distance = transform.localPosition.magnitude;
        Direction = transform.localPosition.normalized;

    }

    // Update is called once per frame
    void Update()
    {
        CollisionCamera();
    } 
      private void CollisionCamera()
    {
        Vector3 posCamera = transform.parent.TransformPoint(Direction * Maxdistance);
        RaycastHit hit;
        if (Physics.Linecast(transform.parent.position, posCamera, out hit))
        {
            distance = Mathf.Clamp(hit.distance * 0.85f, minDistance, Maxdistance);
        }
        else
        {
            distance = Maxdistance;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, Direction * distance, SlowCam * Time.deltaTime); 
    }
}