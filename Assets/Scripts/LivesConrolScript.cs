using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesConrolScript : MonoBehaviour
{
    public SceneManagerScript sceneManager;

    public Image mistake1Icon;
    public Image mistake2Icon;
    public Image mistake3Icon;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneManager.totalMistakes >= 1) {
            mistake1Icon.enabled = true;
        }
        if (sceneManager.totalMistakes >= 2) {
            mistake2Icon.enabled = true;
        }
        if (sceneManager.totalMistakes >= 3) {
            mistake3Icon.enabled = true;
        }
    }
}
