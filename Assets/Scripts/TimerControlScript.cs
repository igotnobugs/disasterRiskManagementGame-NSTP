using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerControlScript : MonoBehaviour
{
    private TMPro.TextMeshProUGUI timerText;

    private float startingTimer = 0;
    private float timerCount = 0;
    private bool pauseTimer = false;
    private bool startTimer = false;

    private bool showTimer = true;
    public GameObject circularTimer;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponentInChildren<TMPro.TextMeshProUGUI>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (!showTimer) {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
        } else {
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
        }

        if (startTimer) {
            if (!pauseTimer && timerCount > 0)
                timerCount -= 1 * Time.deltaTime;
            if (timerCount < 0) timerCount = 0;
            circularTimer.GetComponentInChildren<Image>().fillAmount = timerCount / startingTimer;
        }

        timerText.text = timerCount.ToString("f2");
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

    public void ShowTimer() {
        if (!showTimer)
            showTimer = true;
    }

    public void HideTimer() {
        if (showTimer)
            showTimer = false;
    }

}
