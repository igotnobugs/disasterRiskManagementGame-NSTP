using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScoreControlScript : MonoBehaviour
{
    private int scoreNum;
    private TMPro.TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void AddScore(int score) {
        scoreNum += score;
        scoreText.text = scoreNum.ToString();
    }

    public int GetScore() {
        return scoreNum;
    }
}
