using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScript : MonoBehaviour
{
    //For the current Scene
    public GameObject cameraLocation;
    public string questionText;
    public string firstChoiceText;
    public string secondChoiceText;
    public string thirdChoiceText;

    //public GameObject mainCharacter;
    public GameObject firstObject = null;
    public GameObject secondObject = null;
    public GameObject thirdObject = null;

    //input tag
    public string correctChoice;
    public bool allowCameraMovement;
    public bool setTimer;
    public float timer;

    public GameObject nextScene = null;

    public bool allowReturnPosition = false;

    public bool isMainMenu = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
