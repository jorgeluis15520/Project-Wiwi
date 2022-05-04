using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoController : MonoBehaviour
{
    public Transform[] wayPoints;
    public int currentPoint;
    public float speed;
    public float speedRotation;
    
    public Transform targetPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetPlayer = GetComponent<FieldOfView>().player;

        if (targetPlayer == null)
        {
            Move();
        }
        else
        {
            FollowPLayer();
        }
    }

    void Move()
    { 
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(wayPoints[currentPoint].position.x, transform.position.y, wayPoints[currentPoint].position.z), speed * Time.deltaTime);

        float dist = Vector3.Distance(transform.position, wayPoints[currentPoint].position);

        if (dist <= 1)
        {
            currentPoint++;

            if (currentPoint >= wayPoints.Length)
            {
                currentPoint = 0;
            }
        }

        var dir = wayPoints[currentPoint].position - transform.position;

        var rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speedRotation * Time.deltaTime);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }

    void FollowPLayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPlayer.position.x, transform.position.y, targetPlayer.position.z), speed * Time.deltaTime);

        var dir = targetPlayer.position - transform.position;
        var rotation = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speedRotation * Time.deltaTime);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }
}
