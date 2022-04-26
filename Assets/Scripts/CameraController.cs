using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camara Positions")]
    public Transform[] pointsView;

    public float speed;
    Transform currentView;

    public string[] objectTags;
    // Start is called before the first frame update
    void Start()
    {
        currentView = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentView = pointsView[1];
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            currentView = pointsView[0];
        }

    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * speed);

        Vector3 currentAngle = new Vector3(
            Mathf.Lerp(transform.rotation.eulerAngles.x, currentView.rotation.eulerAngles.x, Time.deltaTime * speed),
            Mathf.Lerp(transform.rotation.eulerAngles.y, currentView.rotation.eulerAngles.y, Time.deltaTime * speed),
            Mathf.Lerp(transform.rotation.eulerAngles.z, currentView.rotation.eulerAngles.z, Time.deltaTime * speed)
            );

        transform.eulerAngles = currentAngle;
    }

    void ChangePosition()
    {
        switch (objectTags)
        {
            default:
                break;
        }
    }
}
