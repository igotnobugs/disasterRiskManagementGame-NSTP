using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoicesControlScript : MonoBehaviour
{
    private bool showPanel = false;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (!showPanel) {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
        }
        else {
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
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
