using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour {

    public Camera mainCamera;
    public float startTimerValue;
    public Canvas UI;

    public GameObject currentScene;

    public GameObject mainCharacter;

    private Button firChoice;
    private Button secChoice;
    private Button thdChoice;

    private GameObject firstObject;
    private GameObject secondObject;
    private GameObject thirdObject;

    private Text timer;
    private Text firText;
    private float timerCount; 
    private Vector3 midPoint = new Vector3();

    private bool choiceIsPicked = false;
    private float score = 0;
    private Text scoreText;
    public bool scored = false;

    private SceneScript currentScript;

    private bool sceneTransistionFinished = false;

    private bool isTimerSet = false;

    // Start is called before the first frame update
    void Start() {
        UI = GameObject.FindGameObjectWithTag("gameUI").GetComponent<Canvas>();

        firChoice = GameObject.FindGameObjectWithTag("firstChoice").GetComponent<Button>();
        secChoice = GameObject.FindGameObjectWithTag("secondChoice").GetComponent<Button>();
        thdChoice = GameObject.FindGameObjectWithTag("thirdChoice").GetComponent<Button>();

        timer = GameObject.FindGameObjectWithTag("timerText").GetComponent<Text>();
        //timerCount = startTimerValue;
        //timer.text = timerCount.ToString("f2");

        scoreText = GameObject.FindGameObjectWithTag("scoreText").GetComponent<Text>();

        //First scene to do, is the selected current Scene      
        //GotToScene(currentScene);
    }

    // Update is called once per frame
    void Update() {
        //Timer Countdown
        if (isTimerSet) {
            if (!choiceIsPicked) timerCount -= 1 * Time.deltaTime;
            else {
                if (!scored) {
                    score += Mathf.Round(timerCount * 10);
                    scored = true;
                }
            }
        }
        //Scoring
        scoreText.text = score.ToString();
        timer.text = timerCount.ToString("f2");

        if (!sceneTransistionFinished) {
            GotToScene(currentScene);
            choiceIsPicked = false;
        }
        

        if (currentScript.allowCameraMovement) {
            //Get midpoint of the two objects, send it to Camera as target
            midPoint.x = (firstObject.transform.position.x + secondObject.transform.position.x) / 2;
            midPoint.y = 0;
            midPoint.z = ((firstObject.transform.position.z + secondObject.transform.position.z) / 2);
            mainCamera.GetComponent<CameraControlScript>().target = midPoint;

            mainCharacter.GetComponent<CharacterControlScript>().target = secondObject.transform.position;
            mainCharacter.GetComponent<CharacterControlScript>().faceTowardsTarget = true;     
        }
    }


    public void ButtonHovered(string tagName) {

        if (choiceIsPicked) return;
        if (!currentScript.allowCameraMovement) return;
        if (!sceneTransistionFinished) return;

       
        mainCamera.GetComponent<CameraControlScript>().isTargeting = true;

        if (tagName == "firstChoice") {
            mainCamera.GetComponent<CameraControlScript>().NewTarget();
            firstObject = mainCharacter;
            secondObject = currentScript.firstObject;
            mainCharacter.GetComponent<CharacterControlScript>().isLooking = true;
        }

        if (tagName == "secondChoice") {
            mainCamera.GetComponent<CameraControlScript>().NewTarget();
            firstObject = mainCharacter;
            secondObject = currentScript.secondObject;
            mainCharacter.GetComponent<CharacterControlScript>().isLooking = true;
        }

        if (tagName == "thirdChoice") {
            mainCamera.GetComponent<CameraControlScript>().NewTarget();
            firstObject = mainCharacter;
            secondObject = currentScript.thirdObject;
            mainCharacter.GetComponent<CharacterControlScript>().isLooking = false;
        }
    }

    public void ButtonClicked(string tagName) {

        if (choiceIsPicked) return;
        if (!sceneTransistionFinished) return;

        if (tagName == currentScript.correctChoice) { 
            SetNextScene(currentScript.nextScene);
        }

        choiceIsPicked = true;
    }

    public void ButtonAway() {

        if (choiceIsPicked) return;
        if (!sceneTransistionFinished) return;

        mainCamera.GetComponent<CameraControlScript>().allowReturnPosition = currentScript.allowReturnPosition;
        mainCamera.GetComponent<CameraControlScript>().NewTarget();
        mainCamera.GetComponent<CameraControlScript>().isTargeting = false;
        mainCharacter.GetComponent<CharacterControlScript>().isLooking = false;

    }

    public void GotToScene(GameObject scene) {      
        currentScript = scene.GetComponent<SceneScript>();

        firChoice.GetComponentInChildren<Text>().text = currentScript.firstChoiceText;
        secChoice.GetComponentInChildren<Text>().text = currentScript.secondChoiceText;
        thdChoice.GetComponentInChildren<Text>().text = currentScript.thirdChoiceText;

        firstObject = currentScript.firstObject;
        secondObject = currentScript.secondObject;
        thirdObject = currentScript.thirdObject;

        mainCamera.GetComponent<CameraControlScript>().GoToNewPosition(currentScript.cameraLocation, 0.01f);
        if (mainCamera.GetComponent<CameraControlScript>().ReachedTargetLocation(currentScript.cameraLocation)) {
            sceneTransistionFinished = true;
        }

        timerCount = currentScript.timer;
        isTimerSet = currentScript.setTimer;       
    }

    public void SetNextScene(GameObject next) {
        currentScene = next;
        mainCamera.GetComponent<CameraControlScript>().NewTarget();
        sceneTransistionFinished = false;
        
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(midPoint, 1);
    }
}


