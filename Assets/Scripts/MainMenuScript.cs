using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platinio.TweenEngine;

public class MainMenuScript : MonoBehaviour
{
    private bool showPanel = false;

    // Start is called before the first frame update
    void Start() {
        transform.position = new Vector3(400, -45, 0);
    }

    // Update is called once per frame
    void Update() {
        if (!showPanel) {
            transform.Move(new Vector3(400, -45, 0), 0.3f);
            //
        }
        else {
            transform.Move(new Vector3(400, 64, 0), 0.3f);
        }
    }

    public void Show() { 
        if (!showPanel)
            showPanel = true;
    }

    public void Hide() {
        if (showPanel)
            showPanel = false;
    }
}
