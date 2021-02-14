using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRecord : MonoBehaviour
{
    // Start is called before the first frame update
    public Text winTime;
    public Text loseTime;
    public Text winScore;
    public Text loseScore;
    private Points playerPoints;
    private float usedTime;

    void Awake()
    {
        playerPoints = GameObject.FindGameObjectWithTag("Player").GetComponent<Points>();        
    }
    // Update is called once per frame

    private void CountWinTime()
    {
        usedTime = playerPoints.sp.totalTime;
        int a = Mathf.FloorToInt(usedTime);
        winTime.text = string.Format("{0:d2}:{1,d2}:{2:d2}", a / 3600, a / 60, a % 60);
        //winTime.text = "00:00:00";
    }
    private void CountLoseTime()
    {
        usedTime = playerPoints.sp.totalTime;
        int a = Mathf.FloorToInt(usedTime);
        loseTime.text = string.Format("{0:d2}:{1:d2}:{2:d2}", a / 3600, a / 60, a % 60);
        //loseTime.text = "00:00:00";
    }
    public void CountScore()
    {
        CountLoseTime();
        loseScore.text = playerPoints.sp.currentPoint.ToString();
    }
    public void BestScore()
    {
        CountWinTime();
        winScore.text = playerPoints.sp.bestScore.ToString();
    }
}
