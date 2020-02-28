using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMoveScript : MonoBehaviour {

    public bool isSelected;
    public Vector3 moveToPosition;
    public GameObject box;

    // Use this for initialization
    void Start () {
        isSelected = false;
        moveToPosition = this.transform.position;

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.A)) {
            this.gameObject.transform.Translate(new Vector3(-5 * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.W))
        {
            this.gameObject.transform.Translate(new Vector3(0, 0, 5 * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.gameObject.transform.Translate(new Vector3(0, 0, -5 * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.gameObject.transform.Translate(new Vector3(5 * Time.deltaTime, 0, 0));
        }

    }



    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Table (1)")
        {
            Destroy(col.gameObject);
        }
    }
}
