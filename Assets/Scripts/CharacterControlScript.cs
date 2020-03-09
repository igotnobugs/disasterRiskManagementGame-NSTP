using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterControlScript : MonoBehaviour
{
    public Vector3 target;
    public bool faceTowardsTarget = false;
    public bool isLooking = false;
    public bool startMoving = false;

    private Animator gAnim;

    public NavMeshAgent agent;
    public Vector3 dest;

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
            float singleStep = 1 * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        } 

        gAnim.SetBool("isLooking", isLooking);


        if (startMoving) {

            agent.SetDestination(dest);

        }
    }

    public void MoveToPosition(Vector3 destination) {
        startMoving = true;
        dest = destination;
    }
}
