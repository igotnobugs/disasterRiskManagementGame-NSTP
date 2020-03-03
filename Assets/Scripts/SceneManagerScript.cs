using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour {

    public Camera mCamera;
    public float startTimerValue;
    public Canvas UI;

    public string firstChoiceText;
    public string secondChoiceText;
    public string thirdChoiceText;

    public GameObject character;
    public GameObject table;
    public GameObject door;

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


    // Start is called before the first frame update
    void Start() {
        UI = GameObject.FindGameObjectWithTag("gameUI").GetComponent<Canvas>();

        firChoice = GameObject.FindGameObjectWithTag("firstChoice").GetComponent<Button>();
        secChoice = GameObject.FindGameObjectWithTag("secondChoice").GetComponent<Button>();
        thdChoice = GameObject.FindGameObjectWithTag("thirdChoice").GetComponent<Button>();

        firChoice.GetComponentInChildren<Text>().text = firstChoiceText;
        secChoice.GetComponentInChildren<Text>().text = secondChoiceText;
        thdChoice.GetComponentInChildren<Text>().text = thirdChoiceText;


        timer = GameObject.FindGameObjectWithTag("timerText").GetComponent<Text>();
        timerCount = startTimerValue;
        timer.text = timerCount.ToString("f2");
    }

    // Update is called once per frame
    void Update() {
        //Timer CountDown
        timerCount -= 1 * Time.deltaTime;
        timer.text = timerCount.ToString("f2");



        if (firstObject != null) {
            //Get midpoint of the two objects, send it to Camera as target
            midPoint.x = (firstObject.transform.position.x + secondObject.transform.position.x) / 2;
            midPoint.y = 0;
            midPoint.z = ((firstObject.transform.position.z + secondObject.transform.position.z) / 2);
            mCamera.GetComponent<CameraControlScript>().target = midPoint;

            character.GetComponent<CharacterControlScript>().target = secondObject.transform.position;
            character.GetComponent<CharacterControlScript>().faceTowardsTarget = true;
        }

    }


    public void buttonHovered(string tagName) {
        if (tagName == "firstChoice") {
            mCamera.GetComponent<CameraControlScript>().newTarget();
            firstObject = character;
            secondObject = door;
            character.GetComponent<CharacterControlScript>().isLooking = true;
        }

        if (tagName == "secondChoice") {
            mCamera.GetComponent<CameraControlScript>().newTarget();
            firstObject = character;
            secondObject = table;
            character.GetComponent<CharacterControlScript>().isLooking = true;
        }

        if (tagName == "thirdChoice") {
            mCamera.GetComponent<CameraControlScript>().newTarget();
            firstObject = character;
            secondObject = character;
            character.GetComponent<CharacterControlScript>().isLooking = false;
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(midPoint, 1);
    }
}


