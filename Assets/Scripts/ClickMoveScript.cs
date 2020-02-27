using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMoveScript : MonoBehaviour {

    public bool isSelected;
    public Vector3 moveToPosition;
    public GameObject gameObject;

    // Use this for initialization
    void Start () {
        isSelected = false;
        moveToPosition = this.transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        if (isSelected) {

            if (Input.GetMouseButtonDown(0)) {
                moveToPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                moveToPosition.y = this.gameObject.transform.position.y;
            }

            this.gameObject.transform.position = moveToPosition;
            
        }
        Debug.Log(this.gameObject.transform.position + " " + moveToPosition);
    }

    //object is clicked
    void OnMouseDown() {
        isSelected = true;
    }
}
