using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterControlScript : MonoBehaviour
{
    public Vector3 target;
    public bool faceTowardsTarget = false;

    public bool isLooking = false;
    public bool isWalking = false;
    public bool isRunning = false;

    public bool startMoving = false;

    private Animator gAnim;

    public NavMeshAgent agent;

    public Vector3 theDestination;
    public bool destinationReached = true;
    float singleStep;

    // Start is called before the first frame update
    void Start()
    {
        gAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLooking) {
            Vector3 targetDirection = target - transform.position;
            singleStep = 1 * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        } 

        gAnim.SetBool("isLooking", isLooking);
        gAnim.SetBool("isWalking", isWalking);
        gAnim.SetBool("isRunning", isRunning);

        if ((isWalking || isRunning) && !destinationReached) {
            if (gAnim.GetCurrentAnimatorStateInfo(0).IsName("Running")) {
                agent.speed = 5.5f;
                agent.SetDestination(theDestination);
                float distance = Vector3.Distance(transform.position, theDestination);

                if (distance <= 1.0) {
                    destinationReached = true;
                }
            }

            if (gAnim.GetCurrentAnimatorStateInfo(0).IsName("Walking")) {
                agent.speed = 2.5f;
                agent.SetDestination(theDestination);
                float distance = Vector3.Distance(transform.position, theDestination);

                if (distance <= 1.0) {
                    destinationReached = true;
                }
            }
        }

        if (destinationReached) {
            isRunning = false;
            isWalking = false;
            gAnim.SetBool("isRunning", false);
            gAnim.SetBool("isWalking", false);
        }
    }

    public void MoveToPosition(Vector3 destination) {
        isLooking = false;
        destinationReached = false;
        startMoving = true;
        theDestination = destination;
    }
}
