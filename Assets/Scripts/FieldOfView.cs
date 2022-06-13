using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public float alertRadius;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public LayerMask objectsMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    public List<Transform> alertTargets = new List<Transform>();

    public Transform targetPlayer;
    public Transform targetObject;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        alertTargets.Clear();
        targetPlayer = null;
        targetObject = null;

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        Collider[] targetsInAlertRadius = Physics.OverlapSphere(transform.position, alertRadius, objectsMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    targetPlayer = target;
                }
            }
        }

        if (targetPlayer == null)
        {
            for (int i = 0; i < targetsInAlertRadius.Length; i++)
            {
                Transform target = targetsInAlertRadius[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                alertTargets.Add(target);

                Objects obj = target.GetComponent<Objects>();

                if (obj.active)
                {
                    targetObject = target;
                }

            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
