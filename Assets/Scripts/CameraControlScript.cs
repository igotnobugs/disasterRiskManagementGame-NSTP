using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlScript : MonoBehaviour
{
    public Vector3 offset;
    public Vector3 target;

    private float t = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.x, t)
                , 10f
                , Mathf.Lerp(transform.position.z, target.z + offset.z, t));

        t += 0.5f * Time.deltaTime;
    }

    public void newTarget() {
        t = 0;
    }
}
