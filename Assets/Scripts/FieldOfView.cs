using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    public event Action OnDetectionAction;

    public Transform DamageImagePivot;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        var viewingPosition = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);

        // find player in targetMask inside radius
        Collider[] rangeChecks = Physics.OverlapSphere(viewingPosition, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            var targetViewingPosition = new Vector3(target.transform.position.x, target.transform.position.y + 1.3f, target.transform.position.z);

            // get direction to target inside radius
            Vector3 directionToTarget = (targetViewingPosition - viewingPosition).normalized;

            // check if target is inside specified viewing angle
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(viewingPosition, targetViewingPosition);

                // check if in between player and viewer there is an obstacle
                if (!Physics.Raycast(viewingPosition, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                    
                    DamageImagePivot.gameObject.SetActive(true);
                    float angle = Vector3.SignedAngle(-directionToTarget, target.transform.forward, Vector3.up);
                    DamageImagePivot.transform.localEulerAngles = new Vector3(0, 0, angle);

                    OnDetectionAction.Invoke();
                }
                else
                {
                    PlayerUnseen();
                }
            }
            else
            {
                PlayerUnseen();
            }
        }
        else if (canSeePlayer)
        {
            PlayerUnseen();
        }
    }

    private void PlayerUnseen()
    {
        canSeePlayer = false;
        DamageImagePivot.gameObject.SetActive(false);
    }
}