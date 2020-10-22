using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Store Progression")]
public class StoryProgressionScript : ScriptableObject
{
    public bool isVictoryBefore;
    public bool isVictoryScene;

    public Vector3 cameraLocation;
    public Vector3 cameraRotation;

    [TextArea]
    public string question;
    public string[] choices;
    public string correctChoice;

    public Vector3 firstObjectPosition;
    public Vector3 secondObjectPosition;
    public Vector3 thirdObjectPosition;

    public bool newCharacterPosition;
    public Vector3 characterPosition;
    public bool fadeOutRoof;
    public bool characterWillMove;
    public bool characterWillLook;
    public bool characterWillWalk;
    public bool allowCameraMovement;
    public float timer;

    public bool allowReturnPosition = false;
    public bool isMainMenu = false;

    public bool lookAtCharacter;
    public Vector3 lookOffset;
    public Vector3 positionOffset;
    [TextArea]
    public string explanationText;
    public bool explanationTextAppearLeft;

    public bool lookAtCamera;
}
