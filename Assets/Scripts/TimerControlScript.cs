using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Platinio.UI;

public class TimerControlScript : MonoBehaviour
{
    public GameObject circularTimer;

    private TMPro.TextMeshProUGUI timerText;
    private float startingTimer = 0;
    private float timerCount = 0;
    private bool pauseTimer = false;
    private bool startTimer = false; 

    private PopUpScript timerControlPanelPopUpScript;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        timerControlPanelPopUpScript = GetComponentInParent<PopUpScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer && timerControlPanelPopUpScript.GetVisibility()) {
            if (!pauseTimer && timerCount > 0)
                timerCount -= 1 * Time.deltaTime;
            if (timerCount < 0) timerCount = 0;
            circularTimer.GetComponentInChildren<Image>().fillAmount = timerCount / startingTimer;
        }

        timerText.text = timerCount.ToString("f0");
    }

    public int GetFloatTimerToInt(int decimals) {
        float timerMult = timerCount * Mathf.Pow(10, decimals);
        return (int)timerMult;
    }

    public void SetTimer(float time) {
        startingTimer = time;
        timerCount = time;
        startTimer = false;
    }

    public void StartTimer() {
        if (!startTimer) {
            startTimer = true;
        }      
    }

    public void PauseTimer() {
        if (!pauseTimer)
            pauseTimer = true;
    }

    public void ResumeTimer() {
        if (pauseTimer)
            pauseTimer = false;
    }
}
