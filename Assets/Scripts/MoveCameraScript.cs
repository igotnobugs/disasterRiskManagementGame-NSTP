using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraScript : MonoBehaviour
{

    public Vector3 targetPoint;
    public GameObject targetObj;

    public Vector3 vectorDistance;
    private Vector3 initialDistance;

    public bool centerOnTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        vectorDistance = this.transform.position - targetObj.transform.position;
        initialDistance = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (centerOnTarget) {
            this.transform.position.Set(targetObj.transform.position.x,
                this.transform.position.y,
                this.transform.position.z);
        }
        else {
            this.transform.position = targetObj.transform.position + vectorDistance;
        }
    }
}
