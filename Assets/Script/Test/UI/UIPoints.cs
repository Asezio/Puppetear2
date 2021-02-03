using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPoints : MonoBehaviour
{
    public Text ScoreText;
    public Text SPText;
    private int currentScore;
    private int currentSP;
    // Start is called before the first frame update
    void Start()
    {
        currentScore = GameObject.FindGameObjectWithTag("Player").GetComponent<Points>().sp.currentPoint;
        currentSP = GameObject.FindGameObjectWithTag("Player").GetComponent<Points>().sp.skillPoint;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text= "Score: " + currentScore.ToString();
        SPText.text = "Skill Points: " + currentSP.ToString();
    }
}
