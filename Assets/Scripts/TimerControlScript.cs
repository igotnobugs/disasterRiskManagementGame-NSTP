using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Platinio.UI;

public class TimerControlScript : MonoBehaviour
{
    private TMPro.TextMeshProUGUI timerText;

    private float startingTimer = 0;
    private float timerCount = 0;
    private bool pauseTimer = false;
    private bool startTimer = false;
    public GameObject circularTimer;

    private TimerScoreControlScript timerControlPanel;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        timerControlPanel = GetComponentInParent<TimerScoreControlScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer && timerControlPanel.isVisible) {
            if (!pauseTimer && timerCount > 0)
                timerCount -= 1 * Time.deltaTime;
            if (timerCount < 0) timerCount = 0;
            circularTimer.GetComponentInChildren<Image>().fillAmount = timerCount / startingTimer;
        }

        timerText.text = timerCount.ToString("f1");
    }

    public void StartTimer(float time) {
        if (!startTimer) {
            startTimer = true;
            startingTimer = time;
            timerCount = time;
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
