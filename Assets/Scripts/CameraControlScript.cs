using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlScript : MonoBehaviour
{
    public Vector3 offset;
    public Vector3 target;

    //public bool isTargeting;

    private Vector3 defaultPosition;
    private Quaternion defaultRotation;

    //public bool allowReturnPosition;

    private bool constantUpdate = false;

    private GameObject firstObjectUpdate;
    private Vector3 secondObjectUpdate;

    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = gameObject.transform.position;
        defaultRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //When it follows two moving objects midpoint
        if (constantUpdate) {
            Vector3 midPoint;
            midPoint.x = (firstObjectUpdate.transform.position.x + secondObjectUpdate.x) / 2;
            midPoint.y = (firstObjectUpdate.transform.position.y + secondObjectUpdate.y) / 2;
            midPoint.z = (firstObjectUpdate.transform.position.z + secondObjectUpdate.z) / 2;

            Hashtable htz = new Hashtable {
                { "x", midPoint.x + offset.x},
                { "y", midPoint.y + offset.y},
                { "z", midPoint.z + offset.z},
                { "time", 0.1f }
            };

            iTween.MoveUpdate(gameObject, midPoint + offset, 1.0f);
        }
    }

    public void TargetMidPoint(Vector3 firstObject, Vector3 secondObject) {
        if (constantUpdate) return;
        Vector3 midPoint;
        midPoint.x = (firstObject.x + secondObject.x) / 2;
        midPoint.y = (firstObject.y + secondObject.y) / 2;
        midPoint.z = (firstObject.z + secondObject.z) / 2;

        Hashtable htz = new Hashtable {
                { "x", midPoint.x + offset.x},
                { "y", midPoint.y + offset.y},
                { "z", midPoint.z + offset.z},
                { "time", 2.0f }
            };
        iTween.MoveTo(gameObject, htz);
    }

    public void TargetMidPoint(GameObject firstObject, Vector3 secondObject) {
        if (constantUpdate) return;
        Vector3 midPoint;
        midPoint.x = (firstObject.transform.position.x + secondObject.x) / 2;
        midPoint.y = (firstObject.transform.position.y + secondObject.y) / 2;
        midPoint.z = (firstObject.transform.position.z + secondObject.z) / 2;

        Hashtable htz = new Hashtable {
                { "x", midPoint.x + offset.x},
                { "y", midPoint.y + offset.y},
                { "z", midPoint.z + offset.z},
                { "time", 2.0f }
            };
        iTween.MoveTo(gameObject, htz);
    }

    public void TargetMidPointUpdate(GameObject firstObject, Vector3 secondObject) {
        firstObjectUpdate = firstObject;
        secondObjectUpdate = secondObject;
        constantUpdate = true;
    }

    public void ReturnToDefaultPosition() {
        constantUpdate = false;
        Hashtable htx = new Hashtable {
                    { "x", defaultPosition.x},
                    { "y", defaultPosition.y},
                    { "z", defaultPosition.z},
                    { "time", 2.0f }
                };
        iTween.MoveTo(gameObject, htx);
    }

    public void MoveAndLook(Vector3 target, Vector3 targetOffset, Vector3 positionOffset) {
        constantUpdate = false;
        iTween.MoveUpdate(gameObject, target + positionOffset, 2.0f);
        iTween.LookUpdate(gameObject, target + targetOffset, 2.0f);
    }

    public void MoveAndLook(GameObject target, Vector3 targetOffset, Vector3 positionOffset) {
        constantUpdate = false;
        iTween.MoveUpdate(gameObject, target.transform.position + positionOffset, 2.0f);
        iTween.LookUpdate(gameObject, target.transform.position + targetOffset, 2.0f);
    }

    public void LookAtObject(Vector3 target) {
        iTween.LookTo(gameObject, target, 1);
    }

    public void SetNewPositionAsDefault() {
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
    }

}
