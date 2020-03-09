using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObjectScript : MonoBehaviour
{
    public bool startVisible = true;

    private bool isVisible = true;

    // Start is called before the first frame update
    void Start()
    {
        Color currentColor = gameObject.GetComponent<MeshRenderer>().material.color;
        if (currentColor.a != 0) {
            gameObject.GetComponent<MeshRenderer>().material.color
                = new Color(currentColor.a, currentColor.g, currentColor.b, currentColor.a - 0.5f * Time.deltaTime);
            if (currentColor.a < 0) {
                gameObject.GetComponent<MeshRenderer>().material.color
                = new Color(currentColor.a, currentColor.g, currentColor.b, 0);
            }
        }
        isVisible = startVisible;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeOut() {
        if (isVisible) {
            isVisible = false;
        }
    }

    public void FadeIn() {
        if (!isVisible) {
            isVisible = true;
        }
    }

}
