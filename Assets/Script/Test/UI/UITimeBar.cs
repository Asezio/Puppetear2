using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimeBar : MonoBehaviour
{
    public Text timeText;
    public float timeMax;
    public static float timeLeft;
    private int convertTime;
    private Image TimeBar;
    public bool isActive;


    // Use this for initialization
    void Start()
    {
        TimeBar = GetComponent<Image>();
        timeLeft = timeMax;
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive == true)
        {
            timeLeft = timeLeft - Time.deltaTime;
        }
        TimeBar.fillAmount = timeLeft / timeMax;
        convertTime = (int)timeLeft;
        timeText.text = "Time Left: " + convertTime.ToString() + "s";
    }
}
