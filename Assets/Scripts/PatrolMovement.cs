using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PatrolMovement : MonoBehaviour
{
    public GameObject[] waypoints;
    int currentWp = 0;

    public float gravity = 9.8f;
    public float moveSpeed = 10.0f;

    private Vector3 _moveDirection = Vector3.zero;
    private CharacterController _charCont;

    private Animator animator;
    private bool hasDetected = false;
    private bool isMoving = true;

    // Start is called before the first frame update
    void Start()
    {
        _charCont = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasDetected) {
            animator.SetBool("isMoving", true);
            if(Vector3.Distance(this.transform.position, waypoints[currentWp].transform.position) < 1)
            {
                currentWp++;
            }
            if(currentWp >= waypoints.Length)
            {
                currentWp = 0;
            }

            this.transform.LookAt(new Vector3(waypoints[currentWp].transform.position.x, transform.position.y, waypoints[currentWp].transform.position.z));
            _moveDirection = transform.forward;
            _moveDirection.y -= this.gravity * Time.deltaTime;
            // Move the controller
            _charCont.Move(_moveDirection * Time.deltaTime);
        }
    }
}
