using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectScript : MonoBehaviour {

    public bool isFadeOut = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeOut() {
        iTween.FadeTo(gameObject, 0, 1);
        GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        isFadeOut = true;
    }

    public void FadeIn() {
        iTween.FadeTo(gameObject, 1, 1);
        GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        isFadeOut = false;
    }
}
