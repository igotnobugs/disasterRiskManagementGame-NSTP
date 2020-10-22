using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour {

    public StoryProgressionScript[] scenes;
    public int progression = 0;
    private StoryProgressionScript cs; //current Sscript used

    //View MainMenu First
    public Camera mCamera;
    public float startTimerValue;
    public Canvas UI;

    public GameObject initialCameraOrientation;
    public GameObject mCharacter;
    private CharacterControlScript characterScript;

    public GameObject mainMenuPanel;
    public GameObject explanationPanelRight;
    public GameObject explanationPanelLeft;
    public GameObject timerScorePanel;
    public GameObject scorePanel;
    public GameObject choicesControlPanel;
    public GameObject gameOverPanel;
    public GameObject victoryBeforePanel;
    public GameObject victoryPanel;

    public GameObject extraSoundObject;
    public AudioClip correctSound;
    public AudioClip wrongSound;

    private GameObject gameQuestionText;
    private Button firChoice;
    private Button secChoice;
    private Button thdChoice;
    private Vector3 firstObject;
    private Vector3 secondObject;
    private Vector3 thirdObject;

    public int totalMistakes = 0;
    public bool scored = false;

    private bool choiceIsPicked = false;  
    private bool sceneTransistionFinished = false;   
    private bool IsScriptRead = false;
    private bool correctChoicePicked = false;
    private bool showMainMenu = true;

    private TimerControlScript timer;
    private GameObject roof;
    private GameObject totalScore;
    

    // Start is called before the first frame update
    void Start() {
        UI = GameObject.FindGameObjectWithTag("gameUI").GetComponent<Canvas>();

        //mainMenuPanel = UI.GetComponentInChildren<MainMenuScript>();

        gameQuestionText = GameObject.Find("questionChoice");
        firChoice = GameObject.FindGameObjectWithTag("firstChoice").GetComponent<Button>();
        secChoice = GameObject.FindGameObjectWithTag("secondChoice").GetComponent<Button>();
        thdChoice = GameObject.FindGameObjectWithTag("thirdChoice").GetComponent<Button>();

        roof = GameObject.Find("roof");

        mCamera.transform.position = initialCameraOrientation.transform.position;
        mCamera.transform.rotation = initialCameraOrientation.transform.rotation;

        timer = UI.GetComponentInChildren<TimerControlScript>();
        totalScore = GameObject.FindGameObjectWithTag("totalScore");
        characterScript = mCharacter.GetComponent<CharacterControlScript>();

        Application.targetFrameRate = -1;
    }

    // Update is called once per frame
    void Update() {

        //Read Scene
        if (!IsScriptRead &&
            characterScript.destinationReached) {
            ReadScriptAndExecute(scenes[progression]);
        }

        //For Victory Scene End, show Victory Panel
        if (cs.isVictoryScene) {
            if (cs.isVictoryBefore) {
                victoryBeforePanel.GetComponent<PopUpScript>().Show();
            } else {
                victoryBeforePanel.GetComponent<PopUpScript>().Hide();
                totalScore.GetComponent<TMPro.TextMeshProUGUI>().text = scorePanel.GetComponent<TimerScoreControlScript>().GetScore().ToString();
                victoryPanel.GetComponent<PopUpScript>().Show();
            }
        }

        // Wait for camera transisition to be finished
        if (!cs.isVictoryScene && 
            cs.allowCameraMovement &&
            sceneTransistionFinished && 
            totalMistakes < 3) {

            mCharacter.GetComponent<CharacterControlScript>().target = secondObject;
            mCharacter.GetComponent<CharacterControlScript>().faceTowardsTarget = true;

            if (!correctChoicePicked) {
                if (!cs.isMainMenu) {
                    timer.StartTimer();
                    timerScorePanel.GetComponent<PopUpScript>().Show();
                    choicesControlPanel.GetComponent<PopUpScript>().Show();
                }
                else {
                    if (showMainMenu) {
                        mainMenuPanel.GetComponent<PopUpScript>().Show();
                        timerScorePanel.GetComponent<PopUpScript>().Hide();
                        choicesControlPanel.GetComponent<PopUpScript>().Hide();
                    }
                }
            }
        }

        //After choices, character will move towards it
        //after it reached, move camera towards it and show the reason
        //Player has to click ok, to advance to next scene
        if (!cs.isMainMenu && 
            correctChoicePicked &&
            characterScript.destinationReached) {
            characterScript.target = mCamera.transform.position;
            mCamera.GetComponent<CameraControlScript>().MoveAndLook(mCharacter, cs.lookOffset, cs.positionOffset);
            mCharacter.GetComponent<CharacterControlScript>().faceTowardsTarget = true;
            timerScorePanel.GetComponent<PopUpScript>().Hide();
            choicesControlPanel.GetComponent<PopUpScript>().Hide();
            roof.GetComponent<VisualEffectScript>().FadeIn();

            if (cs.explanationTextAppearLeft) {
                explanationPanelLeft.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = cs.explanationText;
                explanationPanelLeft.GetComponent<PopUpScript>().Show();
            } else {
                explanationPanelRight.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = cs.explanationText;
                explanationPanelRight.GetComponent<PopUpScript>().Show();
            }
        }

        //Game Over Panel Show
        if (totalMistakes >= 3) {
            timer.PauseTimer();
            gameOverPanel.GetComponent<PopUpScript>().Show();
            timerScorePanel.GetComponent<PopUpScript>().Hide();
            choicesControlPanel.GetComponent<PopUpScript>().Hide();
        }


    }

    //When a choice button is hovered
    public void ButtonHovered(string tagName) {

        if (choiceIsPicked) return;
        if (!cs.allowCameraMovement) return;
        if (!sceneTransistionFinished) return;

        mCharacter.GetComponent<CharacterControlScript>().isLooking = cs.characterWillLook;
        //firstObject = mCharacter.transform.position;

        if (tagName == "firstChoice") {
            secondObject = cs.firstObjectPosition;
        }

        if (tagName == "secondChoice") {
            secondObject = cs.secondObjectPosition;
        }

        if (tagName == "thirdChoice") {
            secondObject = cs.thirdObjectPosition;
        }

        mCamera.GetComponent<CameraControlScript>().TargetMidPoint(mCharacter, secondObject);
    }

    //When a choice button is hovered away
    public void ButtonAway() {
        if (choiceIsPicked) return;
        if (correctChoicePicked) return;
        if (!sceneTransistionFinished) return;

        if (cs.allowReturnPosition)
            mCamera.GetComponent<CameraControlScript>().ReturnToDefaultPosition();

        mCharacter.GetComponent<CharacterControlScript>().isLooking = false;

    }

    //When a choice button is clicked
    public void ButtonClicked(string tagName) {
        if (choiceIsPicked) return;
        if (!sceneTransistionFinished) return;

        if (tagName == cs.correctChoice) {
            correctChoicePicked = true;
            choicesControlPanel.GetComponent<PopUpScript>().Hide();
            timer.PauseTimer();

            characterScript.isWalking = cs.characterWillWalk;
            characterScript.isRunning = !cs.characterWillWalk;

            //Camera will follow the MidPoint of two objects
            mCamera.GetComponent<CameraControlScript>().TargetMidPointUpdate(mCharacter, cs.secondObjectPosition);

            int timeLeft = timer.GetFloatTimerToInt(2);
            scorePanel.GetComponent<TimerScoreControlScript>().AddScore(timeLeft);

            extraSoundObject.GetComponent<AudioSource>().clip = correctSound;
            
            if (!cs.characterWillMove) {
                characterScript.isLooking = false;
                return;
            }

            if (tagName == "firstChoice") {               
                characterScript.MoveToPosition(cs.firstObjectPosition);
            }

            if (tagName == "secondChoice") {
                characterScript.MoveToPosition(cs.secondObjectPosition);
            }

            if (tagName == "thirdChoice") {
                characterScript.MoveToPosition(cs.thirdObjectPosition);
            }

        } else {
            extraSoundObject.GetComponent<AudioSource>().clip = wrongSound;
            totalMistakes += 1;
        }
        extraSoundObject.GetComponent<AudioSource>().Play();
    }

    //When the start button in main menu is clicked
    public void StartGame() {
        if (choiceIsPicked) return;
        if (!sceneTransistionFinished) return;
        correctChoicePicked = true;
        showMainMenu = false;

        mainMenuPanel.GetComponent<PopUpScript>().Hide();
        this.GetComponent<AudioSource>().volume /= 2;

        progression += 1;
        IsScriptRead = false;
    }

    //When the explanation button is clicked
    public void ButtonExplanationNext() {
        explanationPanelLeft.GetComponent<PopUpScript>().Hide();
        explanationPanelRight.GetComponent<PopUpScript>().Hide();
        progression += 1;
        IsScriptRead = false;
        ReadScriptAndExecute(scenes[progression]);
    }

    private void ReadScriptAndExecute(StoryProgressionScript theScript) {
        cs = theScript;
        sceneTransistionFinished = false;
        choiceIsPicked = false;
        correctChoicePicked = false;

        if (cs.isVictoryScene) {
            sceneTransistionFinished = true;
            IsScriptRead = true;
            return;
        }

        //St up question text
        gameQuestionText.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = cs.question;

        //Set up Choices
        firChoice.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = cs.choices[0];
        secChoice.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = cs.choices[1];
        thdChoice.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = cs.choices[2];

        //Set up object positions
        firstObject = cs.firstObjectPosition;
        secondObject = cs.secondObjectPosition;
        thirdObject = cs.thirdObjectPosition;

        //Move the camera
        Hashtable ht = new Hashtable {
            { "position", cs.cameraLocation },
            { "time", 2.0f },
            { "oncompletetarget", gameObject },
            { "oncomplete", "OnTransistionComplete" }
        };
        iTween.MoveTo(mCamera.gameObject, ht);

        Hashtable htp = new Hashtable {
            { "rotation", cs.cameraRotation },
            { "time", 2.0f },
            { "oncompletetarget", gameObject },
            { "oncomplete", "OnTransistionComplete" }
        };
        iTween.RotateTo(mCamera.gameObject, htp);

        //Hide Roof
        if (!roof.GetComponent<VisualEffectScript>().isFadeOut && cs.fadeOutRoof)
            roof.GetComponent<VisualEffectScript>().FadeOut();

        timer.SetTimer(cs.timer);
        timer.ResumeTimer();

        //Move character if true
        if (cs.newCharacterPosition) {
            characterScript.isWalking = cs.characterWillWalk;
            characterScript.isRunning = !cs.characterWillWalk;
            characterScript.MoveToPosition(cs.characterPosition);
        }

        //Set script to be read
        IsScriptRead = true;
    }

    private void OnTransistionComplete() {
        if (!sceneTransistionFinished) {
            mCamera.GetComponent<CameraControlScript>().SetNewPositionAsDefault();
            sceneTransistionFinished = true;
        }
    }

    private bool IsReadyForNextScene() {
        return sceneTransistionFinished;
    }


    public void ResetGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame() {
#if UNITY_STANDALONE
        Application.Quit();
#endif

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}