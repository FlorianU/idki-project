using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PatrolMovement : MonoBehaviour
{
    public GameObject[] waypoints;
    int currentWp = 0;

    public float gravity = 9.8f;
    [Range(0, 3)]
    public float moveSpeed = 1f;
    [Range(1, 5)]
    public float sprintMultiplier = 1.8f;
    private Vector3 _moveDirection = Vector3.zero;

    private CharacterController _charCont;
    private Animator animator;
    private FieldOfView fov;
    private GameObject player;

    private bool hasDetected = false;
    private bool isMoving = true;

    // Start is called before the first frame update
    void Start()
    {
        _charCont = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        fov = GetComponent<FieldOfView>();

        fov.OnDetectionAction += Fov_OnDetectionAction;
    }

    private void Fov_OnDetectionAction()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hasDetected = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasDetected) {
            animator.SetBool("isMoving", true);

            // Get the next waypoint if distance is < 1
            if(Vector3.Distance(this.transform.position, waypoints[currentWp].transform.position) < 1)
            {
                currentWp++;
            }
            if(currentWp >= waypoints.Length)
            {
                currentWp = 0;
            }

            // Look at new waypoint
            this.transform.LookAt(new Vector3(waypoints[currentWp].transform.position.x, transform.position.y, waypoints[currentWp].transform.position.z));
            
            _moveDirection = transform.forward;
            _moveDirection.y -= this.gravity * Time.deltaTime;
            
            // Move the controller
            _charCont.Move(_moveDirection * Time.deltaTime * moveSpeed);
        } else
        {
            animator.SetBool("isRunning", true);

            this.transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

            _moveDirection = transform.forward;
            _moveDirection.y -= this.gravity * Time.deltaTime;

            // Move the controller
            _charCont.Move(_moveDirection * Time.deltaTime * moveSpeed * sprintMultiplier);
        }
    }
}
