using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlScript : MonoBehaviour
{
    public Vector3 offset;
    public Vector3 target;

    public bool isTargeting;
    public bool isMainMenu = true;

    private float t = 0.0f;
    private Vector3 defaultPosition;
    private Quaternion defaultRotation;
    private bool doRotation = true;

    public bool allowReturnPosition = false;
    //need someway to reset this
    public float mt = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = gameObject.transform.position;
        defaultRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTargeting) {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.x + offset.x, t)
                    , offset.y
                    , Mathf.Lerp(transform.position.z, target.z + offset.z, t));
            t += 0.5f * Time.deltaTime;           
        } else {
            if (allowReturnPosition) {
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, defaultPosition.x, t)
                    , defaultPosition.y
                    , Mathf.Lerp(transform.position.z, defaultPosition.z, t));
                t += 0.5f * Time.deltaTime;
            }
        }

    }

    public void GoToNewPosition(GameObject newPosition, float time = 0) {
        mt += time * Time.deltaTime;
        if (transform.position != newPosition.transform.position) {
            transform.position = Vector3.Lerp(transform.position, newPosition.transform.position, mt);
        //} else {
        //    defaultPosition = newPosition.transform.position;
        }

        if (transform.rotation != newPosition.transform.rotation) {
            transform.rotation = Quaternion.Slerp(transform.rotation, newPosition.transform.rotation, mt);
        //} else {
        //    defaultRotation = newPosition.transform.rotation;
        }

        defaultPosition = newPosition.transform.position;
        defaultRotation = newPosition.transform.rotation;
    }

    public void NewTarget() {
        t = 0;
        mt = 0;
    }

    public bool ReachedTargetLocation(GameObject location) {
        return this.transform.position == location.transform.position;
    }
}
