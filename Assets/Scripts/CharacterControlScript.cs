using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlScript : MonoBehaviour
{
    public Vector3 target;

    public bool faceTowardsTarget = false;

    private Animator gAnim;
    public bool isLooking = false;
    public bool startMoving = false;

    private float t = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        gAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (faceTowardsTarget) {
            Vector3 targetDirection = target - transform.position;
            float singleStep = 1 * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        gAnim.SetBool("isLooking", isLooking);


        if (startMoving) {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.x, t)
                , transform.position.y
                , Mathf.Lerp(transform.position.z, target.z, t));

            t += 0.5f * Time.deltaTime;
        }
    }
}
