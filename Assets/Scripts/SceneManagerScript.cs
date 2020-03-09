using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour {

    //View MainMenu First

    public Camera mainCamera;
    public float startTimerValue;
    public Canvas UI;

    public GameObject currentScene;
    public GameObject initialCameraOrientation;

    public GameObject mainCharacter;

    public bool inMainMenu;

    private GameObject gameQuestionText;

    private Button firChoice;
    private Button secChoice;
    private Button thdChoice;

    private GameObject firstObject;
    private GameObject secondObject;
    private GameObject thirdObject;

    private Vector3 midPoint = new Vector3();

    private bool choiceIsPicked = false;

    public bool scored = false;

    private SceneScript currentScript;

    private bool sceneTransistionFinished = false;

    private bool isTimerSet = false;

    private MainMenuScript mainMenuPanel;
    private TimerScoreControlScript timerScorePanel;
    private ChoicesControlScript choicesControlPanel;
    private TimerControlScript timer;
    private GameObject roof;

    public int totalMistakes = 0;

    // Start is called before the first frame update
    void Start() {
        UI = GameObject.FindGameObjectWithTag("gameUI").GetComponent<Canvas>();

        firChoice = GameObject.FindGameObjectWithTag("firstChoice").GetComponent<Button>();
        secChoice = GameObject.FindGameObjectWithTag("secondChoice").GetComponent<Button>();
        thdChoice = GameObject.FindGameObjectWithTag("thirdChoice").GetComponent<Button>();

        roof = GameObject.Find("roof");

        mainCamera.transform.position = initialCameraOrientation.transform.position;
        mainCamera.transform.rotation = initialCameraOrientation.transform.rotation;

        timerScorePanel = UI.GetComponentInChildren<TimerScoreControlScript>();
        timer = UI.GetComponentInChildren<TimerControlScript>();
        choicesControlPanel = UI.GetComponentInChildren<ChoicesControlScript>();
        mainMenuPanel = UI.GetComponentInChildren<MainMenuScript>();

        gameQuestionText = GameObject.Find("questionChoice");
    }

    // Update is called once per frame
    void Update() {
       
        //Camera is moving
        if (!sceneTransistionFinished) {
            GotToScene(currentScene);
            choiceIsPicked = false;

            if (!currentScript.isMainMenu) {
                //mainMenuPanel.Hide();
                Color currentColor = roof.GetComponent<MeshRenderer>().material.color;

                if (currentColor.a != 0) {
                    roof.GetComponent<MeshRenderer>().material.color
                        = new Color(currentColor.a, currentColor.g, currentColor.b, currentColor.a - 0.5f * Time.deltaTime);
                    if (currentColor.a < 0) {
                        roof.GetComponent<MeshRenderer>().material.color
                        = new Color(currentColor.a, currentColor.g, currentColor.b, 0);
                    }
                }
            }

        } else {

            if (!currentScript.isMainMenu) {
                timerScorePanel.Show();
                choicesControlPanel.Show();
                timer.StartTimer(currentScript.timer);
            }
            else {
                mainMenuPanel.Show();
                timerScorePanel.Hide();
                choicesControlPanel.Hide();

                //Hide Roof
                roof.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }

            if (currentScript.allowCameraMovement) {
                //Get midpoint of the two objects, send it to Camera as target
                midPoint.x = (firstObject.transform.position.x + secondObject.transform.position.x) / 2;
                midPoint.y = (firstObject.transform.position.y + secondObject.transform.position.y) / 2; ;
                midPoint.z = ((firstObject.transform.position.z + secondObject.transform.position.z) / 2);
                mainCamera.GetComponent<CameraControlScript>().target = midPoint;

                mainCharacter.GetComponent<CharacterControlScript>().target = secondObject.transform.position;
                mainCharacter.GetComponent<CharacterControlScript>().faceTowardsTarget = true;
            }

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

        } else {
            if (!inMainMenu) {
                totalMistakes += 1;
            }
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

        firChoice.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentScript.firstChoiceText;
        secChoice.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentScript.secondChoiceText;
        thdChoice.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentScript.thirdChoiceText;

        firstObject = currentScript.firstObject;
        secondObject = currentScript.secondObject;
        thirdObject = currentScript.thirdObject;

        gameQuestionText.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentScript.questionText;

        Hashtable ht = new Hashtable {
            { "position", currentScript.cameraLocation.transform.position },
            { "time", 2.0f },
            { "oncompletetarget", gameObject },
            { "oncomplete", "OnTransistionComplete" }
        };
        iTween.MoveTo(mainCamera.gameObject, ht);

        Hashtable htp = new Hashtable {
            { "rotation", currentScript.cameraLocation.transform.rotation.eulerAngles },
            { "time", 2.0f },
            { "oncompletetarget", gameObject },
            { "oncomplete", "OnTransistionComplete" }
        };
        iTween.RotateTo(mainCamera.gameObject, htp);

        //timerCount = currentScript.timer;
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

    private void OnTransistionComplete() {
        if (!sceneTransistionFinished) {
            mainCamera.GetComponent<CameraControlScript>().SetNewPositionAsDefault();
            sceneTransistionFinished = true;
        }
    }
}


